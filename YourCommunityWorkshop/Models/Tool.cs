using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace YourCommunityWorkshop.Models
{
    public class Tool
    {
        [DisplayName("Asset Number")]
        public int ToolId { get; set; }

        [DisplayName("Type of Tool")]
        public string ToolName { get; set; }

        [DisplayName("Brand")]
        public string BrandName { get; set; }

        [DisplayName("Is Retired")]
        public bool Active { get; set; }

        [DisplayName("Is Available")]
        public bool Availability { get; set; }

        [DisplayName("Comments")]
        public string ToolCondition { get; set; }
    }
}