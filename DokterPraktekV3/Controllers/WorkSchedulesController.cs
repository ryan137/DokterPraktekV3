using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DokterPraktekV3;

namespace DokterPraktekV3.Controllers
{
    public class WorkSchedulesController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();

        // GET: WorkSchedules
        public ActionResult Index()
        {
            return View(db.WorkSchedules.ToList());
        }

        // GET: WorkSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkSchedule workSchedule = db.WorkSchedules.Find(id);
            if (workSchedule == null)
            {
                return HttpNotFound();
            }
            return View(workSchedule);
        }

        // GET: WorkSchedules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Day,DoctorID,IsAvailable")] WorkSchedule workSchedule)
        {
            if (ModelState.IsValid)
            {
                db.WorkSchedules.Add(workSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workSchedule);
        }

        // GET: WorkSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkSchedule workSchedule = db.WorkSchedules.Find(id);
            if (workSchedule == null)
            {
                return HttpNotFound();
            }
            return View(workSchedule);
        }

        // POST: WorkSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Day,DoctorID,IsAvailable")] WorkSchedule workSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workSchedule);
        }

        // GET: WorkSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkSchedule workSchedule = db.WorkSchedules.Find(id);
            if (workSchedule == null)
            {
                return HttpNotFound();
            }
            return View(workSchedule);
        }

        // POST: WorkSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkSchedule workSchedule = db.WorkSchedules.Find(id);
            db.WorkSchedules.Remove(workSchedule);
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
