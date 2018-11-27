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
    public class PatientMedicinesController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();

        // GET: PatientMedicines
        public ActionResult Index()
        {
            return View(db.PatientMedicines.ToList());
        }

        // GET: PatientMedicines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientMedicine patientMedicine = db.PatientMedicines.Find(id);
            if (patientMedicine == null)
            {
                return HttpNotFound();
            }
            return View(patientMedicine);
        }

        // GET: PatientMedicines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientMedicines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MedicalHistoryID,MedicineID,Quantity,Description,MedicalPrice")] PatientMedicine patientMedicine)
        {
            if (ModelState.IsValid)
            {
                db.PatientMedicines.Add(patientMedicine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientMedicine);
        }

        // GET: PatientMedicines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientMedicine patientMedicine = db.PatientMedicines.Find(id);
            if (patientMedicine == null)
            {
                return HttpNotFound();
            }
            return View(patientMedicine);
        }

        // POST: PatientMedicines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MedicalHistoryID,MedicineID,Quantity,Description,MedicalPrice")] PatientMedicine patientMedicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientMedicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patientMedicine);
        }

        // GET: PatientMedicines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientMedicine patientMedicine = db.PatientMedicines.Find(id);
            if (patientMedicine == null)
            {
                return HttpNotFound();
            }
            return View(patientMedicine);
        }

        // POST: PatientMedicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientMedicine patientMedicine = db.PatientMedicines.Find(id);
            db.PatientMedicines.Remove(patientMedicine);
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
