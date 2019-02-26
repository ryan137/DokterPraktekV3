using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DokterPraktekV3;
using DokterPraktekV3.Models;

namespace DokterPraktekV3.Controllers
{
    public class PaymentsController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nota(int id)
        {
            var viewModel = new VM_Nota();

            try
            {
                var dataMedicalHistory = db.MedicalHistories.Where(x => x.ID == id).FirstOrDefault();
                viewModel.HistoryID = dataMedicalHistory.ID;
                viewModel.CheckUpDate = dataMedicalHistory.CheckUpDate.ToString("dd-MM-yyyy");
                viewModel.PatientName = dataMedicalHistory.Patient.Name;
                viewModel.CheckUpPrice = dataMedicalHistory.CheckUpPrice;
                viewModel.MedicinesPrice = GetMedicinesPrice(dataMedicalHistory.ID);
                viewModel.Amount = GetPatientTotalFee(dataMedicalHistory.ID);
                viewModel.Paid = GetPatientLatestPayment(dataMedicalHistory.ID);
                viewModel.PatientMedicines = new List<VM_PatientMedicine>();

                foreach (var med in dataMedicalHistory.PatientMedicines)
                {
                    var obj = new VM_PatientMedicine();
                    obj.MedicineName = med.Medicine.Name;
                    obj.MedicineQuantity = med.Quantity;
                    obj.ExpiredDate = med.Medicine.ExpireDate.ToString("dd-MM-yyyy");
                    obj.Dose = med.Description;

                    viewModel.PatientMedicines.Add(obj);
                }
            }
            catch(Exception ex)
            {

            }
            

            return View(viewModel);
        }

        public JsonResult GetPatientPaymentsList()
        {
            List<VM_Payment> viewModel = new List<VM_Payment>();

            var data = db.MedicalHistories.ToList();

            if (data != null)
            {
                foreach (var item in data)
                {
                    VM_Payment model = new VM_Payment();
                    model.PatientName = item.Patient.Name;
                    model.DoctorName = item.Doctor.Name;
                    model.MedicalHistoryId = item.ID;
                    model.AppointmentDate = item.CheckUpDate.ToString("dd-MM-yyy");
                    var checkUpPrice = item.CheckUpPrice;
                    var medicinesPrice = item.PatientMedicines.Sum(x => x.Medicine.Price * x.Quantity);
                    var totalFee = checkUpPrice + medicinesPrice;
                    var paymentList = db.Payments.Where(x => x.MedicalHistoryID == item.ID).ToList();
                    decimal feePaid = 0;
                    if (paymentList != null)
                    {
                        feePaid = paymentList.Sum(x => x.Amount);
                    }

                    model.Amount = totalFee - feePaid;

                    if (model.Amount > 0)
                    {
                        model.Status = "Belum lunas";
                    }
                    else
                    {
                        model.Status = "Lunas";
                    }

                    viewModel.Add(model);
                }
            }

            return Json(new { data = viewModel }, JsonRequestBehavior.AllowGet);
        }

        private decimal GetPatientLatestPayment(int medicalHistoryId)
        {
            var payment = db.Payments.Where(x => x.MedicalHistoryID == medicalHistoryId).ToList();

            decimal latestPayment = payment.Last().Amount;

            return latestPayment;
        }

        private decimal GetMedicinesPrice(int medicalHistoryId)
        {
            var medicines = db.PatientMedicines.Where(x => x.MedicalHistoryID == medicalHistoryId).ToList();

            decimal medicinePrice = medicines.Sum(x => x.Medicine.Price);

            return medicinePrice;
        }

        private decimal GetPatientPaidAmount(int medicalHistoryId)
        {
            var paymentList = db.Payments.Where(x => x.MedicalHistoryID == medicalHistoryId).ToList();
            decimal feePaid = 0;
            if (paymentList != null)
            {
                feePaid = paymentList.Sum(x => x.Amount);
            }

            return feePaid;
        }

        private decimal GetPatientTotalFee(int medicalHistoryId)
        {
            var data = db.MedicalHistories.Where(x => x.ID == medicalHistoryId).FirstOrDefault();
            decimal paidAmt = 0;
            if (data != null)
            {
                var checkUpPrice = data.CheckUpPrice;
                var medicinesPrice = data.PatientMedicines.Sum(x => x.Medicine.Price * x.Quantity);
                var totalFee = checkUpPrice + medicinesPrice;
                decimal feePaid = 0;
                var paymentList = db.Payments.Where(x => x.MedicalHistoryID == medicalHistoryId).ToList();
                if (paymentList != null)
                {
                    feePaid = paymentList.Sum(x => x.Amount);
                }
                paidAmt = totalFee - feePaid;
            }

            return paidAmt;

        }

        public JsonResult GetPaymentDetails(int medicalHistoryId)
        {
            VM_Payment vm = new VM_Payment();

            var model = db.MedicalHistories.Where(x => x.ID == medicalHistoryId).FirstOrDefault();
            
            if (model != null)
            {
                vm.PatientName = model.Patient.Name;
                vm.MedicalHistoryId = model.ID;
                var checkUpPrice = model.CheckUpPrice;
                var medicinesPrice = model.PatientMedicines.Sum(x => x.Medicine.Price * x.Quantity);
                var totalFee = checkUpPrice + medicinesPrice;
                var paymentList = db.Payments.Where(x => x.MedicalHistoryID == model.ID).ToList();
                decimal feePaid = 0;
                if (paymentList != null)
                {
                    feePaid = paymentList.Sum(x => x.Amount);
                }

                vm.TotalFee = totalFee - feePaid;
            }

            return Json(vm);
        }

        [HttpPost]
        public JsonResult Pay(VM_Payment viewModel)
        {
            bool flag = false;
            try
            {
                if (viewModel != null)
                {
                    var model = new Payment();

                    model.MedicalHistoryID = viewModel.MedicalHistoryId;
                    model.Amount = viewModel.Amount;

                    db.Payments.Add(model);
                    db.SaveChanges();

                    flag = true;
                }
            }
            catch(Exception ex)
            {
                flag = false;
            }
            
            if (flag)
            {
                return Json(new { success = true, responseText = "Add Success." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Add Failed." }, JsonRequestBehavior.AllowGet);
            }
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
