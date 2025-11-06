using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMFST.MIP.CarServiceDashboard.Models;

namespace UMFST.MIP.CarServiceDashboard.Data

{
    public class AppContext : DbContext
    {
        
        public AppContext() : base("name=CarServiceDb") { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<UMFST.MIP.CarServiceDashboard.Models.Task> Tasks { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Diagnostic> Diagnostics { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}
