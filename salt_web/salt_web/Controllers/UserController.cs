using salt_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using salt_web.Models.User;
using System.Net;
using AutoMapper;
using salt_web.Helper;
using System.Data.Entity.Validation;

namespace salt_web.Controllers
{
    public class UserController : Controller
    {
        private salt_entities db = new salt_entities();

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            TempData["message"] = null;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "UserName_desc" : "";
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.EmailAddressSortParm = sortOrder == "EmailAddress" ? "EmailAddress_desc" : "EmailAddress";

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
                //    ////select UserName, FirstName, LastName, Email from aspnet_Users 
                //a join aspnet_Membership b on a.userid = b.userid where isapproved = 1 order by username, firstname, lastname
                //var _data = context.Vendors.Where(x => !string.IsNullOrEmpty(x.VendorCode)).Where(y => !(y.VendorCode == null || y.VendorCode.Trim() == string.Empty));

                IQueryable<UserViewModel> _data = (from a in context.aspnet_Users
                             join b in context.aspnet_Membership on a.UserId equals b.UserId
                             where b.IsApproved == true
                             select new UserViewModel
                             {                                 
                                 UserId = a.UserId,
                                 UserName = a.UserName,
                                 FirstName = a.FirstName,
                                 LastName = a.LastName,
                                 Email = b.Email                                 
                             }).OrderBy(a => a.FirstName).ThenBy(b => b.LastName);

                //IQueryable<UserViewModel> _data1 = (from a in context.aspnet_Users
                //                                   join b in context.aspnet_Membership on a.UserId equals b.UserId
                //                                   where b.IsApproved == true
                //                                   select new UserViewModel
                //                                   {
                //                                       UserId = a.UserId,
                //                                       UserName = a.UserName,
                //                                       FirstName = a.FirstName,
                //                                       LastName = a.LastName,
                //                                       Email = b.Email
                //                                   }).OrderBy(a => a.FirstName).ThenBy(b => b.LastName);


                /*var query = context.aspnet_Users.Join(context.aspnet_Membership,
                            acc => acc.UserId,
                            bank => bank.UserId,
                            (acc, bank) => new { aspnet_Users = acc, aspnet_Membership = bank });

                query.Where(x => x.aspnet_Users.FirstName.Contains(searchString));
                */
                //var _data = from s in db.Clients select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    _data = _data.Where(x => x.FirstName.Contains(searchString) ||
                         x.UserName.Contains(searchString) ||
                         x.LastName.Contains(searchString) ||
                         x.Email.Contains(searchString));
                   //_data = _data.Where(x => x.FirstName.Contains(searchString)).Where(y => y.UserName.Contains(searchString)).Where(y => y.LastName.Contains(searchString)).Where(y => y.EmailAddress.Contains(searchString));
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
                    case "EmailAddress":
                        _data = _data.OrderBy(s => s.Email);
                        break;
                    case "EmailAddress_desc":
                        _data = _data.OrderByDescending(s => s.Email);
                        break;
                    default:  // VendorCode ascending 
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
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(string id)
        {
            DropDownLoadHelper ddlUser = new DropDownLoadHelper();
            ddlUser.DropDownLoadUser();

            ViewBag.DropDownGroup = UserDropDowns.Group;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new salt_entities())
            {
                string sql = string.Format(" SELECT aspnet_Roles.RoleId, aspnet_Roles.RoleName, aspnet_Users.FirstName, aspnet_Users.LastName, aspnet_Users.userid, " +
                                           " aspnet_Membership.Password,aspnet_Membership.Email, aspnet_Membership.PasswordQuestion, " +
                                           " aspnet_Membership.PasswordAnswer, aspnet_Users.UserName FROM aspnet_Users INNER JOIN aspnet_UsersInRoles " +
                                           " ON aspnet_Users.UserId = aspnet_UsersInRoles.UserId INNER JOIN aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId " +
                                           " FULL OUTER JOIN aspnet_Roles ON aspnet_UsersInRoles.RoleId = aspnet_Roles.RoleId WHERE aspnet_Users.UserName = '{0}'", id);

                var _data = context.Database.SqlQuery<UserViewModel>(sql);

                var queryResult = _data.Select(t => new UserViewModel
                    {
                        UserId = t.UserId,
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        UserName = t.UserName,
                        Password = t.Password,
                        ConfirmPassword = t.Password,
                        Email = t.Email,
                        PasswordQuestion = t.PasswordQuestion,
                        PasswordAnswer = t.PasswordAnswer,
                        RoleName = t.RoleName
                    }).FirstOrDefault(); 

                if (queryResult == null)
                {
                    return HttpNotFound();
                }
                return View(queryResult);
            }            
        }
 
        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
            if (allErrors != null && allErrors.Count() > 0)
            {
                foreach (var error in allErrors)
                {
                    ModelState.AddModelError("", error.ToString());
                }
                DropDownLoadHelper ddlUser = new DropDownLoadHelper();
                ddlUser.DropDownLoadUser();
                ViewBag.DropDownGroup = UserDropDowns.Group;
                return View();
            }

            try
            {
                using (var context = new salt_entities())
                {
                    user.AspNetMembership.Password = user.Password;
                    user.AspNetMembership.Email = user.Email;
                    user.AspNetMembership.PasswordQuestion = user.PasswordQuestion;
                    user.AspNetMembership.PasswordAnswer = user.PasswordAnswer;
                    user.AspNetMembership.PasswordSalt = string.Empty;

                    context.aspnet_Membership.Add(user.AspNetMembership);

                    
                    int flag = context.SaveChanges();
                    if (flag == 1)
                    {
                        TempData["type"] = "success";
                        TempData["title"] = "Vendor";
                        TempData["message"] = "The Vendor data was updated successfully.";
                    }

                    user.RoleName = user.RoleName;

                  
                    context.aspnet_Roles.Add(user.AspnetRoles);

                    int flag1 = context.SaveChanges();
                    if (flag == 1)
                    {
                        TempData["type"] = "success";
                        TempData["title"] = "Vendor";
                        TempData["message"] = "The Vendor data was updated successfully.";
                    }



                    user.RoleName = user.RoleName;
                    


                }
                return RedirectToAction("Edit");
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

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
