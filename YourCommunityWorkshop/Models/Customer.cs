using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YourCommunityWorkshop.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [DisplayName("Full Name")]
        [Required(ErrorMessage = "You need to give a name.")]
        public string CustomerName { get; set; }

        [DisplayName("Contact Number")]
        [Required(ErrorMessage = "You need to give a contact number.")]
        public string CustomerPhone { get; set; }
    }
}