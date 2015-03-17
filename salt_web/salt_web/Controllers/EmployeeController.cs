using Newtonsoft.Json;
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
using System.Web.Script.Serialization;
using PagedList;

namespace salt_web.Controllers
{
    public class EmployeeController : BaseController
    {
        private salt_entities db = new salt_entities();

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            TempData["message"] = null;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "UserName_desc" : "";
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.InitialsSortParm = sortOrder == "Initials" ? "Initials_desc" : "Initials";

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

                string excludeGUID = "d4a3f2da-5a7b-4b32-8ef6-e84f1a96b32e";
                var  _data = db.aspnet_Users.Where(x => x.Initials != null).Where(y => y.UserId.ToString() != excludeGUID.ToString());
                

                if (!String.IsNullOrEmpty(searchString))
                {
                    _data = _data.Where(s => s.UserName.Contains(searchString) || s.FirstName.Contains(searchString) || s.LastName.Contains(searchString) || s.Initials.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "UserName_desc":
                        _data = _data.OrderByDescending(s => s.UserName);
                        break;
                    case "FirstName":
                        _data = _data.OrderBy(s => s.FirstName);
                        break;
                    case "FirstName_desc":
                        _data = _data.OrderByDescending(s => s.FirstName);
                        break;
                    case "LastName":
                        _data = _data.OrderBy(s => s.LastName);
                        break;
                    case "LastName_desc":
                        _data = _data.OrderByDescending(s => s.LastName);
                        break;
                   case "Initials":
                        _data = _data.OrderBy(s => s.Initials);
                        break;
                    case "Initials_desc":
                        _data = _data.OrderByDescending(s => s.Initials);
                        break;
                    default:  // ClientCode ascending 
                        _data = _data.OrderBy(s => s.UserName);
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
        
        public ActionResult Details(Guid? id)
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

        public ActionResult Create()
        {
            DropDownLoadHelper ddlEmployee = new DropDownLoadHelper();
            ddlEmployee.DropDownLoadEmployee();

            ViewBag.DropDownManager = EmployeeDropDowns.Manager;
            ViewBag.DropDownSecManager = EmployeeDropDowns.Manager;
            ViewBag.DropDownTrainer = EmployeeDropDowns.Trainer;  
            return View(); 

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel empViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string guid = "{77112510-277a-4695-862c-cccbd28625c8}";
                    Guid _applicationId;
                    Guid _userId = Guid.NewGuid();
                    if (guid.TryParseGuid(out _applicationId)) { 
                        //user.UserMembership.ApplicationId = _applicationId; 
                    }

                    using (var context = new salt_entities())
                    {
                        empViewModel.AspNetUsers.UserId = _userId;
                        empViewModel.AspNetUsers.ApplicationId = _applicationId;

                        empViewModel.AspNetUsers.UserName = empViewModel.UserName;
                        empViewModel.AspNetUsers.LoweredUserName = empViewModel.UserName.ToLower();
                        empViewModel.AspNetUsers.IsAnonymous = false;
                        empViewModel.AspNetUsers.LastActivityDate = DateTime.Now;
                        empViewModel.AspNetUsers.FirstName = empViewModel.FirstName;
                        empViewModel.AspNetUsers.LastName = empViewModel.LastName;
                        empViewModel.AspNetUsers.Initials = empViewModel.Initials;
                        empViewModel.AspNetUsers.OldID = empViewModel.OldID;
                        empViewModel.AspNetUsers.DOH = empViewModel.DOH;
                        empViewModel.AspNetUsers.LDE = empViewModel.LDE;
                        empViewModel.AspNetUsers.Manager = empViewModel.Manager;
                        empViewModel.AspNetUsers.SecManager = empViewModel.SecManager;

                        empViewModel.AspNetUsers.Trainer = empViewModel.Trainer;
                        empViewModel.AspNetUsers.TypeManager = empViewModel.TypeManager;
                        empViewModel.AspNetUsers.TypeAuditor = empViewModel.TypeAuditor;
                        empViewModel.AspNetUsers.TypeBDR = empViewModel.TypeBDR;
                        empViewModel.AspNetUsers.TypeTrainer = empViewModel.TypeTrainer;
                        empViewModel.AspNetUsers.TypeAcctRep = empViewModel.TypeAcctRep;
                        empViewModel.AspNetUsers.TypeColRep = empViewModel.TypeColRep;
                      
                        context.aspnet_Users.Add(empViewModel.AspNetUsers);
                        context.SaveChanges();


                    }
                    //AspNet_Membership
                    using (var context = new salt_entities())
                    {
                        empViewModel.AspNetMembership.UserId = _userId;
                        empViewModel.AspNetMembership.ApplicationId = _applicationId;

                        empViewModel.AspNetMembership.CreateDate = DateTime.Now;
                        empViewModel.AspNetMembership.Email = empViewModel.EmailAddress;
                        empViewModel.AspNetMembership.Comment = empViewModel.Comment;
                        empViewModel.AspNetMembership.IsApproved = empViewModel.IsApproved;
                        empViewModel.AspNetMembership.LastLoginDate = DateTime.Now;
                        empViewModel.AspNetMembership.LastPasswordChangedDate = DateTime.Now.AddDays(-1);
                        empViewModel.AspNetMembership.LastLockoutDate = DateTime.Now.AddDays(-1);
                        empViewModel.AspNetMembership.Password = empViewModel.Password;
                        empViewModel.AspNetMembership.PasswordFormat = 1;
                        empViewModel.AspNetMembership.PasswordQuestion = empViewModel.PasswordQuestion;
                        empViewModel.AspNetMembership.PasswordAnswer = empViewModel.PasswordAnswer;
                        empViewModel.AspNetMembership.PasswordSalt = "NULLVALUE";

                        empViewModel.AspNetMembership.IsLockedOut = false;
                        empViewModel.AspNetMembership.FailedPasswordAttemptCount = 0;
                        empViewModel.AspNetMembership.FailedPasswordAttemptWindowStart = DateTime.Now.AddDays(-1);
                        empViewModel.AspNetMembership.FailedPasswordAnswerAttemptCount = 0;
                        empViewModel.AspNetMembership.FailedPasswordAnswerAttemptWindowStart = DateTime.Now.AddDays(-1);

                        context.aspnet_Membership.Add(empViewModel.AspNetMembership);
                        int flag = context.SaveChanges();
                        if (flag == 1)
                        {
                            TempData["type"] = "success";
                            TempData["title"] = "Client";
                            TempData["message"] = "The Employee data was created successfully.";
                        }
                    }
                    return RedirectToAction("Create");

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
                DropDownLoadHelper ddlEmployee = new DropDownLoadHelper();
                ddlEmployee.DropDownLoadEmployee();
                ViewBag.DropDownManager = EmployeeDropDowns.Manager;
                ViewBag.DropDownSecManager = EmployeeDropDowns.Manager;
                ViewBag.DropDownTrainer = EmployeeDropDowns.Trainer;          


            }
            return View();
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor client = db.Vendors.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel vendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendor).State = EntityState.Modified;
                int flag = db.SaveChanges();
                if (flag == 1)
                {
                    TempData["type"] = "success";
                    TempData["title"] = "Client";
                    TempData["message"] = "The Employee data was created successfully.";
                }
                return RedirectToAction("Edit");
            }
            return View(vendor);
        }

        // GET: Clients/Delete/5
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

        // POST: Clients/Delete/5
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
