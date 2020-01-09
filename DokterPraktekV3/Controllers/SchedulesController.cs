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
using Microsoft.AspNet.Identity;

namespace DokterPraktekV3.Controllers
{
    public class SchedulesController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();

        public ActionResult TodayBookList()
        {
            return View();
        }

        public ActionResult AllDoctorSchedules()
        {
            return View();
        }

        public JsonResult GetTodayScheduleList()
        {
            List<VM_TodaySchedule> viewModel = new List<VM_TodaySchedule>();

            if (User.IsInRole("Doctor"))
            {
                var userId = User.Identity.GetUserId();
                var doctor = db.Doctors.FirstOrDefault(x => x.UserID == userId);

                if (doctor != null)
                {
                    var schList = db.Schedules.Where(x => x.DoctorID == doctor.ID && x.DateSchedule == DateTime.Today).ToList();

                    foreach (var sch in schList)
                    {
                        var vm = new VM_TodaySchedule
                        {
                            ScheduleId = sch.ID,
                            PatientId = sch.PatientID,
                            BookingNumber = sch.BookingNumber,
                            BookingStatus = sch.BookingStatus,
                            PatientName = sch.Patient.Name,
                            DateSchedule = sch.DateSchedule.ToString("dd-MM-yyyy")
                        };

                        viewModel.Add(vm);
                    }
                }
            }
            else
            {
                var schList = db.Schedules.Where(x => x.DateSchedule == DateTime.Today).ToList();

                foreach (var sch in schList)
                {
                    var vm = new VM_TodaySchedule
                    {
                        ScheduleId = sch.ID,
                        BookingNumber = sch.BookingNumber,
                        PatientName = sch.Patient.Name,
                        BookingStatus = sch.BookingStatus,
                        DateSchedule = sch.DateSchedule.ToString("dd-MM-yyyy")
                    };

                    viewModel.Add(vm);
                }
            }
           

            return Json(new { data = viewModel }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllDoctorScheduleList()
        {
            List<VM_TodaySchedule> viewModel = new List<VM_TodaySchedule>();
            var userId = User.Identity.GetUserId();
            var doctor = db.Doctors.FirstOrDefault(x => x.UserID == userId);
            var schList = db.Schedules.Where(x => x.DoctorID == doctor.ID).ToList();

            foreach (var sch in schList.OrderBy(x => x.DateSchedule))
            {
                var vm = new VM_TodaySchedule
                {
                    BookingNumber = sch.BookingNumber,
                    DoctorName = sch.Doctor.Name,
                    PatientName = sch.Patient.Name,
                    BookingStatus = sch.BookingStatus,
                    DateSchedule = sch.DateSchedule.ToString("dd-MM-yyyy")
                };

                viewModel.Add(vm);
            }

            return Json(new { data = viewModel }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelBooking(int scheduleId)
        {
            try
            {
                var schedule = db.Schedules.Find(scheduleId);
                schedule.BookingStatus = "Cancel";
                db.SaveChanges();

                return Json(new { success = true , responseText = "Update Success" });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, responseText = "Update Failed" });
            }
        }

        public ActionResult AllBookList()
        {
            return View();
        }

        public JsonResult GetAllScheduleList()
        {
            List<VM_TodaySchedule> viewModel = new List<VM_TodaySchedule>();

            var schList = db.Schedules.ToList();

            foreach (var sch in schList)
            {
                var vm = new VM_TodaySchedule
                {
                    BookingNumber = sch.BookingNumber,
                    DoctorName = sch.Doctor.Name,
                    PatientName = sch.Patient.Name,
                    BookingStatus = sch.BookingStatus,
                    DateSchedule = sch.DateSchedule.ToString("dd-MM-yyyy")
                };

                viewModel.Add(vm);
            }

            return Json(new { data = viewModel }, JsonRequestBehavior.AllowGet);
        }

        // GET: Schedules/SelectDoctor
        public ActionResult SelectDoctor()
        {
            List<VM_SelectDoctor> viewModel = new List<VM_SelectDoctor>();
            var doctorList = db.Doctors.ToList();

            foreach (var doctor in doctorList)
            {
                var data = new VM_SelectDoctor()
                {
                    DoctorID = doctor.ID,
                    DoctorName = doctor.Name,
                    WorkSchedule = db.WorkSchedules.Where(x => x.DoctorID == doctor.ID && x.IsAvailable == true).ToList()
                };
                viewModel.Add(data);
            }
            return View(viewModel);
        }

        // GET: Schedules/Create
        public ActionResult Create(int doctorId)
        {
            VM_PatientRegister viewModel = new VM_PatientRegister()
            {
                DoctorID = doctorId
            };

            ViewBag.BlockDate = DateTime.Now.ToString("yyyy-mm-dd");
            return View(viewModel);
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VM_PatientRegister viewModel)
        {
            if (ModelState.IsValid)
            {
                // TO DO

                // Check if doctor schedule
                var day = viewModel.DateSchedule.DayOfWeek.ToString();
                var isDocAvailable = (from workSch in db.WorkSchedules
                                      where workSch.DoctorID == viewModel.DoctorID && workSch.Day == day
                                      select workSch.IsAvailable).First();
                if (isDocAvailable)
                {
                    // Check if this patient is registered by checking phone number
                    var isPatientRegistered = (from patient in db.Patients
                                               where patient.PhoneNumber == viewModel.PhoneNumber
                                               select patient).FirstOrDefault();
                    var objSchedule = new Schedule();
                    if (isPatientRegistered != null)
                    {
                        objSchedule = CreateBookSchedule(isPatientRegistered.ID, viewModel);
                    }
                    else
                    {
                        // Create new patient
                        var objPatient = CreatePatient(viewModel);

                        // Create booking
                        objSchedule = CreateBookSchedule(objPatient.ID, viewModel);
                    }

                    objSchedule.Doctor = db.Doctors.Where(x => x.ID == viewModel.DoctorID).FirstOrDefault();

                    TempData["scheduleTempData"] = objSchedule;
                    return RedirectToAction("PatientRegisterResult");
                }
                else
                {
                    ModelState.AddModelError("DateSchedule", "Doctor is not available on the choosen date");
                    ViewBag.BlockDate = DateTime.Now.ToString("yyyy-MM-dd");
                    return View(viewModel);
                }
            }

            return View(viewModel);
        }

        // GET: Schedules/Create
        public ActionResult CreateRegistered(int doctorId)
        {
            VM_PatientRegister viewModel = new VM_PatientRegister()
            {
                DoctorID = doctorId
            };

            ViewBag.BlockDate = DateTime.Now.ToString("yyyy-mm-dd");
            return View(viewModel);
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRegistered(VM_PatientRegister viewModel)
        {
            if (ModelState.IsValid)
            {
                // TO DO
                // Check if doctor available on choosen date
                var day = viewModel.DateSchedule.DayOfWeek.ToString();
                var isDocAvailable = (from workSch in db.WorkSchedules
                                      where workSch.DoctorID == viewModel.DoctorID && workSch.Day == day
                                      select workSch.IsAvailable).First();

                if (isDocAvailable)
                {
                    // Check if patient number is registered
                    var isPatientRegistered = (from patient in db.Patients
                                               where patient.ID == viewModel.PatientNumber
                                               select patient).FirstOrDefault();
                    // If yes create booking
                    if (isPatientRegistered != null)
                    {
                        var objSchedule = new Schedule();

                        objSchedule = CreateBookSchedule(isPatientRegistered.ID, viewModel);

                        objSchedule.Doctor = db.Doctors.Where(x => x.ID == viewModel.DoctorID).FirstOrDefault();

                        TempData["scheduleTempData"] = objSchedule;
                        return RedirectToAction("PatientRegisterResult");
                    }
                    // Else show error message patient number is not registered/wrong
                    else
                    {
                        ModelState.AddModelError("PatientNumber", "This Patient Number is not registered");
                        return View(viewModel);
                    }
                }
                // Else show error doctor not available
                else
                {
                    ModelState.AddModelError("DateSchedule", "Doctor is not available on the choosen date");
                    ViewBag.BlockDate = DateTime.Now.ToString("yyyy-MM-dd");
                    return View(viewModel);
                }        
            }

            return View(viewModel);
        }


        private Patient CreatePatient(VM_PatientRegister viewModel)
        {
            var lastPatientId = (from patient in db.Patients
                               orderby patient.ID descending
                               select patient.ID).FirstOrDefault();

            var objPatient = new Patient()
            {
                Name = viewModel.PatientName,
                Address = viewModel.PatientAddress,
                Gender = viewModel.Gender,
                PhoneNumber = viewModel.PhoneNumber
            };

            db.Patients.Add(objPatient);
            db.SaveChanges();

            return objPatient;
        }

        private Schedule CreateBookSchedule(int patientId, VM_PatientRegister viewModel)
        {
            // Create booking
            var lastBookingNo = (from schedule in db.Schedules
                                 where schedule.DoctorID == viewModel.DoctorID && schedule.DateSchedule == viewModel.DateSchedule
                                 orderby schedule.ID descending
                                 select schedule.BookingNumber).FirstOrDefault();
            if (lastBookingNo == null)
            {
                lastBookingNo = 101;
            }
            else
            {
                lastBookingNo += 1;
            }

            var objSchedule = new Schedule()
            {
                PatientID = patientId,
                DoctorID = viewModel.DoctorID,
                BookingNumber = lastBookingNo,
                DateSchedule = viewModel.DateSchedule,
                BookingStatus = "Booking"
            };

            db.Schedules.Add(objSchedule);
            db.SaveChanges();

            return objSchedule;
        }

        // GET
        public ActionResult PatientRegisterResult()
        {
            var model = (Schedule)TempData["scheduleTempData"];
            return View(model);
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
