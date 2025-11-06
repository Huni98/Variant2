using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity; // For .Include()
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using UMFST.MIP.CarServiceDashboard.Data; // Your AppContext
using UMFST.MIP.CarServiceDashboard.Models; // Your Entities
using UMFST.MIP.CarServiceDashboard.Services.DTOs; // The DTOs I provided earlier
using System.Net;

// 1. 
// 1. MANDATORY: Change your namespace to match the exam rules
//
namespace Variant2

{
    public partial class MainWindow : Form
    {
        private const string JsonFileName = "data_car_service.json";
        private const string ErrorLogFileName = "invalid_car_service.txt";
        private const string ExportFileName = "workorder_summary.txt";

        // 2. 
        // 2. This must be a class-level field, not a local variable
        //
        private readonly StringBuilder _errorLog = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();

            // 3. 
            // 3. Manually wire up event handlers
            //
            this.Load += MainWindow_Load;
            dgvWorkOrders.CellFormatting += dgvWorkOrders_CellFormatting;
            dgvTestResults.CellFormatting += dgvTestResults_CellFormatting;
            lbClients.SelectedIndexChanged += lbClients_SelectedIndexChanged;
            cmbCarForDiagnostics.SelectedIndexChanged += cmbCarForDiagnostics_SelectedIndexChanged;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // On startup, try to load data from the DB if it exists
            LoadAllData();
        }

        /// <summary>
        /// Helper to refresh all data in the UI
        /// </summary>
        private void LoadAllData()
        {
            LoadWorkOrdersGrid();
            LoadClientsList();
            LoadDiagnosticsCarComboBox();

            // Clear dependent grids
            dgvCars.DataSource = null;
            dgvObdCodes.DataSource = null;
            dgvTestResults.DataSource = null;
        }

        /// <summary>
        /// Helper to log an invalid entry and append to the log
        /// </summary>
        private void LogInvalid(string message, ref int counter)
        {
            _errorLog.AppendLine(message);
            counter++;
        }


        #region 1. Reset/Import Button (The Core Logic)

