using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMFST.MIP.CarServiceDashboard
{
    public class Client
    {
        [Key] // Primary Key ("CL-993")
        public string Id { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Navigation property: A client can own multiple cars
        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
