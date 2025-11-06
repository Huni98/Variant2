using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMFST.MIP.CarServiceDashboard
{
    public class Part
    {
        // We need an auto-incrementing key because SKU is not unique
        // across different work orders.
        [Key]
        public int PartEntryId { get; set; }

        public string Sku { get; set; }
        public string Name { get; set; }

        [Column("Quantity")] // Map 'qty' from JSON
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; } // Use decimal for money

        // Foreign Key to WorkOrder
        [ForeignKey("WorkOrder")]
        public string WorkOrderId { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
