using DokterPraktekV3.Models;
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
    public class PatientsController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();

        // GET: Patients
        public ActionResult Index()
        {
            return View(db.Patients.ToList());
        }

        public ActionResult PatientHistoryDetails(int scheduleId)
        {
            var schedule = db.Schedules.Find(scheduleId);
            Patient model = new Patient();
            if (schedule != null)
            {
                model = db.Patients.Where(x => x.ID == schedule.PatientID).FirstOrDefault();
                ViewBag.ScheduleId = schedule.ID;
            }
            return View(model);
        }
        
        public ActionResult PatientList()
        {
            return View();
        }

        public ActionResult PatientDetails(int patientId)
        {
            Patient model = new Patient();
            if (patientId != 0)
            {
                model = db.Patients.Where(x => x.ID == patientId).FirstOrDefault();
            }

            return View(model);
        }

        public JsonResult GetPatientList()
        {
            List<VM_Patient> viewModel = new List<VM_Patient>();
            
            var patientList = db.Patients.ToList();

            if (patientList != null)
            {
                foreach (var item in patientList)
                {
                    VM_Patient model = new VM_Patient();
                    model.ID = item.ID;
                    model.Name = item.Name;
                    model.Address = item.Address;
                    model.PhoneNumber = item.PhoneNumber;
                    model.Gender = item.Gender.Equals(true) ? "Pria" : "Wanita";

                    viewModel.Add(model);
                }
            }

            return Json(new { data = viewModel }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPatientDetails(int id)
        {
            VM_Patient vm = new VM_Patient();
            
            if (id != 0)
            {
                var patientModel = db.Patients.Where(x => x.ID == id).FirstOrDefault();

                vm.ID = patientModel.ID;
                vm.Name = patientModel.Name;
                vm.PhoneNumber = patientModel.PhoneNumber;
                vm.Address = patientModel.Address;
                vm.GenderBool = patientModel.Gender;
            }

            return Json(vm);
        }

        [HttpPost]
        public JsonResult EditPatient(VM_Patient viewModel)
        {
            bool flag = false;
            try
            {
                if (viewModel != null)
                {
                    var model = new Patient();

                    model = db.Patients.FirstOrDefault(x => x.ID == viewModel.ID);

                    if (model != null)
                    {
                        model.Name = viewModel.Name;
                        model.Address = viewModel.Address;
                        model.PhoneNumber = viewModel.PhoneNumber;
                        model.Gender = viewModel.GenderBool;

                        db.SaveChanges();

                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }
            
            if (flag)
            {
                return Json(new { success = true, responseText = "Edit Success." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Edit Failed." }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetPatientHistoryDetails(int medicalHistoryId)
        {
            VM_PatientHistoryDetails vm = new VM_PatientHistoryDetails();

            if (medicalHistoryId != 0)
            {
                var patientMedHistory = db.MedicalHistories.Where(x => x.ID == medicalHistoryId).FirstOrDefault();

                vm.Sickness = patientMedHistory.Sickness;
                vm.Description = patientMedHistory.DescriptionInfo;
                vm.CheckUpDate = patientMedHistory.CheckUpDate.ToString("dd-MM-yyyy");
                vm.CheckUpPrice = patientMedHistory.CheckUpPrice;
                vm.PatientMedicines = new List<VM_PatientMedicine>();

                var patientMedicines = db.PatientMedicines.Where(x => x.MedicalHistoryID == patientMedHistory.ID).ToList();

                foreach (var med in patientMedicines)
                {
                    VM_PatientMedicine viewModel = new VM_PatientMedicine();

                    viewModel.MedicineName = med.Medicine.Name;
                    viewModel.MedicineQuantity = med.Quantity;
                    viewModel.Dose = med.Description;

                    vm.PatientMedicines.Add(viewModel);
                }
            }

            return Json(vm);
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
