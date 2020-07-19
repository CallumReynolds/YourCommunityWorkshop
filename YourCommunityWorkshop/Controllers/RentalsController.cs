using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using YourCommunityWorkshop.DAL;
using YourCommunityWorkshop.Models;
using YourCommunityWorkshop.ViewModels;

namespace YourCommunityWorkshop.Controllers
{
    public class RentalsController : Controller
    {
        private ToolContext db = new ToolContext();

        #region Old Rental Controller Code
        //// GET: Rentals
        //public ActionResult Index()
        //{
        //    //return View();

        //    // Something goes here...

        //    var customerRentalsViewModel = db.Rentals.Select(
        //        r => new CustomerRentalsViewModel
        //        {
        //            RentalId = r.RentalId,
        //            DateRented = r.DateRented,
        //            CustomerName = db.Customers.Where(c => c.CustomerId == r.CustomerId)
        //                                        .Select(u => u.CustomerName).FirstOrDefault()
        //        }).OrderByDescending(o => o.DateRented).ToList();

        //    return View(customerRentalsViewModel);
        //}

        //#region Edit
        //// GET: Edit
        //public ActionResult Edit(int Id)
        //{
        //    var rental = db.Rentals.Single(r => r.RentalId == Id);

        //    var rentedTools = db.RentalTools.Where(r => r.RentalId == Id)
        //                                        .Select(m => new CustomerToolsViewModel
        //                                        {
        //                                            RentalToolId = m.RentalToolId,
        //                                            RentalId = m.RentalId,
        //                                            ToolName = db.Tools.Where(c =>
        //                                                                c.ToolId == m.ToolId)
        //                                                                .Select(f => f.ToolName)
        //                                                                .FirstOrDefault()
        //                                        }).ToList();

        //    // RentedTools is a list of all tools rented in a single rental record
        //    rental.RentedTools = rentedTools; 
        //    return View(rental);
        //}

        //[HttpPost]
        //public ActionResult Edit(int Id, FormCollection collection)
        //{
        //    try
        //    {
        //        var rental = db.Rentals.Single(r => r.RentalId == Id);
        //        if (TryUpdateModel(rental))
        //        {
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        return View(rental);
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
        //    var rental = new Rental();

        //    rental.RentalId = (db.Rentals.Count<Rental>() == 0) ?
        //                        1 : db.Rentals.Max(r => r.RentalId) + 1;
        //    rental.DateRented = DateTime.Now;
        //    rental.Customers = GetCustomers();
        //    rental.RentedTools = new List<CustomerToolsViewModel>();

        //    return View(rental);
        //}

        //// POST: Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        Rental rental = new Rental();
        //        rental.RentalId = (db.Rentals.Count<Rental>() == 0) ?
        //                            1 : db.Rentals.Max(r => r.RentalId) + 1;
        //        // Parses the user input to an int via the form
        //        rental.CustomerId = int.Parse(collection["CustomerId"]);
        //        rental.DateRented = DateTime.Now;
        //        db.Rentals.Add(rental);
        //        db.SaveChanges();

        //        int count = db.RentalTools.Where(r => r.RentalId == rental.RentalId)
        //                                                .Count<RentalTool>();
        //        if(count == 0)
        //        {
        //            return RedirectToAction("Edit", new { Id = rental.RentalId });
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //#endregion

        //#region Delete
        //// GET: Delete
        //public ActionResult Delete(int Id)
        //{
        //    Rental rental = db.Rentals.Single(r => r.RentalId == Id);
        //    return View(rental);
        //}

