using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using YourCommunityWorkshop.DAL;
using YourCommunityWorkshop.Models;
using YourCommunityWorkshop.ViewModels;

namespace YourCommunityWorkshop.Controllers
{
    public class ToolController : Controller
    {
        private ToolContext db = new ToolContext();

        #region Local Controller
        //    // GET: Tool
        //    public ActionResult Index()
        //    {
        //        return View(db.Tools.ToList());
        //    }

        //    #region Edit
        //    // GET: Edit
        //    public ActionResult Edit(int Id)
        //    {
        //        var tool = db.Tools.Single(t => t.ToolId == Id);

        //        return View(tool);
        //    }

        //    // POST: Edit
        //    [HttpPost]
        //    public ActionResult Edit(int Id, FormCollection collection)
        //    {
        //        try
        //        {
        //            var tool = db.Tools.Single(t => t.ToolId == Id);
        //            if (TryUpdateModel(tool))
        //            {
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }

        //            return View(tool);
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //    #endregion

        //    #region Create
        //    // GET: Create
        //    public ActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: Create
        //    [HttpPost]
        //    public ActionResult Create(FormCollection collection)
        //    {
        //        try
        //        {
        //            Tool tool = new Tool();

        //            tool.ToolName = collection["ToolName"];
        //            tool.BrandName = collection["BrandName"];                            
        //            tool.Active = Convert.ToBoolean(collection["Active"]);              // Doesn't create a new tool if this is checked
        //            tool.Availability = Convert.ToBoolean(collection["Availability"]); // Can edit the tool, set it to checked then save and it saves as checked
        //            tool.ToolCondition = collection["ToolCondition"];

        //            db.Tools.Add(tool);
        //            db.SaveChanges();

        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //    #endregion

        //    #region Details
        //    // GET: details
        //    public ActionResult Details(int Id)
        //    {
        //        var tool = db.Tools.Single(t => t.ToolId == Id);

        //        return View(tool);
        //    }
        //    #endregion

        //    #region Delete
        //    // GET: Delete
        //    public ActionResult Delete(int Id)
        //    {
        //        var tool = db.Tools.Single(t => t.ToolId == Id);

        //        return View(tool);
        //    }

        //    // POST: Delete
        //    [HttpPost]
        //    public ActionResult Delete(int Id, FormCollection collection)
        //    {
        //        try
        //        {
        //            var tool = db.Tools.Single(t => t.ToolId == Id);
        //            db.Tools.Remove(tool);
        //            db.SaveChanges();

        //            return RedirectToAction("Index");
        //        }
        //        catch 
        //        {
        //            return View();
        //        }
        //    }
        //    #endregion
        //}
        #endregion

        public static List<Tool> ToolList = new List<Tool>
        {
            new Tool{ToolId = 1, ToolName = "The Avengers"},
            new Tool{ToolId = 2, ToolName = "Star Wars"},
            new Tool{ToolId = 3, ToolName = "The Matrix"},
        };

        #region Reports 


        #region Exports

        [HttpPost]
        public FileResult ExportCheckOutTools()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
            IEnumerable<Tool> Tools = response.Content.ReadAsAsync<IEnumerable<Tool>>().Result;

            var tools = Tools.Where(c => c.Availability == false);

            string[] columnNames = new string[] { "Tool Name", "Brand Name", "Active", "Availability", "Comments" };

            //Build the csv file data as a comma seperated string
            string csv = string.Empty;

            foreach (string columnName in columnNames)
            {
                //Add the header row for CSV file
                csv += columnName + ',';
            }

            //Add new line
            csv += "\r\n";

            foreach (var tool in tools)
            {
                //Add the Data rows
                csv += tool.ToolName.Replace(",", ";") + ',';
                csv += tool.BrandName.Replace(",", ";") + ',';
                csv += tool.Active.ToString().Replace(",", ";") + ',';
                csv += tool.Availability.ToString().Replace(",", ";") + ',';
                csv += tool.ToolCondition.Replace(",", ";") + ',';

                csv += "\r\n";
            }

