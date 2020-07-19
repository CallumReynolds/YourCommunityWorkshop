using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using YourCommunityWorkshop.Models;

namespace YourCommunityWorkshop.ViewModels
{
    public class CustomerHistoryRentalsViewModel
    {
        [DisplayName("Rental Id")]
        public int RentalId { get; set; }

        [DisplayName("Tool Id")]
        public int ToolId { get; set; }

        [DisplayName("Date Rented")]
        public DateTime DateRented { get; set; }

        [DisplayName("Tool Name")]
        public string ToolName { get; set; }

        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }
    }
}