        //// POST: Delete
        //[HttpPost]
        //public ActionResult Delete(int Id, FormCollection collection)
        //{
        //    try
        //    {
        //        var rental = db.Rentals.Single(r => r.RentalId == Id);
        //        db.Rentals.Remove(rental);
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
        //    var customerRentalDetails = db.Rentals.Where(c1 => c1.RentalId == Id).Select(r => new CustomerRentalDetailsViewModel
        //    {
        //        Rental = r,
        //        CustomerName = db.Customers.Where(c1 => c1.CustomerId == r.CustomerId).Select(cu => cu.CustomerName).FirstOrDefault(),
        //        RentedTools = r.RentalTools.Select(
        //            ri => new CustomerToolsViewModel
        //            {
        //                RentalId = ri.RentalId,
        //                ToolName = db.Tools.Where(c2 => c2.ToolId == ri.ToolId).Select(m => m.ToolName).FirstOrDefault()
        //            }).ToList()
        //    }).Single();

        //    return View(customerRentalDetails);
        //}
        //#endregion

        //#region AddTools
        //// GET: AddTools
        //public ActionResult AddTools(int RentalId)
        //{
        //    var rentalTool = new RentalTool();
        //    var tools = GetTools();
        //    rentalTool.RentalId = RentalId;
        //    rentalTool.Tools = tools;

        //    return View(rentalTool);
        //}

        //// POST: AddTools
        //[HttpPost]
        //public ActionResult AddTools(FormCollection collection)
        //{
        //    int Id = 0;

        //    try
        //    {
        //        RentalTool rentalTool = new RentalTool();
        //        rentalTool.ToolId = int.Parse(collection["ToolId"].ToString());
        //        rentalTool.RentalId = int.Parse(collection["RentalId"].ToString());
        //        Id = rentalTool.RentalId;
        //        db.RentalTools.Add(rentalTool);
        //        db.SaveChanges();

        //        return RedirectToAction("Edit", new { Id });
        //    }
        //    catch (Exception e)
        //    {
        //        return View("No record associated to rental can be found." +
        //            "\nMake sure to submit the rental details before" +
        //            "adding Tools");
        //    }
        //}
        //#endregion

        //#region EditRentedTool
        //// GET: EditRentedTool
        //public ActionResult EditRentedTool(int Id)
        //{
        //    var rentalTool = db.RentalTools.Single(r => r.RentalToolId == Id);
        //    rentalTool.Tools = GetTools();

        //    return View(rentalTool);
        //}

        //// POST: EditRentedTool
        //[HttpPost]
        //public ActionResult EditRentedTool(int Id, RentalTool rentalTool)
        //{
        //    try
        //    {
        //        var rentalToolLocal = db.RentalTools.Single(r => r.RentalToolId == Id);
        //        Id = rentalTool.RentalId;
        //        if (TryUpdateModel(rentalTool))
        //        {
        //            db.SaveChanges();

        //            return RedirectToAction("Edit", new { Id });
        //        }

        //        return View(rentalTool);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //#endregion

        //#region Helper Methods

        //public IEnumerable<SelectListTool> GetCustomers()
        //{
        //    List<SelectListTool> customers = db.Customers.AsNoTracking()
        //                                        .OrderBy(o => o.CustomerName)
        //                                        .Select(c => new SelectListTool
        //                                        {
        //                                            Value = c.CustomerId.ToString(),
        //                                            Text = c.CustomerName
        //                                        }).ToList();

        //    return new SelectList(customers, "Value", "Text");
        //}

        //public IEnumerable<SelectListTool> GetTools()
        //{
        //    List<SelectListTool> tools = db.Tools.AsNoTracking()
        //                                    .OrderBy(o => o.ToolName)
        //                                    .Select(t => new SelectListTool
        //                                    {
        //                                        Value = t.ToolId.ToString(),
        //                                        Text = t.ToolName   // Might have to add the other Tool model properties
        //                                    }).ToList();

        //    return new SelectList(tools, "Value", "Text");
        //}

        //#endregion
        #endregion

        // GET: Rentals
        public ActionResult Index()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Rentals").Result;
            IEnumerable<Rental> rentals = response.Content.ReadAsAsync<IEnumerable<Rental>>().Result;
            response = WebClient.ApiClient.GetAsync("Customers").Result;
            IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;
            response = WebClient.ApiClient.GetAsync("Tools").Result;
            IList<Tool> dbTools = response.Content.ReadAsAsync<IList<Tool>>().Result;

