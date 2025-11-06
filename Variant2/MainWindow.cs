using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using UMFST.MIP.CarServiceDashboard;



namespace Variant2
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnResetImport_Click(object sender, EventArgs e)
        {
            // Use a StringBuilder to log errors (Req 4)
            var errorLog = new StringBuilder();
            int invalidEntries = 0;

            // 1. Reset Database
            using (var context = new AppContext())
            {
                // This is a simple way to clear and recreate the DB
                context.Database.Delete();
                context.Database.Create();
            }

            // 2. Read local JSON file
            string jsonPath = "data_car_service.json"; // Assumes file is in bin/Debug
            if (!File.Exists(jsonPath))
            {
                MessageBox.Show("Error: data_car_service.json not found.", "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string jsonData = File.ReadAllText(jsonPath);

            // 3. Deserialize
            RootData importedData = JsonConvert.DeserializeObject<RootData>(jsonData);

            // 4. Validate and Import
            using (var context = new AppContext())
            {
                // Add Mechanics (assuming they are simple and valid)
                context.Mechanics.AddRange(importedData.Mechanics);

                // Add Clients (with nested cars)
                foreach (var client in importedData.Clients)
                {
                    if (!IsEmailValid(client.Email) || string.IsNullOrEmpty(client.Name))
                    {
                        errorLog.AppendLine($"Invalid Client: {client.Name} (Email: {client.Email}). Skipped.");
                        invalidEntries++;
                        continue; // Skip this client and their cars
                    }

                    var validClient = new Client
                    {
                        Name = client.Name,
                        Email = client.Email,
                        Phone = client.Phone
                    };

                    foreach (var car in client.Cars)
                    {
                        if (!IsVinValid(car.Vin) || car.Odometer < 0)
                        {
                            errorLog.AppendLine($"Invalid Car: {car.Vin} (Odometer: {car.Odometer}). Skipped.");
                            invalidEntries++;
                            continue; // Skip this car
                        }
                        // Add valid car to the valid client
                        validClient.Cars.Add(car);
                    }
                    context.Clients.Add(validClient);
                }

                // ... You MUST do the same validation loops for WorkOrders, Diagnostics, etc.
                // Example for WorkOrder tasks:
                foreach (var wo in importedData.WorkOrders)
                {
                    bool tasksValid = true;
                    foreach (var task in wo.Tasks)
                    {
                        if (task.LaborHours < 0 || task.Rate < 0)
                        {
                            errorLog.AppendLine($"Invalid Task in WO for Car {wo.CarId}: Hours={task.LaborHours}. Skipped task.");
                            invalidEntries++;
                            tasksValid = false;
                            // Here you might just remove the task, not the whole WO
                        }
                    }
                    // Add the WorkOrder (if valid)
                    // context.WorkOrders.Add(wo);
                }

                // 5. Save to DB
                context.SaveChanges();
            }

            // 6. Write error log
            File.WriteAllText("invalid_car_service.txt", errorLog.ToString());

            MessageBox.Show($"Import complete. {invalidEntries} invalid entries were skipped and logged.", "Import Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 7. Refresh all grids
            LoadWorkOrdersGrid();
            LoadClientsList();
        }

        // Validation Helper Methods (Req 4)
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

        private void LoadWorkOrdersGrid()
        {
            using (var context = new AppContext())
            {
                // Use .Include() to load related data for the TotalCost and display
                var orders = context.WorkOrders
                    .Include("Car")
                    .Include("Invoice")
                    .Include("Tasks")
                    .Include("Parts")
                    .ToList(); // Execute query

                // Use the [NotMapped] TotalCost property we defined
                dgvWorkOrders.DataSource = orders.Select(wo => new
                {
                    Id = wo.WorkOrderId,
                    Car = $"{wo.Car?.Make} {wo.Car?.Model}", // Null check
                    Status = wo.Status,
                    Total = wo.TotalCost, // Access the computed property
                    IsPaid = wo.Invoice?.IsPaid ?? false // Null check
                }).ToList();
            }
        }

        // Handle cell coloring (Req B.1)
        private void dgvWorkOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvWorkOrders.Columns["IsPaid"].Index && e.Value != null)
            {
                bool isPaid = (bool)e.Value;
                if (isPaid)
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                    e.Value = "Paid";
                }
                else
                {
                    e.CellStyle.BackColor = Color.Salmon;
                    e.Value = "Unpaid";
                }
                e.FormattingApplied = true;
            }
        }

        private void LoadClientsList()
        {
            using (var context = new AppContext())
            {
                lbClients.DataSource = context.Clients.ToList();
                lbClients.DisplayMember = "Name";
                lbClients.ValueMember = "ClientId";
            }
        }

        private void lbClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbClients.SelectedItem == null) return;

            var selectedClient = (Client)lbClients.SelectedItem;

            using (var context = new AppContext())
            {
                // Load cars only for the selected client
                dgvCars.DataSource = context.Cars
                    .Where(c => c.ClientId == selectedClient.ClientId)
                    .ToList();
            }
        }

        private void btnExportSummary_Click(object sender, EventArgs e)
        {
            var summary = new StringBuilder();
            summary.AppendLine("WORK ORDER SUMMARY - AUTOFIX CENTRAL");
            summary.AppendLine("=====================================");

            int unpaidCount = 0;

            using (var context = new AppContext())
            {
                // Must .Include() all data needed for the report
                var orders = context.WorkOrders
                    .Include("Car")
                    .Include("Invoice")
                    .Include("Tasks")
                    .Include("Parts")
                    .ToList();

                foreach (var wo in orders)
                {
                    string carName = wo.Car != null ? $"{wo.Car.Make} {wo.Car.Model}" : "Unknown Car";
                    bool isPaid = wo.Invoice?.IsPaid ?? false;
                    if (!isPaid) unpaidCount++;

                    // W1 | Dacia Duster | Total: 122.5 EUR | Paid: NO
                    summary.AppendLine(
                        $"W{wo.WorkOrderId} | {carName} | Total: {wo.TotalCost:F2} EUR | Paid: {(isPaid ? "YES" : "NO")}"
                    );
                }
            }

            summary.AppendLine("-------------------------------------");
            summary.AppendLine($"Unpaid Orders: {unpaidCount}");

            // Get invalid count from log file
            int invalidCount = 0;
            if (File.Exists("invalid_car_service.txt"))
            {
                invalidCount = File.ReadAllLines("invalid_car_service.txt").Length;
            }
            summary.AppendLine($"Invalid Entries Skipped: {invalidCount}");

            // Write the final file
            File.WriteAllText("workorder_summary.txt", summary.ToString());
            MessageBox.Show("workorder_summary.txt exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