        private void btnResetImport_Click(object sender, EventArgs e)
        {
            _errorLog.Clear();
            int invalidEntries = 0;

            // 1. Reset Database
            try
            {
                using (var context = new UMFST.MIP.CarServiceDashboard.Data.AppContext())
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not reset database: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Download JSON file
            string jsonData;
            const string jsonUrl = "https://cdn.shopify.com/s/files/1/0883/3282/8936/files/data_car_service.json?v=1762418871";

            try
            {
                using (WebClient client = new WebClient())
                {
                    jsonData = client.DownloadString(jsonUrl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not download JSON: {ex.Message}", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Deserialize into DTOs (not Entities)
            RootDto importedData;
            try
            {
                // Use the DTO class RootDto
                importedData = JsonConvert.DeserializeObject<RootDto>(jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing JSON: {ex.Message}", "JSON Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Validate and Import
            using (var context = new UMFST.MIP.CarServiceDashboard.Data.AppContext())
            {
                try
                {
                    // A: Mechanics [cite: 1]
                    foreach (var mechDto in importedData.Garage.Mechanics)
                    {
                        if (mechDto.Years < 0 || string.IsNullOrEmpty(mechDto.Name))
                        {
                            LogInvalid($"Invalid Mechanic {mechDto.Id}: Negative years or no name. Skipped.", ref invalidEntries);
                            continue;
                        }
                        context.Mechanics.Add(new Mechanic
                        {
                            Id = mechDto.Id,
                            Name = mechDto.Name,
                            Years = mechDto.Years,
                            Specializations = string.Join(",", mechDto.Specialization)
                        });
                    }
                    context.SaveChanges(); // Save to satisfy foreign keys

                    // B: Clients and Cars [cite: 2, 3]
                    foreach (var clientDto in importedData.Clients)
                    {
                        if (string.IsNullOrEmpty(clientDto.Name) || !IsEmailValid(clientDto.Email))
                        {
                            LogInvalid($"Invalid Client {clientDto.Id}: Bad name or email '{clientDto.Email}'. Skipped.", ref invalidEntries);
                            continue;
                        }

                        var newClient = new Client
                        {
                            Id = clientDto.Id,
                            Name = clientDto.Name.Replace("\n", ""), // Clean up data
                            Phone = clientDto.Phone,
                            Email = clientDto.Email
                        };
                        context.Clients.Add(newClient);

                        foreach (var carDto in clientDto.Cars)
                        {
                            if (!IsVinValid(carDto.Vin) || carDto.OdometerKm < 0)
                            {
                                LogInvalid($"Invalid Car {carDto.Vin}: Bad VIN or negative odometer. Skipped.", ref invalidEntries);
                                continue;
                            }
                            context.Cars.Add(new Car
                            {
                                Vin = carDto.Vin,
                                Make = carDto.Make,
                                Model = carDto.Model,
                                Year = carDto.Year,
                                Odometer = carDto.OdometerKm, // Map DTO 'OdometerKm' to Entity 'Odometer'
                                ClientId = newClient.Id // Link to the client
                            });
                        }
                    }
                    context.SaveChanges(); // Save Clients/Cars

                    // C: Work Orders [cite: 4, 5, 6]
                    foreach (var woDto in importedData.WorkOrders)
                    {
                        // Check if the car for this WO is valid and exists in the DB
                        if (!context.Cars.Any(c => c.Vin == woDto.CarVin))
                        {
                            LogInvalid($"Invalid WorkOrder {woDto.Id}: Car VIN {woDto.CarVin} is invalid or was skipped. Skipped WO.", ref invalidEntries);
                            continue;
                        }

                        if (!DateTime.TryParse(woDto.ReceivedAt, out var receivedDate))
                        {
                            LogInvalid($"Invalid WorkOrder {woDto.Id}: Bad date format. Skipped WO.", ref invalidEntries);
                            continue;
                        }

                        var newWorkOrder = new WorkOrder
                        {
                            Id = woDto.Id,
                            CarVin = woDto.CarVin,
                            ReceivedAt = receivedDate,
                            Type = woDto.Type
                        };
                        context.WorkOrders.Add(newWorkOrder);

                        // D: Tasks [cite: 4, 6]
                        foreach (var taskDto in woDto.Tasks)
                        {
                            if (taskDto.LaborH < 0 || taskDto.Rate < 0 || !context.Mechanics.Any(m => m.Id == taskDto.MechanicId))
                            {
                                LogInvalid($"Invalid Task {taskDto.Id} for WO {woDto.Id}: Bad numbers or invalid mechanic. Skipped Task.", ref invalidEntries);
                                continue;
                            }
                            context.Tasks.Add(new Task
                            {
                                Id = taskDto.Id,
                                Description = taskDto.Desc, // Map DTO 'Desc'
                                LaborHours = taskDto.LaborH, // Map DTO 'LaborH'
                                Rate = taskDto.Rate,
                                MechanicId = taskDto.MechanicId,
                                WorkOrderId = newWorkOrder.Id // Link to WO
                            });
                        }

                        // E: Parts [cite: 4, 6]
                        foreach (var partDto in woDto.Parts)
                        {
                            if (partDto.Qty < 0 || partDto.UnitPrice < 0)
                            {
                                LogInvalid($"Invalid Part {partDto.Sku} for WO {woDto.Id}: Negative qty or price. Skipped Part.", ref invalidEntries);
                                continue;
                            }
                            context.Parts.Add(new Part
                            {
                                Sku = partDto.Sku,
                                Quantity = partDto.Qty, // Map DTO 'Qty'
                                UnitPrice = partDto.UnitPrice,
                                WorkOrderId = newWorkOrder.Id // Link to WO
                            });
                        }

                        // F: Invoice [cite: 7]
                        // Validate mixed-type data
                        if (woDto.Invoice == null ||
                            !(woDto.Invoice.Currency is string currency) ||
                            !(woDto.Invoice.Paid is bool isPaid))
                        {
                            LogInvalid($"Invalid Invoice for WO {woDto.Id}: Malformed currency or paid status. Skipped Invoice.", ref invalidEntries);
                            continue;
                        }
                        context.Invoices.Add(new Invoice
                        {
                            WorkOrderId = newWorkOrder.Id, // Link to WO (PK/FK)
                            Currency = currency,
                            IsPaid = isPaid
                        });
                    }
                    context.SaveChanges(); // Save Work Orders

                    // G: Diagnostics [cite: 7]
                    foreach (var obdDto in importedData.Diagnostics.Obd)
                    {
                        if (!context.Cars.Any(c => c.Vin == obdDto.CarVin)) continue; // Skip if car was invalid
                        foreach (var code in obdDto.Codes)
                        {
                            context.Diagnostics.Add(new Diagnostic
                            {
                                CarVin = obdDto.CarVin,
                                DtcCode = code.Dtc,
                                Status = code.Status
                            });
                        }
                    }

                    // H: Tests [cite: 7, 8]
                    foreach (var testDto in importedData.Diagnostics.Tests)
                    {
                        if (!context.WorkOrders.Any(w => w.Id == testDto.WorkOrderId)) continue; // Skip if WO was invalid
                        context.Tests.Add(new Test
                        {
                            WorkOrderId = testDto.WorkOrderId,
                            Name = testDto.Name,
                            IsOk = testDto.Ok // Map DTO 'Ok'
                        });
                    }

                    context.SaveChanges(); // Save Diagnostics/Tests
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"A critical error occurred during import: {ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // 5. Write error log
            File.WriteAllText(ErrorLogFileName, _errorLog.ToString());

            // 6. Refresh all UI elements
            LoadAllData();

            MessageBox.Show($"Import complete. {invalidEntries} invalid entries were skipped and logged.", "Import Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion


        #region 2. Tab 1: Work Order Methods

        private void LoadWorkOrdersGrid()
        {
            using (var context = new UMFST.MIP.CarServiceDashboard.Data.AppContext())
            {
                // Use .Include() to load related data for the TotalCost and display
                var orders = context.WorkOrders
                    .Include(wo => wo.Car)
                    .Include(wo => wo.Invoice)
                    .Include(wo => wo.Tasks)
                    .Include(wo => wo.Parts)
                    .ToList(); // Execute query

                // Use the [NotMapped] TotalCost property we defined
                dgvWorkOrders.DataSource = orders.Select(wo => new
                {
                    Id = wo.Id, // Use 'Id' (string)
                    Car = $"{wo.Car?.Make} {wo.Car?.Model}", // Null check
                    Total = wo.TotalCost, // Access the computed property
                    IsPaid = wo.Invoice?.IsPaid ?? false // Null check
                }).ToList();
            }
        }

        private void dgvWorkOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvWorkOrders.Columns.Contains("IsPaid") &&
                e.ColumnIndex == dgvWorkOrders.Columns["IsPaid"].Index && e.Value != null)
            {
                bool isPaid = (bool)e.Value;
                e.CellStyle.BackColor = isPaid ? Color.LightGreen : Color.Salmon;
                e.Value = isPaid ? "Paid" : "Unpaid";
                e.FormattingApplied = true;
            }
        }

        private void btnExportSummary_Click(object sender, EventArgs e)
        {
            var summary = new StringBuilder();
            summary.AppendLine("WORK ORDER SUMMARY - AUTOFIX CENTRAL");
            summary.AppendLine("=====================================");

            int unpaidCount = 0;

            try
            {
                using (var context = new UMFST.MIP.CarServiceDashboard.Data.AppContext())
                {
                    var orders = context.WorkOrders
                        .Include(wo => wo.Car)
                        .Include(wo => wo.Invoice)
                        .Include(wo => wo.Tasks)
                        .Include(wo => wo.Parts)
                        .ToList();

                    foreach (var wo in orders)
                    {
                        string carName = wo.Car != null ? $"{wo.Car.Make} {wo.Car.Model}" : "Unknown Car";
                        bool isPaid = wo.Invoice?.IsPaid ?? false;
                        if (!isPaid) unpaidCount++;

                        // W1 | Dacia Duster | Total: 122.5 EUR | Paid: NO
                        summary.AppendLine(
                            $"W{wo.Id} | {carName} | Total: {wo.TotalCost:F2} EUR | Paid: {(isPaid ? "YES" : "NO")}"
                        );
                    }
                }

                summary.AppendLine("-------------------------------------");
                summary.AppendLine($"Unpaid Orders: {unpaidCount}");

                // Get invalid count from log file
                int invalidCount = 0;
                if (File.Exists(ErrorLogFileName))
                {
                    // ReadAllLines handles empty file correctly (returns 0)
                    invalidCount = File.ReadAllLines(ErrorLogFileName).Length;
                }
                summary.AppendLine($"Invalid Entries Skipped: {invalidCount}");

                // Write the final file
                File.WriteAllText(ExportFileName, summary.ToString());
                MessageBox.Show($"{ExportFileName} exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not export file: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Add/Edit/Delete Stubs ---

        private void btnAddWorkOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add functionality is not required for this exam.", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEditWorkOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Edit functionality is not required for this exam.", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeleteWorkOrder_Click(object sender, EventArgs e)
        {
            if (dgvWorkOrders.CurrentRow == null)
            {
                MessageBox.Show("Please select a work order to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the ID from the selected row
            string woId = dgvWorkOrders.CurrentRow.Cells["Id"].Value.ToString();

            if (MessageBox.Show($"Are you sure you want to delete Work Order {woId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                using (var context = new UMFST.MIP.CarServiceDashboard.Data.AppContext())
                {
                    // Find the work order by its string ID
                    var orderToDelete = context.WorkOrders
                        .Include(wo => wo.Tasks)
                        .Include(wo => wo.Parts)
                        .Include(wo => wo.Invoice)
                        .Include(wo => wo.Tests)
                        .FirstOrDefault(wo => wo.Id == woId);

                    if (orderToDelete != null)
                    {
                        // EF will handle deleting all related data (cascade)
                        context.WorkOrders.Remove(orderToDelete);
                        context.SaveChanges();
                    }
                }

                // Refresh the grid
                LoadWorkOrdersGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete work order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region 3. Tab 2: Clients & Cars Methods

        private void LoadClientsList()
        {
            using (var context = new UMFST.MIP.CarServiceDashboard.Data.AppContext())
            {
                lbClients.DataSource = context.Clients.ToList();
                lbClients.DisplayMember = "Name";
                lbClients.ValueMember = "Id"; // Use 'Id' (string)
            }

            // Also populate the filter ComboBox
            using (var context = new UMFST.MIP.CarServiceDashboard.Data.AppContext())
            {
                cmbMakeFilter.DataSource = context.Cars
                    .Select(c => c.Make)
                    .Distinct()
                    .ToList();
                cmbMakeFilter.SelectedItem = null;
            }
        }

        private void lbClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When client changes, load their cars (and apply filters)
            LoadCarsGrid();
        }

        private void btnFilterCars_Click(object sender, EventArgs e)
        {
            // When filter button is clicked, reload cars for current client
            LoadCarsGrid();
        }

        /// <summary>
        /// Loads the cars grid based on selected client and filters
        /// </summary>
        private void LoadCarsGrid()
        {
            if (lbClients.SelectedItem == null)
            {
                dgvCars.DataSource = null;
                return;
            }

            string selectedClientId = (string)lbClients.SelectedValue;

            // Get filter values
            string makeFilter = cmbMakeFilter.Text;
            string yearFilter = txtYearFilter.Text;

            using (var context = new UMFST.MIP.CarServiceDashboard.Data.AppContext())
            {
                // Start with cars for the selected client
                IQueryable<Car> query = context.Cars
                    .Where(c => c.ClientId == selectedClientId);

                // Apply make filter
                if (!string.IsNullOrEmpty(makeFilter))
                {
                    query = query.Where(c => c.Make == makeFilter);
                }

                // Apply year filter
                if (int.TryParse(yearFilter, out int year))
                {
                    query = query.Where(c => c.Year == year);
                }

                dgvCars.DataSource = query.ToList();
            }
        }

        #endregion


        #region 4. Tab 3: Diagnostics Methods

        private void LoadDiagnosticsCarComboBox()
        {
            using (var context = new UMFST.MIP.CarServiceDashboard.Data.AppContext())
            {
                // Load all cars, show VIN
                cmbCarForDiagnostics.DataSource = context.Cars
                    .Select(c => new { c.Vin, Display = c.Vin + " (" + c.Make + " " + c.Model + ")" })
                    .ToList();
                cmbCarForDiagnostics.DisplayMember = "Display";
                cmbCarForDiagnostics.ValueMember = "Vin";
                cmbCarForDiagnostics.SelectedItem = null;
            }
        }

        private void cmbCarForDiagnostics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCarForDiagnostics.SelectedValue == null)
            {
                dgvObdCodes.DataSource = null;
                dgvTestResults.DataSource = null;
                return;
            }

            string selectedVin = (string)cmbCarForDiagnostics.SelectedValue;

            using (var context = new UMFST.MIP.CarServiceDashboard.Data.AppContext())
            {
                // Load OBD Codes
                dgvObdCodes.DataSource = context.Diagnostics
                    .Where(d => d.CarVin == selectedVin)
                    .Select(d => new { d.DtcCode, d.Status })
                    .ToList();

                // Load Test Results
                // 1. Find WOs for this car
                // 2. Find Tests for those WOs
                dgvTestResults.DataSource = context.Tests
                    .Where(t => t.WorkOrder.CarVin == selectedVin)
                    .Select(t => new { t.WorkOrderId, t.Name, t.IsOk })
                    .ToList();
            }
        }

        // Highlight failed tests (Req B.3)
        private void dgvTestResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTestResults.Columns.Contains("IsOk") &&
                e.ColumnIndex == dgvTestResults.Columns["IsOk"].Index && e.Value != null)
            {
                bool isOk = (bool)e.Value;
                if (!isOk)
                {
                    // Highlight the entire row
                    e.CellStyle.BackColor = Color.Salmon;
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        #endregion


        #region 5. Validation Helper Methods (Req 4)

        private bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            // Simple regex for email
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool IsVinValid(string vin)
        {
            if (string.IsNullOrEmpty(vin)) return false;
            // Basic VIN check: 17 chars, no I, O, Q
            return Regex.IsMatch(vin, @"^[A-HJ-NPR-Z0-9]{17}$");
        }

        #endregion
    }
}