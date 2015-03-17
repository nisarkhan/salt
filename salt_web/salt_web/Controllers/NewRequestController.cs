using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using salt_web.Models;

namespace salt_web.Controllers
{
    public class NewRequestController : Controller
    {
        private salt_entities db = new salt_entities();

        // GET: NewRequest
        public ActionResult Index()
        {
            TempData["message"] = null;
            return View(db.WorkWebs.ToList());
        }

        // GET: NewRequest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkWeb workWeb = db.WorkWebs.Find(id);
            if (workWeb == null)
            {
                return HttpNotFound();
            }
            return View(workWeb);
        }

        // GET: NewRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewRequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkWeb workWeb)
        {
            if (ModelState.IsValid)
            {
                workWeb.Date = DateTime.Today;
                db.WorkWebs.Add(workWeb);

                int flag = db.SaveChanges();
                if (flag == 1)
                {
                    TempData["type"] = "success";
                    TempData["title"] = "Client";
                    TempData["message"] = "The New Request data was created successfully.";
                }
                return RedirectToAction("Create");
            }

            return View(workWeb);
        }

        // GET: NewRequest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkWeb workWeb = db.WorkWebs.Find(id);
            if (workWeb == null)
            {
                return HttpNotFound();
            }
            return View(workWeb);
        }

        // POST: NewRequest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Subject,Description,Priority,Status")] WorkWeb workWeb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workWeb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workWeb);
        }

        // GET: NewRequest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkWeb workWeb = db.WorkWebs.Find(id);
            if (workWeb == null)
            {
                return HttpNotFound();
            }
            return View(workWeb);
        }

        // POST: NewRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkWeb workWeb = db.WorkWebs.Find(id);
            db.WorkWebs.Remove(workWeb);
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
