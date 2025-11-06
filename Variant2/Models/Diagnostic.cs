using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMFST.MIP.CarServiceDashboard.Models
{
    // Represents a single OBD code (DTC)
    public class Diagnostic
    {
        [Key]
        public int DiagnosticId { get; set; } // Auto-incrementing key

        [Column("DtcCode")] // From 'dtc' in JSON
        public string DtcCode { get; set; }

        public string Status { get; set; }

        // Foreign Key to the Car
        [ForeignKey("Car")]
        public string CarVin { get; set; }
        public virtual Car Car { get; set; }
    }
}
