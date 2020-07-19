using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using YourCommunityWorkshop.Models;

namespace YourCommunityWorkshop.DAL
{
    public class ToolContext : DbContext
    {
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalTool> RentalTools { get; set; }

        public ToolContext() : base("ToolRental")
        {
            Database.SetInitializer(new ToolInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}