using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMFST.MIP.CarServiceDashboard.Models
{
    public class Task
    {
        [Key] // Primary Key ("T-1", "T-3")
        public string Id { get; set; }

        [Column("Description")] // Map 'desc' from JSON to 'Description'
        public string Description { get; set; }

        [Column("LaborHours")] // Map 'laborH' from JSON
        public double LaborHours { get; set; }

        public decimal Rate { get; set; } // Use decimal for money

        // Foreign Key to WorkOrder
        [ForeignKey("WorkOrder")]
        public string WorkOrderId { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        // Foreign Key to Mechanic
        [ForeignKey("Mechanic")]
        public string MechanicId { get; set; }
        public virtual Mechanic Mechanic { get; set; }
    }
}
