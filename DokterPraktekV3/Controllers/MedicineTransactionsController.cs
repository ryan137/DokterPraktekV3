using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DokterPraktekV3
{
    public class MedicineTransactionsController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();

        // GET: MedicineTransactions
        public ActionResult Index()
        {
            return View(db.MedicineTransactions.ToList());
        }

        // GET: MedicineTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineTransaction medicineTransaction = db.MedicineTransactions.Find(id);
            if (medicineTransaction == null)
            {
                return HttpNotFound();
            }
            return View(medicineTransaction);
        }

        // GET: MedicineTransactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicineTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MedicineID,DoctorID,TransactionStatus,Quantity")] MedicineTransaction medicineTransaction)
        {
            if (ModelState.IsValid)
            {
                db.MedicineTransactions.Add(medicineTransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medicineTransaction);
        }

        // GET: MedicineTransactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineTransaction medicineTransaction = db.MedicineTransactions.Find(id);
            if (medicineTransaction == null)
            {
                return HttpNotFound();
            }
            return View(medicineTransaction);
        }

        // POST: MedicineTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MedicineID,DoctorID,TransactionStatus,Quantity")] MedicineTransaction medicineTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicineTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicineTransaction);
        }

        // GET: MedicineTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineTransaction medicineTransaction = db.MedicineTransactions.Find(id);
            if (medicineTransaction == null)
            {
                return HttpNotFound();
            }
            return View(medicineTransaction);
        }

        // POST: MedicineTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicineTransaction medicineTransaction = db.MedicineTransactions.Find(id);
            db.MedicineTransactions.Remove(medicineTransaction);
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
