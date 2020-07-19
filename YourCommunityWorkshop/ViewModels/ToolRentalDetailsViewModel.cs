using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using YourCommunityWorkshop.Models;

namespace YourCommunityWorkshop.ViewModels
{
    public class ToolRentalDetailsViewModel
    {
        [DisplayName("Rental Id")]
        public int RentalId { get; set; }

        [DisplayName("Date Rented")]
        public DateTime DateRented { get; set; }

        [DisplayName("Date Returned")]
        public string DateReturn { get; set;}

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        public string ToolName { get; set; }

        [DisplayName("Asset Number")]
        public int ToolId { get; set; }

        public RentalTool rentalTool { get; set; }

        public Rental Rental { get; set; }

        public List<CustomerToolsViewModel> RentedTools { get; set; }
    }
}