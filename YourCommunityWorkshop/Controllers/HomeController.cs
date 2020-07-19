using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YourCommunityWorkshop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Community Workshop Information and Help Page";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "YourProject Contact Page.";

            return View();
        }

        public ActionResult ToolsHelp()
        {
            return View();
        }

        public ActionResult CustomersHelp()
        {
            return View();
        }

        public ActionResult RentalsHelp()
        {
            return View();
        }
    }
}
