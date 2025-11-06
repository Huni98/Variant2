using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMFST.MIP.CarServiceDashboard
{
    public class Mechanic
    {
        [Key] // This will be the Primary Key ("M-1", "M-2")
        public string Id { get; set; }

        public string Name { get; set; }
        public int Years { get; set; }

        // The specialization array is stored as a single string
        // You will join the array during import (e.g., "Engine,OBD")
        public string Specializations { get; set; }

        // Navigation property: A mechanic can be assigned to many tasks
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
