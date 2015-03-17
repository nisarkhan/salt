using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using salt_web.Models;
using salt_web.Helper;
using salt_web.Models.ClientType;
using System.Data.Entity.Validation;
using AutoMapper;
using PagedList;

namespace salt_web.Controllers
{
    public class ClientController : BaseController
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
        
        public ActionResult Create()
        {
            using (var context = new salt_entities())
            {
                //IEnumerable<SelectListItem> contractType = db.MenuContractTypes.Select(b => new SelectListItem { Value = b.Code, Text = b.TypeOfContract }).ToList();
                //var userNameFiltered = db.aspnet_Users.Where(x => x.FirstName != null && x.LastName != null).ToList();
                //IEnumerable<SelectListItem> userName = userNameFiltered.Select(b => new SelectListItem { Value = b.UserId.ToString(), Text = b.LastName + " " + b.FirstName }).ToList();
                //IEnumerable<SelectListItem> stateName = db.MenuUSStates.Select(b => new SelectListItem { Value = b.Abbrev.ToString(), Text = b.FullName }).ToList();

                //ViewBag.DropDownTypeOfContract = contractType;
                //ViewBag.DropDownPrimarySalesStaff = userName;
                //ViewBag.DropDownState = stateName;

                DropDownLoadHelper ddl = new DropDownLoadHelper();
                ddl.DropDownLoadClient();
                ViewBag.DropDownTypeOfContract = ClientDropDowns.TypeOfContract;
                ViewBag.DropDownPrimarySalesStaff = ClientDropDowns.PrimarySalesStaff;
                ViewBag.DropDownState = ClientDropDowns.State;

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientViewModel client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var context = new salt_entities())
                    {
                        client.ClientPKID = Guid.NewGuid();
                        client.Travel = "5";
                        Client clientModel = Mapper.Map<ClientViewModel, Client>(client);

                        db.Clients.Add(clientModel);

                        int flag = db.SaveChanges();

                        if (flag == 1)
                        {
                            TempData["type"] = "success";
                            TempData["title"] = "Client";
                            TempData["message"] = "The Client data was created successfully.";
                        }

                        return RedirectToAction("Create");
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
            }
            else
            {
                DropDownLoadHelper ddl = new DropDownLoadHelper();
                ddl.DropDownLoadClient();
                ViewBag.DropDownTypeOfContract = ClientDropDowns.TypeOfContract;
                ViewBag.DropDownPrimarySalesStaff = ClientDropDowns.PrimarySalesStaff;
                ViewBag.DropDownState = ClientDropDowns.State;
            }
            return View();
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
                IEnumerable<SelectListItem> contractType = db.MenuContractTypes.Select(b => new SelectListItem { Value = b.Code, Text = b.TypeOfContract }).ToList();
                var userNameFiltered = db.aspnet_Users.Where(x => x.FirstName != null && x.LastName != null).ToList();
                IEnumerable<SelectListItem> userName = userNameFiltered.Select(b => new SelectListItem { Value = b.UserId.ToString(), Text = b.LastName + " " + b.FirstName }).ToList();
                IEnumerable<SelectListItem> stateName = db.MenuUSStates.Select(b => new SelectListItem { Value = b.Abbrev.ToString(), Text = b.FullName }).ToList();


                //string sql = string.Format("SELECT * FROM [Clients] where CustomerID = '{0}'", id);
                //var client = context.Database.SqlQuery<Client>(sql).FirstOrDefault();

                var client = context.Clients.Where(x => x.CustomerID == id).FirstOrDefault();

                Mapper.CreateMap<Client, ClientViewModel>();
                ClientViewModel cvm = Mapper.Map<Client, ClientViewModel>(client);


                ViewBag.DropDownTypeOfContract = contractType;
                ViewBag.DropDownPrimarySalesStaff = userName;
                ViewBag.DropDownState = stateName;

                return View(cvm);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientViewModel client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    client.Travel = "5";

                    Mapper.CreateMap<ClientViewModel, Client>();

                    Client clientModel = Mapper.Map<ClientViewModel, Client>(client);
                    db.Clients.Add(clientModel);

                    db.Entry(clientModel).State = EntityState.Modified;
                    int flag = db.SaveChanges();
                    if (flag == 1)
                    {
                        TempData["type"] = "success";
                        TempData["title"] = "Client";
                        TempData["message"] = "The Client data was updated successfully.";
                    }
                    return RedirectToAction("Edit");
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

        // GET: Client/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
