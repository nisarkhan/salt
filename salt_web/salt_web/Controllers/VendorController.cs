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
using System.Web.Script.Serialization;
using PagedList;

namespace salt_web.Controllers
{
    public class VendorController : BaseController
    {

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            TempData["message"] = null;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.VendorCodeSortParm = String.IsNullOrEmpty(sortOrder) ? "VendorCode_desc" : "";
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

                var _data = context.Vendors.Where(x => !string.IsNullOrEmpty(x.VendorCode)).Where(y => !(y.VendorCode == null || y.VendorCode.Trim() == string.Empty));

                //var _data = from s in db.Clients select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    _data = _data.Where(s => s.CompanyLegalName.Contains(searchString) || s.VendorCode.Contains(searchString) || s.PhoneNumber.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "VendorCode_desc":
                        _data = _data.OrderByDescending(s => s.VendorCode);
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
                    default:  // VendorCode ascending 
                        _data = _data.OrderBy(s => s.VendorCode);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VendorViewModel vendor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    vendor.VendorPKID = Guid.NewGuid();
                    Mapper.CreateMap<VendorViewModel, Vendor>();
                    Vendor model = Mapper.Map<VendorViewModel, Vendor>(vendor);

                    using (var context = new salt_entities())
                    {
                        context.Vendors.Add(model);

                        int flag = context.SaveChanges();

                        if (flag == 1)
                        {
                            TempData["type"] = "success";
                            TempData["title"] = "Client";
                            TempData["message"] = "The Vendor data was created successfully.";
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

            return View(vendor);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new salt_entities())
            {
                Vendor client = context.Vendors.Where(x => x.VendorPKID == id).FirstOrDefault();

                Mapper.CreateMap<Vendor, VendorViewModel>();
                VendorViewModel vvm = Mapper.Map<Vendor, VendorViewModel>(client);

                if (client == null)
                {
                    return HttpNotFound();
                }
                return View(vvm);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VendorViewModel vendor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<VendorViewModel, Vendor>();

                    using (var context = new salt_entities())
                    {
                        Vendor model = Mapper.Map<VendorViewModel, Vendor>(vendor);
                        context.Vendors.Add(model);

                        context.Entry(model).State = EntityState.Modified;
                        int flag = context.SaveChanges();
                        if (flag == 1)
                        {
                            TempData["type"] = "success";
                            TempData["title"] = "Vendor";
                            TempData["message"] = "The Vendor data was updated successfully.";
                        }
                        return RedirectToAction("Edit");
                    }
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
            return View(vendor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}