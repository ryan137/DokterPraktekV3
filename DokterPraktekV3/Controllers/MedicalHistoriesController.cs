using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DokterPraktekV3.Models;

namespace DokterPraktekV3
{
    public class MedicalHistoriesController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();

        [HttpPost]
        public JsonResult AddPatientMedicalHistory(VM_MedicalHistory viewModel)
        {
            bool flag = false;
            try
            {
                var userId = User.Identity.GetUserId();
                var doctor = db.Doctors.FirstOrDefault(x => x.UserID == userId);

                if (viewModel != null)
                {
                    db.Schedules.Find(viewModel.ScheduleId).BookingStatus = "Completed";

                    MedicalHistory medicalHistoryModel = new MedicalHistory();
                    medicalHistoryModel.DoctorID = doctor.ID;
                    medicalHistoryModel.PatientID = viewModel.PatientId;
                    medicalHistoryModel.Sickness = viewModel.Sickness;
                    medicalHistoryModel.DescriptionInfo = viewModel.DescriptionInfo;
                    medicalHistoryModel.CheckUpDate = DateTime.Now;
                    medicalHistoryModel.CheckUpPrice = Convert.ToDecimal(viewModel.CheckUpPrice);

                    db.MedicalHistories.Add(medicalHistoryModel);
                    db.SaveChanges();

                    if (viewModel.PatientMedicineList != null)
                    {
                        foreach (var item in viewModel.PatientMedicineList)
                        {
                            PatientMedicine patientMedicineModel = new PatientMedicine();
                            patientMedicineModel.MedicalHistoryID = medicalHistoryModel.ID;
                            patientMedicineModel.MedicineID = item.MedicineId;
                            patientMedicineModel.Quantity = item.MedicineQuantity;
                            patientMedicineModel.Description = item.Dose;
                            db.PatientMedicines.Add(patientMedicineModel);
                            db.SaveChanges();

                            MedicineTransaction medicineTransModel = new MedicineTransaction();
                            medicineTransModel.MedicineID = item.MedicineId;
                            medicineTransModel.DoctorID = doctor.ID;
                            medicineTransModel.TransactionStatus = false;
                            medicineTransModel.Quantity = item.MedicineQuantity;
                            medicineTransModel.TransactionDate = DateTime.Now;
                            db.MedicineTransactions.Add(medicineTransModel);
                            db.SaveChanges();
                        }
                    }

                    flag = true;
                }
            }
            catch (Exception ex)
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

        public JsonResult GetPatientMedicicalHistoryList(int patientId)
        {
            List<VM_MedicalHistory> viewModel = new List<VM_MedicalHistory>();

            var patient = db.Patients.Where(x => x.ID == patientId).FirstOrDefault();
            if (patient != null)
            {
                var list = db.MedicalHistories.Where(x => x.PatientID == patient.ID).ToList();

                foreach (var item in list)
                {
                    VM_MedicalHistory model = new VM_MedicalHistory();
                    model.ID = item.ID;
                    model.Sickness = item.Sickness;
                    model.DescriptionInfo = item.DescriptionInfo;
                    model.CheckUpDate = item.CheckUpDate.ToShortDateString();
                    viewModel.Add(model);
                }
            }

            return Json(new { data = viewModel }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPatientHistoryDetails(int patientId)
        {
            var viewModel = new MedicalHistory();

            var patient = db.Patients.Where(x => x.ID == patientId).FirstOrDefault();
            if (patient != null)
            {
                viewModel = db.MedicalHistories.Where(x => x.PatientID == patient.ID).FirstOrDefault();
            }

            return Json(new { data = viewModel }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditPatientHistory(VM_MedicalHistory viewModel)
        {
            if (viewModel != null)
            {
                var model = new MedicalHistory();

                model = db.MedicalHistories.FirstOrDefault(x => x.ID == viewModel.ID);

                if (model != null)
                {
                    try
                    {
                        model.Sickness = viewModel.Sickness;
                        model.DescriptionInfo = viewModel.DescriptionInfo;
                        model.CheckUpPrice = Convert.ToDecimal(viewModel.CheckUpPrice);

                        db.SaveChanges();

                        var result = new { success = true, responseText = "Edit Success", obj = viewModel };

                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Edit Failed");
                    }
                }
            }

            return Json(new { success = false, responseText = "Edit Failed." }, JsonRequestBehavior.AllowGet);
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
