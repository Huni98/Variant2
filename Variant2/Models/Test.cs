using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMFST.MIP.CarServiceDashboard.Models

{
    public class Test
    {
        [Key]
        public int TestId { get; set; } // Auto-incrementing key

        public string Name { get; set; }

        [Column("IsOk")] // From 'ok' in JSON
        public bool IsOk { get; set; }

        // Foreign Key to the WorkOrder
        [ForeignKey("WorkOrder")]
        public string WorkOrderId { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
