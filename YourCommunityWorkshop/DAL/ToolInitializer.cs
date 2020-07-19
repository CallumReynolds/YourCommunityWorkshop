using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using YourCommunityWorkshop.Models;

namespace YourCommunityWorkshop.DAL
{
    public class ToolInitializer : DropCreateDatabaseAlways<ToolContext>
    {
        protected override void Seed(ToolContext context)
        {
            var tools = new List<Tool>
            {
                new Tool{ToolId = 1, ToolName = "Hammer", BrandName = "KFC", Active = true, Availability = true, ToolCondition = "Top shit"}
            };

            tools.ForEach(t => context.Tools.Add(t));
            context.SaveChanges();

            var customers = new List<Customer>
            {
                new Customer{CustomerId = 1, CustomerName = "Markus Kruber", CustomerPhone = "0491 570 156"}
            };

            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();

            var rentals = new List<Rental>
            {
                new Rental{RentalId = 1, CustomerId = 1, DateRented = DateTime.Parse("01/01/2017"), DateReturn = null}
            };

            rentals.ForEach(r => context.Rentals.Add(r));
            context.SaveChanges();

            var rentalTools = new List<RentalTool>
            {
                new RentalTool{RentalToolId = 1, RentalId = 1, ToolId = 1}
            };

            rentalTools.ForEach(rt => context.RentalTools.Add(rt));
            context.SaveChanges();
        }
    }
}