using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace UMFST.MIP.CarServiceDashboard.Services.DTOs
{
    // Root object
    public class RootDto
    {
        public GarageDto Garage { get; set; }
        public List<ClientDto> Clients { get; set; }
        public List<WorkOrderDto> WorkOrders { get; set; }
        public DiagnosticsDto Diagnostics { get; set; }
    }

    // --- Garage ---
    public class GarageDto
    {
        public string Name { get; set; }
        public List<MechanicDto> Mechanics { get; set; }
    }

    public class MechanicDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Specialization { get; set; }
        public int Years { get; set; }
    }

    // --- Clients & Cars ---
    public class ClientDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<CarDto> Cars { get; set; }
    }

    public class CarDto
    {
        public string Vin { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double OdometerKm { get; set; }
    }

    // --- Work Orders ---
    public class WorkOrderDto
    {
        public string Id { get; set; }
        public string CarVin { get; set; }
        public string ReceivedAt { get; set; }
        public string Type { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<PartDto> Parts { get; set; }
        public InvoiceDto Invoice { get; set; }
    }

    public class TaskDto
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public double LaborH { get; set; }
        public decimal Rate { get; set; }
        public string MechanicId { get; set; }
    }

    public class PartDto
    {
        public string Sku { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class InvoiceDto
    {
        // Use 'object' to handle invalid data
        public object Currency { get; set; }
        public object Paid { get; set; }
    }

    // --- Diagnostics ---
    public class DiagnosticsDto
    {
        public List<ObdDto> Obd { get; set; }
        public List<TestDto> Tests { get; set; }
    }

    public class ObdDto
    {
        public string CarVin { get; set; }
        public List<CodeDto> Codes { get; set; }
    }

    public class CodeDto
    {
        public string Dtc { get; set; }
        public string Status { get; set; }
    }

    public class TestDto
    {
        public string WorkOrderId { get; set; }
        public string Name { get; set; }
        public bool Ok { get; set; }
    }
}
