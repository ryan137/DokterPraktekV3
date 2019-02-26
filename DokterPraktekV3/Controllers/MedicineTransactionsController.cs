using DokterPraktekV3.Models;
using Microsoft.AspNet.Identity;
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

        public ActionResult MedicineTransList()
        {
            return View();
        }

        public JsonResult GetMedicineTransList()
        {
            List<VM_MedicineTransList> viewModel = new List<VM_MedicineTransList>();
            var userId = User.Identity.GetUserId();
            var doctor = db.Doctors.FirstOrDefault(x => x.UserID == userId);

            if (doctor != null)
            {
                var medTrans = db.MedicineTransactions.Where(x => x.DoctorID == doctor.ID).ToList();

                foreach (var trans in medTrans)
                {
                    var vm = new VM_MedicineTransList
                    {
                        MedicineName = trans.Medicine.Name,
                        Quantity = trans.Quantity,
                        TransactionStatus = trans.TransactionStatus.Equals(true) ? "In" : "Out",
                        TransactionDate = trans.TransactionDate.ToString("dd-MM-yyyy")
                    };

                    viewModel.Add(vm);
                }
            }

            return Json(new { data = viewModel }, JsonRequestBehavior.AllowGet);
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
