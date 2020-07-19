using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourCommunityWorkshop.ViewModels;

namespace YourCommunityWorkshop.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturn { get; set; }
        public virtual ICollection<RentalTool> RentalTools { get; set; }
        public IEnumerable<SelectListItem> Customers { get; set; }
        public IEnumerable<CustomerToolsViewModel> RentedTools { get; set; }
    }
}