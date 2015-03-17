using Newtonsoft.Json;
using salt_web.Helper;
using salt_web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace salt_web.Controllers
{

    public class JQueryDataTableParamModel
    {
        public string sEcho { get; set; }

        public string sSearch { get; set; }

        public int iDisplayLength { get; set; }

        public int iDisplayStart { get; set; }
    }

    public class VendorController1 : BaseController
    {
        private salt_entities db = new salt_entities();
     
        // GET: Clients
        public ActionResult Index()
        {            
            return View();
        }


        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public static int ToInt(string toParse)
        {
            int result;
            if (int.TryParse(toParse, out result)) return result;

            return result;
        }

        public string GetAjaxData(JQueryDataTableParamModel param)
        {

            using (var e = new salt_entities())
            {
                int sEcho = ToInt(System.Web.HttpContext.Current.Request.Params["sEcho"]);
                int iDisplayLength = ToInt(System.Web.HttpContext.Current.Request.Params["iDisplayLength"]);
                int iDisplayStart = ToInt(System.Web.HttpContext.Current.Request.Params["iDisplayStart"]);
                string rawSearch = System.Web.HttpContext.Current.Request.Params["sSearch"];

                string participant = System.Web.HttpContext.Current.Request.Params["iParticipant"];

                var sb = new StringBuilder();

                var whereClause = string.Empty;
                ////if (participant.Length > 0)
                ////{
                ////    sb.Append(" Where UserName like ");
                ////    sb.Append("'%" + participant + "%'");
                ////    whereClause = sb.ToString();
                ////}
                sb.Clear();

                var filteredWhere = string.Empty;

                var wrappedSearch = "'%" + rawSearch + "%'";

                ////if (rawSearch.Length > 0)
                ////{
                ////    sb.Append(" WHERE ID LIKE ");
                ////    sb.Append(wrappedSearch);
                ////    sb.Append(" OR UserName LIKE ");
                ////    sb.Append(wrappedSearch);

                ////    filteredWhere = sb.ToString();
                ////}

                sb.Clear();

                var numberOfRowsToReturn = "";
                numberOfRowsToReturn = iDisplayLength == -1 ? "TotalRows" : (iDisplayStart + iDisplayLength).ToString();


                //var totalRowsCount = new ObjectParameter("TotalRowsCount", typeof(int));
                //var filteredRowsCount = new ObjectParameter("FilteredRowsCount", typeof(int));

                var totalDisplayRecords = "";
                var totalRecords = "";
                string outputJson = string.Empty;

                var rowClass = "";
                var count = 0;
                //VendorsListViewModel vm = new VendorsListViewModel();
                
                string sql = "SELECT [VendorCode], [CompanyLegalName], [PhoneNumber], [vendorpkid] as VendorId FROM [Vendors] order by [CompanyLegalName]";
                var aaData = e.Database.SqlQuery<VendorViewModel>(sql).Where(x => !string.IsNullOrEmpty(x.VendorCode)).Where(y => !string.IsNullOrWhiteSpace(y.VendorCode)).ToList();

                foreach (var data in aaData)
                {
                    if (totalRecords.Length == 0)
                    {
                        totalRecords = "15000";// vm.TotalRows.ToString();
                        totalDisplayRecords = "10";// vm.TotalDisplayRows.ToString();
                    }

                    sb.Append("{");
                    sb.AppendFormat(@"""DT_RowId"": ""{0}""", count++);
                    sb.Append(",");
                    sb.AppendFormat(@"""DT_RowClass"": ""{0}""", rowClass);
                    sb.Append(",");
                    sb.AppendFormat(@"""0"": ""{0}""", data.VendorCode);
                    sb.Append(",");
                    sb.AppendFormat(@"""1"": ""{0}""", data.CompanyLegalName);
                    sb.Append(",");
                    sb.AppendFormat(@"""2"": ""{0}""", data.PhoneNumber);
                    sb.Append("},"); 


                }

                // handles zero records
                if (totalRecords.Length == 0)
                {
                    sb.Append("{");
                    //sb.Append(@"""sEcho"": ");
                    //sb.AppendFormat(@"""{0}""", sEcho);
                    //sb.Append(",");
                    sb.Append(@"""iTotalRecords"": 0");
                    sb.Append(",");
                    sb.Append(@"""iTotalDisplayRecords"": 0");
                    sb.Append(", ");
                    sb.Append(@"""aaData"": [ ");
                    sb.Append("]}");
                    outputJson = sb.ToString();

                    return outputJson;
                }
                outputJson = sb.Remove(sb.Length - 1, 1).ToString();
                sb.Clear();

                sb.Append("{");
               // sb.Append(@"""sEcho"": ");
               // sb.AppendFormat(@"""{0}""", sEcho);
               // sb.Append(",");
                sb.Append(@"""iTotalRecords"": ");
                sb.Append(totalRecords);
                sb.Append(",");
                sb.Append(@"""iTotalDisplayRecords"": ");
                sb.Append(totalDisplayRecords);
                sb.Append(", ");
                sb.Append(@"""aaData"": [ ");
                sb.Append(outputJson);
                sb.Append("]}");

                outputJson = sb.ToString();

                return outputJson;


                dynamic collectionWrapper = new { results = aaData };

              //  return JsonConvert.SerializeObject(collectionWrapper, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });


               // var aaData = data.Select(d => new string[] { d.FirstName, d.LastName, d.Nationality, d.DateOfBirth.Value.ToString("dd MMM yyyy") }).ToArray();
                ////return Json(new
                ////{
                ////    sEcho = "1",
                ////    data = aaData,
                ////    iTotalRecords = Convert.ToInt32(15000),
                ////    iTotalDisplayRecords = Convert.ToInt32(10)
                ////}, JsonRequestBehavior.AllowGet);

 
            }
        }
         
        public string GetVendorList()
        {

            using (var context = new salt_entities())
            {
                string sql = "SELECT [VendorCode], [CompanyLegalName], [PhoneNumber], [vendorpkid] as VendorId FROM [Vendors] order by [CompanyLegalName]";
                var _data = context.Database.SqlQuery<VendorViewModel>(sql).Where(x => !string.IsNullOrEmpty(x.VendorCode)).Where(y => !string.IsNullOrWhiteSpace(y.VendorCode)).ToList();

                dynamic collectionWrapper = new { results  = _data};

                return JsonConvert.SerializeObject(collectionWrapper, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                //return Content(JsonConvert.SerializeObject(userList));
                //return Json(userList, "application/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult GetVendorList1()
        {
            //http://msdn.microsoft.com/en-us/data/jj592907.aspx


            using (var context = new salt_entities())
            {
                string sql = "SELECT [VendorCode], [CompanyLegalName], [PhoneNumber], [vendorpkid] as VendorId FROM [Vendors] order by [CompanyLegalName]";
                var _data = context.Database.SqlQuery<VendorViewModel>(sql).Where( x => !string.IsNullOrEmpty(x.VendorCode)).Where(y => !string.IsNullOrWhiteSpace(y.VendorCode)).ToList();

                return Json(_data, "application/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);

                ////var serializer = new JavaScriptSerializer();
                ////// For simplicity just use Int32's max value.
                ////// You could always read the value from the config section mentioned above.
                ////serializer.MaxJsonLength = Int32.MaxValue;

                ////var resultData = new { Value = "foo", Text = "var" };
                ////var result = new ContentResult
                ////{
                ////    Content = serializer.Serialize(_data),
                ////    ContentType = "application/json"
                ////};
                ////return (result);
                //return Json(result, JsonRequestBehavior.AllowGet);


            }

        }


        // GET: Clients/Details/5
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

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                vendor.VendorPKID = Guid.NewGuid();
                db.Vendors.Add(vendor);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            Vendor client = db.Vendors.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Edit(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
