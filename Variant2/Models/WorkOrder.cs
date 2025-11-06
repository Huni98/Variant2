using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace UMFST.MIP.CarServiceDashboard.Models
{
    public class WorkOrder
    {
        [Key] // Primary Key ("W1", "W2")
        public string Id { get; set; }

        public DateTime ReceivedAt { get; set; } // You must parse this from the string
        public string Type { get; set; }

        // Foreign Key to Car
        [ForeignKey("Car")]
        public string CarVin { get; set; }
        public virtual Car Car { get; set; }

        // Navigation properties
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
        public virtual ICollection<Part> Parts { get; set; } = new List<Part>();
        public virtual ICollection<Test> Tests { get; set; } = new List<Test>();

        // One-to-one relationship with Invoice
        public virtual Invoice Invoice { get; set; }

        // This is the "method" (computed property) you need (Req B.1)
        [NotMapped] // Tells EF not to create this column in the DB
        public decimal TotalCost
        {
            get
            {
                // Ensure Tasks and Parts are not null before summing
                decimal tasksCost = Tasks?.Sum(t => (decimal)t.LaborHours * t.Rate) ?? 0;
                decimal partsCost = Parts?.Sum(p => p.Quantity * p.UnitPrice) ?? 0;
                return tasksCost + partsCost;
            }
        }
    }
}
