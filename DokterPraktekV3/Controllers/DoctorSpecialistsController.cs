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
    public class DoctorSpecialistsController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();

        // GET: DoctorSpecialists
        public ActionResult Index()
        {
            var doctorSpecialists = db.DoctorSpecialists.Include(d => d.AspNetUser).Include(d => d.Specialist);
            return View(doctorSpecialists.ToList());
        }

        // GET: DoctorSpecialists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorSpecialist doctorSpecialist = db.DoctorSpecialists.Find(id);
            if (doctorSpecialist == null)
            {
                return HttpNotFound();
            }
            return View(doctorSpecialist);
        }

        // GET: DoctorSpecialists/Create
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.SpecialtyID = new SelectList(db.Specialists, "ID", "SpecialistName");
            return View();
        }

        // POST: DoctorSpecialists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SpecialtyID,DoctorID")] DoctorSpecialist doctorSpecialist)
        {
            if (ModelState.IsValid)
            {
                db.DoctorSpecialists.Add(doctorSpecialist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorID = new SelectList(db.AspNetUsers, "Id", "Email", doctorSpecialist.DoctorID);
            ViewBag.SpecialtyID = new SelectList(db.Specialists, "ID", "SpecialistName", doctorSpecialist.SpecialtyID);
            return View(doctorSpecialist);
        }

        // GET: DoctorSpecialists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorSpecialist doctorSpecialist = db.DoctorSpecialists.Find(id);
            if (doctorSpecialist == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.AspNetUsers, "Id", "Email", doctorSpecialist.DoctorID);
            ViewBag.SpecialtyID = new SelectList(db.Specialists, "ID", "SpecialistName", doctorSpecialist.SpecialtyID);
            return View(doctorSpecialist);
        }

        // POST: DoctorSpecialists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SpecialtyID,DoctorID")] DoctorSpecialist doctorSpecialist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctorSpecialist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.AspNetUsers, "Id", "Email", doctorSpecialist.DoctorID);
            ViewBag.SpecialtyID = new SelectList(db.Specialists, "ID", "SpecialistName", doctorSpecialist.SpecialtyID);
            return View(doctorSpecialist);
        }

        // GET: DoctorSpecialists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorSpecialist doctorSpecialist = db.DoctorSpecialists.Find(id);
            if (doctorSpecialist == null)
            {
                return HttpNotFound();
            }
            return View(doctorSpecialist);
        }

        // POST: DoctorSpecialists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoctorSpecialist doctorSpecialist = db.DoctorSpecialists.Find(id);
            db.DoctorSpecialists.Remove(doctorSpecialist);
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
