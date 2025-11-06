using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UMFST.MIP.CarServiceDashboard
{
    public class Car
    {
        [Key] // The VIN is the natural Primary Key
        public string Vin { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        // We use 'double' as specified in the exam, from 'odometerKm'
        public double Odometer { get; set; }

        // Foreign Key relationship to the Client
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public virtual Client Client { get; set; }

        // Navigation properties
        public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
        public virtual ICollection<Diagnostic> Diagnostics { get; set; } = new List<Diagnostic>();
    }
}
