using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMFST.MIP.CarServiceDashboard
{
    public class AppContext : DbContext
    {
        // Connection string points to a local SQLite file
        public AppContext() : base("name=CarServiceDb") { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Diagnostic> Diagnostics { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}
