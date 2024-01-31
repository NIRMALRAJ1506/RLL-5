using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext() : base("Insurance")
        {
            Database.SetInitializer<InsuranceDbContext>(null);
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Questions> Questions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure primary key for Questions entity


            // Other configurations...

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<AppliedPolicy> AppliedPolicies { get; set; }

        public DbSet<Answer> Answers { get; set; }

    }
}
