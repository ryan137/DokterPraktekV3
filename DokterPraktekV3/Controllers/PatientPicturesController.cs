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
    public class PatientPicturesController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();

        // GET: PatientPictures
        public ActionResult Index()
        {
            return View(db.PatientPictures.ToList());
        }

        // GET: PatientPictures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientPicture patientPicture = db.PatientPictures.Find(id);
            if (patientPicture == null)
            {
                return HttpNotFound();
            }
            return View(patientPicture);
        }

        // GET: PatientPictures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientPictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PatientID,ImageType,ImageContent")] PatientPicture patientPicture)
        {
            if (ModelState.IsValid)
            {
                db.PatientPictures.Add(patientPicture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientPicture);
        }

        // GET: PatientPictures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientPicture patientPicture = db.PatientPictures.Find(id);
            if (patientPicture == null)
            {
                return HttpNotFound();
            }
            return View(patientPicture);
        }

        // POST: PatientPictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PatientID,ImageType,ImageContent")] PatientPicture patientPicture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientPicture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patientPicture);
        }

        // GET: PatientPictures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientPicture patientPicture = db.PatientPictures.Find(id);
            if (patientPicture == null)
            {
                return HttpNotFound();
            }
            return View(patientPicture);
        }

        // POST: PatientPictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientPicture patientPicture = db.PatientPictures.Find(id);
            db.PatientPictures.Remove(patientPicture);
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