            var customerRentalsViewModel = rentals.Select(
                r => new CustomerRentalsViewModel
                {
                RentalId = r.RentalId,
                DateRented = r.DateRented,
                DateReturn = r.DateReturn.ToString(),
                CustomerName = customers.Where(c => c.CustomerId == r.CustomerId).Select(u => u.CustomerName).FirstOrDefault(),
                ToolName = dbTools.Where(t => t.ToolId == r.RentalTools.Select(rt => rt.ToolId).FirstOrDefault()).Select(tl => tl.ToolName).FirstOrDefault(),
                }).OrderByDescending(o => o.DateRented).ToList();

            customerRentalsViewModel.Where(d => d.DateReturn.ToString() == "").ToList().ForEach(y => y.DateReturn = "Not Returned");

            return View(customerRentalsViewModel);
        }

        // GET: Rentals/NoReturnDate
        public ActionResult ReturnToolView()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Rentals").Result;
            IEnumerable<Rental> rentals = response.Content.ReadAsAsync<IEnumerable<Rental>>().Result;
            response = WebClient.ApiClient.GetAsync("Customers").Result;
            IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;
            response = WebClient.ApiClient.GetAsync("Tools").Result;
            IList<Tool> dbTools = response.Content.ReadAsAsync<IList<Tool>>().Result;

            var returnToolsView = rentals.Select(
                r => new CustomerRentalsViewModel
                {
                    RentalId = r.RentalId,
                    DateRented = r.DateRented,
                    DateReturn = r.DateReturn.ToString(),
                    CustomerName = customers.Where(c => c.CustomerId == r.CustomerId).Select(u => u.CustomerName).FirstOrDefault(),
                    ToolName = dbTools.Where(t => t.ToolId == r.RentalTools.Select(rt => rt.ToolId).FirstOrDefault()).Select(tl => tl.ToolName).FirstOrDefault(),
                }).OrderByDescending(o => o.DateRented).ToList();

            // Displays all the Rentals that don't have a return date (Rentaltools waiting to be checked back in)
            returnToolsView.Where(d => d.DateReturn.ToString() == "").ToList().ForEach(y => y.DateReturn = "Not Returned");

