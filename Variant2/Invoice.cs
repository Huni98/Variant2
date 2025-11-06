using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMFST.MIP.CarServiceDashboard
{
    public class Invoice
    {
        // For a one-to-one relationship, the Primary Key
        // can also be the Foreign Key.
        [Key]
        [ForeignKey("WorkOrder")]
        public string WorkOrderId { get; set; }

        public string Currency { get; set; }
        public bool IsPaid { get; set; }

        // Navigation property back to the WorkOrder
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
