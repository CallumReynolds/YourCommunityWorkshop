using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YourCommunityWorkshop.Models
{
    public class RentalTool
    {
        public int RentalToolId { get; set; }
        public int RentalId { get; set; }
        public int ToolId { get; set; }
        public IEnumerable<SelectListItem> Tools { get; set; }
    }
}