            //Download the csv file
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "Report " + DateTime.Now.ToString() + ".csv");
        }

        [HttpPost]
        public FileResult ExportActiveTools()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
            IEnumerable<Tool> Tools = response.Content.ReadAsAsync<IEnumerable<Tool>>().Result;

            var tools = Tools.Where(c => c.Active == false);

            string[] columnNames = new string[] { "Tool Name", "Brand Name", "Active", "Availability", "Comments" };

            string csv = string.Empty;

            foreach (string columnName in columnNames)
            {
                csv += columnName + ',';
            }

            csv += "\r\n";

            foreach (var tool in tools)
            {
                csv += tool.ToolName.Replace(",", ";") + ',';
                csv += tool.BrandName.Replace(",", ";") + ',';
                csv += tool.Active.ToString().Replace(",", ";") + ',';
                csv += tool.Availability.ToString().Replace(",", ";") + ',';
                csv += tool.ToolCondition.Replace(",", ";") + ',';

                csv += "\r\n";
            }

            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "Report " + DateTime.Now.ToString() + ".csv");
        }

        [HttpPost]
        public FileResult ExportActiveByBrandTools()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
            IEnumerable<Tool> Tools = response.Content.ReadAsAsync<IEnumerable<Tool>>().Result;

            var tools = Tools.Where(c => c.Active == false).OrderBy(o => o.BrandName);

            string[] columnNames = new string[] { "Tool Name", "Brand Name", "Active", "Availability", "Comments" };

            string csv = string.Empty;

            foreach (string columnName in columnNames)
            {
                csv += columnName + ',';
            }

            csv += "\r\n";

            foreach (var tool in tools)
            {
                csv += tool.ToolName.Replace(",", ";") + ',';
                csv += tool.BrandName.Replace(",", ";") + ',';
                csv += tool.Active.ToString().Replace(",", ";") + ',';
                csv += tool.Availability.ToString().Replace(",", ";") + ',';
                csv += tool.ToolCondition.Replace(",", ";") + ',';

                csv += "\r\n";
            }

            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "Report " + DateTime.Now.ToString() + ".csv");
        }

        [HttpPost]
        public FileResult ExportRetiredTools()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
            IEnumerable<Tool> Tools = response.Content.ReadAsAsync<IEnumerable<Tool>>().Result;

            var tools = Tools.Where(c => c.Active == true);

            string[] columnNames = new string[] { "Tool Name", "Brand Name", "Active", "Availability", "Comments" };

            string csv = string.Empty;

            foreach (string columnName in columnNames)
            {
                csv += columnName + ',';
            }

            csv += "\r\n";

            foreach (var tool in tools)
            {
                csv += tool.ToolName.Replace(",", ";") + ',';
                csv += tool.BrandName.Replace(",", ";") + ',';
                csv += tool.Active.ToString().Replace(",", ";") + ',';
                csv += tool.Availability.ToString().Replace(",", ";") + ',';
                csv += tool.ToolCondition.Replace(",", ";") + ',';

                csv += "\r\n";
            }

            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "Report " + DateTime.Now.ToString() + ".csv");
        }

        [HttpPost]
        public FileResult ExportRetiredByBrandTools()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
            IEnumerable<Tool> Tools = response.Content.ReadAsAsync<IEnumerable<Tool>>().Result;

            var tools = Tools.Where(c => c.Active == true).OrderBy(o => o.BrandName);

            string[] columnNames = new string[] { "Tool Name", "Brand Name", "Active", "Availability", "Comments" };

            string csv = string.Empty;

            foreach (string columnName in columnNames)
            {
                csv += columnName + ',';
            }

            csv += "\r\n";

            foreach (var tool in tools)
            {
                csv += tool.ToolName.Replace(",", ";") + ',';
                csv += tool.BrandName.Replace(",", ";") + ',';
                csv += tool.Active.ToString().Replace(",", ";") + ',';
                csv += tool.Availability.ToString().Replace(",", ";") + ',';
                csv += tool.ToolCondition.Replace(",", ";") + ',';

                csv += "\r\n";
            }

            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "Report " + DateTime.Now.ToString() + ".csv");
        }

        #endregion

        public ActionResult Report()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;

            return View();
        }

        public ActionResult ReportCheckOutTools()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
            IEnumerable<Tool> Tools = response.Content.ReadAsAsync<IEnumerable<Tool>>().Result;

            return View(Tools.Where(c => c.Availability == false));
        }

        public ActionResult ReportActiveTools()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
            IEnumerable<Tool> Tools = response.Content.ReadAsAsync<IEnumerable<Tool>>().Result;

            return View(Tools.Where(c => c.Active == false));
        }

        public ActionResult ReportActiveByBrandTools()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
            IEnumerable<Tool> Tools = response.Content.ReadAsAsync<IEnumerable<Tool>>().Result;

            return View(Tools.Where(c => c.Active == false).OrderBy(o => o.BrandName));
        }

        public ActionResult ReportRetiredTools()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
            IEnumerable<Tool> Tools = response.Content.ReadAsAsync<IEnumerable<Tool>>().Result;

            return View(Tools.Where(c => c.Active == true));
        }

        public ActionResult ReportRetiredByBrandTools()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
            IEnumerable<Tool> Tools = response.Content.ReadAsAsync<IEnumerable<Tool>>().Result;

            return View(Tools.Where(c => c.Active == true).OrderBy(o => o.BrandName));
        }

        [HttpPost]
        public FileResult ExportToolHistory(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Tools/{Id}").Result;
            var tool = response.Content.ReadAsAsync<Tool>().Result;

            response = WebClient.ApiClient.GetAsync("Rentals").Result;

            IEnumerable<Rental> rentals = response.Content.ReadAsAsync<IEnumerable<Rental>>().Result;

            response = WebClient.ApiClient.GetAsync("Customers").Result;
            IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;

            response = WebClient.ApiClient.GetAsync("RentalTools").Result;
            IEnumerable<RentalTool> rentalTools = response.Content.ReadAsAsync<IEnumerable<RentalTool>>().Result;

            var toolHistory = rentals.Select(
                r => new ToolRentalDetailsViewModel
                {
                    RentalId = r.RentalId,
                    DateRented = r.DateRented,
                    CustomerName = customers.Where(c => c.CustomerId == r.CustomerId).Select(u => u.CustomerName).FirstOrDefault(),
                    ToolId = r.RentalTools.Select(y => y.ToolId).FirstOrDefault(),
                }).OrderByDescending(o => o.DateRented).ToList();

            var toolHistoryList = toolHistory.Where(x => x.ToolId == tool.ToolId);

            string[] columnNames = new string[] { "RentalId", "Date Rented", "Customer Name" }; //, "Tool Id" };

            //Build the csv file data as a comma seperated string
            string csv = string.Empty;

            foreach (string columnName in columnNames)
            {
                //Add the header row for CSV file
                csv += columnName + ',';
            }

            //Add new line
            csv += "\r\n";

            foreach (var tHistory in toolHistoryList)
            {
                //Add the Data rows
                csv += tHistory.RentalId.ToString().Replace(",", ";") + ',';
                csv += tHistory.DateRented.ToString().Replace(",", ";") + ',';
                csv += tHistory.CustomerName.Replace(",", ";") + ',';
                //csv += tHistory.ToolId.ToString().Replace(",", ";") + ',';

                csv += "\r\n";
            }

            //Download the csv file
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "Report " + DateTime.Now.ToString() + ".csv");
        }

        public ActionResult ToolHistory(int Id)
        {
            // The history of rentals for a specific tool
            // Get the specific tool
            // Display all the rentals with that specific tool

            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Tools/{Id}").Result;
            var tool = response.Content.ReadAsAsync<Tool>().Result;

            response = WebClient.ApiClient.GetAsync("Rentals").Result;

            IEnumerable<Rental> rentals = response.Content.ReadAsAsync<IEnumerable<Rental>>().Result;

            response = WebClient.ApiClient.GetAsync("Customers").Result;
            IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;

            response = WebClient.ApiClient.GetAsync("RentalTools").Result;
            IEnumerable<RentalTool> rentalTools = response.Content.ReadAsAsync<IEnumerable<RentalTool>>().Result;

            var toolHistory = rentals.Select(
                r => new ToolRentalDetailsViewModel
                {
                    RentalId = r.RentalId,
                    DateRented = r.DateRented,
                    DateReturn = r.DateReturn.ToString(),
                    CustomerName = customers.Where(c => c.CustomerId == r.CustomerId).Select(u => u.CustomerName).FirstOrDefault(),
                    ToolName = tool.ToolName,
                    ToolId = r.RentalTools.Select(y => y.ToolId).FirstOrDefault(),
                }).OrderByDescending(o => o.DateRented).ToList();

            toolHistory.Where(d => d.DateReturn.ToString() == "").ToList().ForEach(y => y.DateReturn = "Not Returned");

            if (toolHistory.Any(c => c.ToolId == tool.ToolId))
            {
                return View(toolHistory.Where(x => x.ToolId == tool.ToolId));
            }
            else
            {
                TempData["SuccessMessage"] = "No history for " + tool.BrandName + " " + tool.ToolName + ".";
                return RedirectToAction("Index");
            }
            
        }

        #endregion

        public ActionResult Index()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
          
            // we are using IEnumerable because we only want to enumerate the collection and we are not going to add or delete elements
            IEnumerable<Tool> Tools = response.Content.ReadAsAsync<IEnumerable<Tool>>().Result;

            return View(Tools);
        }

        public ActionResult Edit(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Tools/{Id}").Result;
            var Tool = response.Content.ReadAsAsync<Tool>().Result;
            return View(Tool);
        }

        [HttpPost]
        public ActionResult Edit(int Id, Tool Tool)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Tools/{Id}", Tool).Result;
                //we will refer to this in the Index.cshtml of the Tool so alertify can display the message.
                TempData["SuccessMessage"] = "Saved successfully.";

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                return View(Tool);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Tools/{Id}").Result;
            var Tool = response.Content.ReadAsAsync<Tool>().Result;
            return View(Tool);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tool Tool)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Tools", Tool).Result;
                //we will refer to this in the Index.cshtml of the Tool so alertify can display the message.
                TempData["SuccessMessage"] = "Tool added successfully.";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult DisplayTool()
        {
            var Tool = new Tool() { ToolName = "The Avengers" };

            return View(Tool);
        }

        public ActionResult Delete(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Tools/{Id}").Result;
            var Tool = response.Content.ReadAsAsync<Tool>().Result;
            return View(Tool);
        }

        [HttpPost]
        public ActionResult Delete(int Id, FormCollection collection)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Tools/{Id}").Result;
                //we will refer to this in the Index.cshtml of the Tool so alertify can display the message.
                TempData["SuccessMessage"] = "Tool deleted successfully.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}