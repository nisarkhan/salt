using AutoMapper;
using salt_web.Helper;
using salt_web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace salt_web.Controllers
{
    public class JobTrackingController : BaseController
    {
        private salt_entities db = new salt_entities();
        
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            TempData["message"] = null;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.ClientCodeSortParm = String.IsNullOrEmpty(sortOrder) ? "ClientCode_desc" : "";
            ViewBag.CompanyLegalNameSortParm = sortOrder == "CompanyLegalName" ? "CompanyLegalName_desc" : "CompanyLegalName";
            ViewBag.PhoneNumberSortParm = sortOrder == "PhoneNumber" ? "PhoneNumber_desc" : "PhoneNumber";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            using (var context = new salt_entities())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var _data = from s in db.Clients select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    _data = _data.Where(s => s.CompanyLegalName.Contains(searchString) || s.ClientCode.Contains(searchString) || s.PhoneNumber.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "ClientCode_desc":
                        _data = _data.OrderByDescending(s => s.ClientCode);
                        break;
                    case "CompanyLegalName":
                        _data = _data.OrderBy(s => s.CompanyLegalName);
                        break;
                    case "CompanyLegalName_desc":
                        _data = _data.OrderByDescending(s => s.CompanyLegalName);
                        break;
                    case "PhoneNumber":
                        _data = _data.OrderBy(s => s.PhoneNumber);
                        break;
                    case "PhoneNumber_desc":
                        _data = _data.OrderByDescending(s => s.PhoneNumber);
                        break;
                    default:  // ClientCode ascending 
                        _data = _data.OrderBy(s => s.ClientCode);
                        break;
                }

                int pageSize = 25;
                int pageNumber = (page ?? 1);

                var onePageOfData = _data.ToPagedList(pageNumber, pageSize);
                ViewBag.OnePageOfData = onePageOfData;
                return View();
                //Mapper.CreateMap<Client, ClientViewModel>();
                //var cvm = Mapper.Map<IEnumerable<Client>, IEnumerable<ClientViewModel>>(_data);
                //return View(cvm.ToPagedList(pageNumber, pageSize));
            }            
        }
         
        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var context = new salt_entities())
            {
                DropDownLoadHelper ddl = new DropDownLoadHelper();
                ddl.DropDownLoadJobTracking();

                ViewBag.DropDownAuditorsKey = JobTrackingDropDowns.AuditorsKey;
                ViewBag.DropDownContractStatus = JobTrackingDropDowns.ContractStatus;
                ViewBag.DropDownPrimarySalesStaff = JobTrackingDropDowns.PrimarySalesStaff;
                ViewBag.DropDownAccountRep = JobTrackingDropDowns.AccountRep;
                ViewBag.DropDownConfirmed = JobTrackingDropDowns.Confirmed;
                ViewBag.DropDownAssigned = JobTrackingDropDowns.Assigned;
                ViewBag.DropDownTravel = JobTrackingDropDowns.Travel;
                ViewBag.DropDownRental = JobTrackingDropDowns.Rental;

                Client client = db.Clients.Where(x => x.CustomerID == id).FirstOrDefault();

                Mapper.CreateMap<Client, ClientViewModel>();
                ClientViewModel cvm = Mapper.Map<Client, ClientViewModel>(client);
                
                return View(cvm);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientViewModel client)
        {
            using (var context = new salt_entities())
            {
                //   var clientEdit = ViewBag.ClientEdit as Client;
                var clientEdit = context.Clients.Where(x => x.CustomerID == client.CustomerID).FirstOrDefault();

                client.CompanyLegalName = clientEdit.CompanyLegalName;
                client.FaxNumber = clientEdit.FaxNumber;
                client.Address = clientEdit.Address;
                client.City = clientEdit.City;
                client.State = clientEdit.State;
                client.ZipCode = clientEdit.ZipCode;
                client.PhoneNumber = clientEdit.PhoneNumber;
                client.ContactFirstName = clientEdit.ContactFirstName;
                client.ContactLastName = clientEdit.ContactLastName;
                client.AuditDate = clientEdit.AuditDate;

                client.ClientCode = clientEdit.ClientCode;
                client.CompanyLegalName = clientEdit.CompanyLegalName;
                client.PhoneNumber = clientEdit.PhoneNumber;               
            }

            try
            {
                Mapper.CreateMap<ClientViewModel, Client>();
                Client model = Mapper.Map<ClientViewModel, Client>(client);
                db.Clients.Add(model);

                var errors = ModelState.Where(x => x.Value.Errors.Any()).Select(x => new { x.Key, x.Value.Errors });

                if (ModelState.IsValid)
                {

                    db.Entry(model).State = EntityState.Modified;
                    int flag = db.SaveChanges();
                    if (flag == 1)
                    {
                        TempData["type"] = "success";
                        TempData["title"] = "job Tracking";
                        TempData["message"] = "The Job Tracking data was updated successfully.";
                    }
                    return RedirectToAction("Edit");
                }
                else
                {
                    DropDownLoadHelper ddl = new DropDownLoadHelper();
                    ddl.DropDownLoadJobTracking();

                    ViewBag.DropDownAuditorsKey = JobTrackingDropDowns.AuditorsKey;
                    ViewBag.DropDownContractStatus = JobTrackingDropDowns.ContractStatus;
                    ViewBag.DropDownPrimarySalesStaff = JobTrackingDropDowns.PrimarySalesStaff;
                    ViewBag.DropDownAccountRep = JobTrackingDropDowns.AccountRep;
                    ViewBag.DropDownConfirmed = JobTrackingDropDowns.Confirmed;
                    ViewBag.DropDownAssigned = JobTrackingDropDowns.Assigned;
                    ViewBag.DropDownTravel = JobTrackingDropDowns.Travel;
                    ViewBag.DropDownRental = JobTrackingDropDowns.Rental;

                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return View(client);
        }
    }
}