            return View(returnToolsView.Where(x => x.DateReturn.ToString() == "Not Returned"));
        }

        // GET: Rentals/Id
        public ActionResult CheckInToolView(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{Id}").Result;

            var rentals = response.Content.ReadAsAsync<Rental>().Result;

            response = WebClient.ApiClient.GetAsync("Customers").Result;
            IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;
            response = WebClient.ApiClient.GetAsync("Tools").Result;
            IList<Tool> dbTools = response.Content.ReadAsAsync<IList<Tool>>().Result;

            var checkInToolView = new CustomerRentalsViewModel
                {
                    RentalId = rentals.RentalId,
                    DateRented = rentals.DateRented,
                    DateReturn = rentals.DateReturn.ToString(),
                    CustomerName = customers.Where(c => c.CustomerId == rentals.CustomerId).Select(u => u.CustomerName).FirstOrDefault(),
                    ToolName = dbTools.Where(t => t.ToolId == rentals.RentalTools.Select(rt => rt.ToolId).FirstOrDefault()).Select(tl => tl.ToolName).FirstOrDefault(),
                };

            return View(checkInToolView);
        }

        // PUT: RentalTool and Rental
        [HttpPost]
        public ActionResult CheckInToolView(int Id, Rental rental, Tool tool) 
        {
            // Get data from the API
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{Id}").Result;
            rental = response.Content.ReadAsAsync<Rental>().Result;

            response = WebClient.ApiClient.GetAsync("Tools").Result;                       
            IList<Tool> currentTool = response.Content.ReadAsAsync<IList<Tool>>().Result;

            rental.DateReturn = DateTime.Now;

            // Store the selected RentalToolId
            int currentRentalToolId = rental.RentalTools.Select(x => x.ToolId).FirstOrDefault();

            // If the tool in the list contains the same Id as the Tool in RentalTool then change the Availability back to true.
            currentTool.Where(w => w.ToolId == currentRentalToolId).ForEach(y => y.Availability = true);
            
            // Get the Tool where its Id equals the RentalTool Id.
            tool = currentTool.Where(x => x.ToolId == currentRentalToolId).FirstOrDefault();           

            // Send the changes back to the server.
            response = WebClient.ApiClient.PutAsJsonAsync($"Tools/{currentRentalToolId}", tool).Result;
            response = WebClient.ApiClient.PutAsJsonAsync($"Rentals/{rental.RentalId}", rental).Result;

            TempData["SuccessMessage"] = "Tool checked in successfully.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{Id}").Result;
                var rental = response.Content.ReadAsAsync<Rental>().Result;
                response = WebClient.ApiClient.GetAsync($"RentalToolsById/{Id}").Result;
                IList<RentalTool> rentalTools = response.Content.ReadAsAsync<IList<RentalTool>>().Result;
                response = WebClient.ApiClient.GetAsync("Tools").Result;
                IList<Tool> dbTools = response.Content.ReadAsAsync<IList<Tool>>().Result;

                var customers = GetCustomers();
                var rentedTools = rentalTools.Select(
                        m => new CustomerToolsViewModel
                        {
                            RentalToolId = m.RentalToolId,
                            RentalId = m.RentalId,
                            ToolName = dbTools.Where(c => c.ToolId == m.ToolId).Select(f => f.ToolName).FirstOrDefault()
                        }).ToList();

                rental.Customers = customers;
                rental.RentedTools = rentedTools;

                return View(rental);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(int Id, Rental rental)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Rentals/{Id}", rental).Result;

                TempData["SuccessMessage"] = "Saved successfully.";

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                return View(rental);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{Id}").Result;
            var rental = response.Content.ReadAsAsync<Rental>().Result;
            response = WebClient.ApiClient.GetAsync("Customers").Result;
            IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;
            response = WebClient.ApiClient.GetAsync("Tools").Result;
            IList<Tool> dbTools = response.Content.ReadAsAsync<IList<Tool>>().Result;
            var customerRentalDetails = new CustomerRentalDetailsViewModel
            {
                Rental = rental,
                CustomerName = customers.Where(ci => ci.CustomerId == rental.CustomerId).Select(cn => cn.CustomerName).FirstOrDefault(),
                RentedTools = rental.RentalTools.Select(
                        ri => new CustomerToolsViewModel
                        {
                            RentalId = ri.RentalId,
                            ToolName = dbTools.Where(c2 => c2.ToolId == ri.ToolId).Select(m => m.ToolName).FirstOrDefault()
                        }).ToList()
            };

            return View(customerRentalDetails);
        }

        public ActionResult Report()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Rentals").Result;

            return View();
        }

        public ActionResult Create()
        {
            var rental = new Rental();
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("GetRentalMaxId").Result;
            // Setting the primary key value to a negative value will make SQL server to find the next available PKID when you save it.
            rental.RentalId = -999;
            rental.DateRented = DateTime.Now;
            var customers = GetCustomers();
            rental.Customers = customers;
            rental.RentedTools = new List<CustomerToolsViewModel>();

            return View(rental);
        }

        [HttpPost]
        public ActionResult Create(Rental rental)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Rentals", rental).Result;
                rental = response.Content.ReadAsAsync<Rental>().Result;
                response = WebClient.ApiClient.GetAsync($"RentalToolsById/{rental.RentalId}").Result;
                IList<RentalTool> rentalTools = response.Content.ReadAsAsync<IList<RentalTool>>().Result;

                TempData["SuccessMessage"] = "Rental added successfully.";

                if (rentalTools.Count == 0)
                    return RedirectToAction("Edit", new { Id = rental.RentalId });
                else
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{Id}").Result;
            var rental = response.Content.ReadAsAsync<Rental>().Result;

            return View(rental);
        }

        [HttpPost]
        public ActionResult Delete(int Id, Rental rental)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Rentals/{Id}").Result;

                TempData["SuccessMessage"] = "Deleted successfully.";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddTools(int RentalId)
        {
            var rentalTool = new RentalTool();
            var tools = GetTools();
            rentalTool.RentalId = RentalId;
            rentalTool.Tools = tools;
            

            return View(rentalTool);
        }

        [HttpPost]
        public ActionResult AddTools(RentalTool rentalTool, Tool tool)
        {
            int Id = 0;
            try
            {
                Id = rentalTool.RentalId;
                HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("RentalTools", rentalTool).Result;

                response = WebClient.ApiClient.GetAsync($"Tools/{rentalTool.ToolId}").Result;
                var rentedTool = tool;
                rentedTool = response.Content.ReadAsAsync<Tool>().Result;

                // Change the tool Availability to false because it has been 'hired'
                if (rentedTool.ToolId == rentalTool.ToolId)
                {
                    rentedTool.Availability = false;
                }               

                response = WebClient.ApiClient.PutAsJsonAsync($"Tools/{rentedTool.ToolId}", rentedTool).Result;

                TempData["SuccessMessage"] = "Rental tool added successfully.";

                return RedirectToAction("Edit", new { Id, rentedTool });

            }
            catch (Exception)
            {
                return View("No record of the associated rental can be found.  Make sure to submit the rental details before adding Tools.");
            }
        }

        public ActionResult EditRentedTool(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"RentalTools/{Id}").Result;
            var rentalTool = response.Content.ReadAsAsync<RentalTool>().Result;
            var tools = GetTools();
            rentalTool.Tools = tools;

            return View(rentalTool);
        }

        [HttpPost]
        public ActionResult EditRentedTool(int Id, RentalTool rentalTool)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"RentalTools/{Id}", rentalTool).Result;

                Id = rentalTool.RentalId;
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Edit", new { Id });
                
                return View(rentalTool);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteRentedTool(int Id)
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync($"RentalTools/{Id}").Result;
            var rentalTool = response.Content.ReadAsAsync<RentalTool>().Result;
            var tools = GetTools();
            rentalTool.Tools = tools;

            return View(rentalTool);
        }

        [HttpPost]
        public ActionResult DeleteRentedTool(int Id, FormCollection collection)
        {
            try
            {
                HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"RentalTools/{Id}").Result;
                var rentalTool = response.Content.ReadAsAsync<RentalTool>().Result;
                Id = rentalTool.RentalId;
                return RedirectToAction("Edit", new { Id });
            }
            catch
            {
                return View();
            }
        }

        //Dropdown lists
        public IEnumerable<SelectListItem> GetTools()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
            IList<Tool> dbTools = response.Content.ReadAsAsync<IList<Tool>>().Result;

            List<SelectListItem> Tools = dbTools
                                            .Where(w => w.Active == false && w.Availability == true) // Gets tools that aren't retired and are available for hire
                                            .OrderBy(o => o.ToolName)
                                            .Select(m => new SelectListItem
                                            {
                                                Value = m.ToolId.ToString(),
                                                Text = m.ToolName
                                            }).ToList();

            return new SelectList(Tools, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetCustomers()
        {
            HttpResponseMessage response = WebClient.ApiClient.GetAsync("Customers").Result;
            IList<Customer> dbCustomers = response.Content.ReadAsAsync<IList<Customer>>().Result;
            List<SelectListItem> customers = dbCustomers
                .OrderBy(o => o.CustomerName)
                .Select(c => new SelectListItem
                {
                    Value = c.CustomerId.ToString(),
                    Text = c.CustomerName
                }).ToList();

            return new SelectList(customers, "Value", "Text");
        }

    }
}