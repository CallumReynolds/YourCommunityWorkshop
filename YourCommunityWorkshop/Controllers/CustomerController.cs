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
    public class CustomerController : Controller
    {
        // Database instantiation
        private ToolContext db = new ToolContext();

        //// GET: Customer
        //public ActionResult Index()
        //{
        //    return View(db.Customers.ToList());
        //}


        //#region Edit 
        //// GET: Edit
        //public ActionResult Edit(int Id)
        //{
        //    var customer = db.Customers.Single(c => c.CustomerId == Id);

        //    return View(customer);
        //}

        //// POST : Edit
        //[HttpPost]
        //public ActionResult Edit(int Id, FormCollection collection)
        //{
        //    try
        //    {
        //        var customer = db.Customers.Single(c => c.CustomerId == Id);

        //        if (TryUpdateModel(customer))
        //        {
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        return View(customer);
        //    }
        //    catch
        //    {
        //        return View();                
        //    }
        //}
        //#endregion


        //#region Create
        //// GET: Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        Customer customer = new Customer();

        //        customer.CustomerName = collection["CustomerName"];
        //        customer.CustomerPhone = collection["CustomerPhone"];
        //        db.Customers.Add(customer);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //#endregion


        //#region Details
        //// GET: Details
        //public ActionResult Details(int Id)
        //{
        //    var customer = db.Customers.Single(c => c.CustomerId == Id);

        //    return View(customer);
        //}
        //#endregion


        //#region Delete
        //// GET: Delete
        //public ActionResult Delete(int Id)
        //{
        //    var customer = db.Customers.Single(c => c.CustomerId == Id);

        //    return View(customer);
        //}

        //// POST: Delete
        //[HttpPost]
        //public ActionResult Delete(int Id, FormCollection collection)
        //{
        //    try
        //    {
        //        var customer = db.Customers.Single(c => c.CustomerId == Id);
        //        db.Customers.Remove(customer);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //#endregion

        #region Customer History and Export

        // Exports the History of a customer
        [HttpPost]
        public FileResult ExportCustomerHistory(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customers/{Id}").Result;
            var customer = response.Content.ReadAsAsync<Customer>().Result;

            response = WebClient.ApiClient.GetAsync("Rentals").Result;
            IEnumerable<Rental> rentals = response.Content.ReadAsAsync<IEnumerable<Rental>>().Result;

            response = WebClient.ApiClient.GetAsync("Tools").Result;
            IList<Tool> tools = response.Content.ReadAsAsync<IList<Tool>>().Result;

            response = WebClient.ApiClient.GetAsync("RentalTools").Result;
            IEnumerable<RentalTool> rentalTools = response.Content.ReadAsAsync<IEnumerable<RentalTool>>().Result;

            var customerHistory = rentals.Select(
                    r => new CustomerHistoryRentalsViewModel
                    {
                        RentalId = r.RentalId,
                        DateRented = r.DateRented,
                        CustomerId = r.CustomerId,                        
                        ToolId = r.RentalTools.Select(rt => rt.ToolId).FirstOrDefault(),
                        ToolName = tools.Where(t => t.ToolId == r.RentalTools.Select(rt => rt.ToolId).FirstOrDefault()).Select(tl => tl.ToolName).FirstOrDefault()
                    }).OrderByDescending(o => o.DateRented).ToList();

            var customerHistoryList = customerHistory.Where(c => c.CustomerId == customer.CustomerId);

            string[] columnNames = new string[] { "RentalId", "Date Rented", "Tool Name", "CustomerId" };

            //Build the csv file data as a comma seperated string
            string csv = string.Empty;

            foreach (string columnName in columnNames)
            {
                //Add the header row for CSV file
                csv += columnName + ',';
            }

            //Add new line
            csv += "\r\n";

            foreach (var cHistory in customerHistoryList)
            {
                //Add the Data rows
                csv += cHistory.RentalId.ToString().Replace(",", ";") + ',';
                csv += cHistory.DateRented.ToString().Replace(",", ";") + ',';
                csv += cHistory.ToolName.Replace(",", ";") + ',';
                csv += cHistory.CustomerId.ToString().Replace(",", ";") + ',';

                csv += "\r\n";
            }

            //Download the csv file
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "Report " + DateTime.Now.ToString() + ".csv");
        }

        // Displays the Rental History of a specific customer
        public ActionResult CustomerHistory(int Id)
        {

                HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customers/{Id}").Result;
                var customer = response.Content.ReadAsAsync<Customer>().Result;

                response = WebClient.ApiClient.GetAsync("Rentals").Result;
                IEnumerable<Rental> rentals = response.Content.ReadAsAsync<IEnumerable<Rental>>().Result;

                response = WebClient.ApiClient.GetAsync("Tools").Result;
                IList<Tool> tools = response.Content.ReadAsAsync<IList<Tool>>().Result;

                response = WebClient.ApiClient.GetAsync("RentalTools").Result;
                //IList<RentalTool> rentalTools = response.Content.ReadAsAsync<IList<RentalTool>>().Result;
                IEnumerable<RentalTool> rentalTools = response.Content.ReadAsAsync<IEnumerable<RentalTool>>().Result;

                //var toolid = rentals.Select(r => r.RentalTools.Select(rt => rt.ToolId));
                //Debug.WriteLine("These are rentals " + rentals.ToString());

                var customerHistory = rentals.Select(
                        r => new CustomerHistoryRentalsViewModel
                        {
                            RentalId = r.RentalId,
                            DateRented = r.DateRented,
                            CustomerId = r.CustomerId,
                            CustomerName = customer.CustomerName,
                            ToolId = r.RentalTools.Select(rt => rt.ToolId).FirstOrDefault(),
                            ToolName = tools.Where(t => t.ToolId == r.RentalTools.Select(rt => rt.ToolId).FirstOrDefault()).Select(tl => tl.ToolName).FirstOrDefault()
                            //ToolName = r.RentedTools.Select(x => x.ToolName).FirstOrDefault(),
                        }).OrderByDescending(o => o.DateRented).ToList();

                if(customerHistory.Any(c => c.CustomerId == customer.CustomerId))
                {
                    return View(customerHistory.Where(c => c.CustomerId == customer.CustomerId));                   
                }
                else
                {
                    TempData["SuccessMessage"] = "No history for " + customer.CustomerName + ".";
                    return RedirectToAction("Index");
                }            
           
        }
        #endregion

        public static List<Customer> customerList = new List<Customer>
        {
            new Customer{CustomerId = 1, CustomerName = "John Smith", CustomerPhone="3390 0675"},
            new Customer{CustomerId = 2, CustomerName = "Mary Parks", CustomerPhone="3855 1515"},
            new Customer{CustomerId = 3, CustomerName = "Robert Boyd", CustomerPhone="3290 9090"},

        };

        // GET: Customers
        public ActionResult Index()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Customers").Result;
            IEnumerable<Customer> customers = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customers/{Id}").Result;
            var customer = response.Content.ReadAsAsync<Customer>().Result;
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Customers", customer).Result;

                TempData["SuccessMessage"] = "Customer added successfully.";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customers/{Id}").Result;
            var customer = response.Content.ReadAsAsync<Customer>().Result;
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int Id, Customer customer)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Customers/{Id}", customer).Result;

                TempData["SuccessMessage"] = "Saved successfully.";

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                return View(customer);
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customers/{Id}").Result;
            var customer = response.Content.ReadAsAsync<Customer>().Result;

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int Id, Customer customer)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Customers/{Id}").Result;

                TempData["SuccessMessage"] = "Customer deleted successfully.";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}