using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace YourCommunityWorkshop.ViewModels
{
    public class CustomerRentalsViewModel
    {
        [DisplayName("Rental Id")]
        public int RentalId { get; set; }

        [DisplayName("Date Rented")]
        public DateTime DateRented { get; set; }

        [DisplayName("Date Returned")]
        public string DateReturn { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Tool Name")]
        public string ToolName { get; set; }
    }
}