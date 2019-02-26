using DokterPraktekV3.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DokterPraktekV3
{
    public class DoctorsController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();
        
        //
        // GET: /Account/DoctorList
        public ActionResult DoctorList()
        {
            return View();
        }

        public ActionResult Settings()
        {
            VM_DoctorSettings viewModel = new VM_DoctorSettings();
            var userId = User.Identity.GetUserId();

            var doctor = db.Doctors.FirstOrDefault(x => x.UserID == userId);

            if (doctor != null)
            {
                viewModel.Doctor = doctor;
                viewModel.WorkSchedules = db.WorkSchedules.Where(x => x.DoctorID == doctor.ID).ToList();
            }

            viewModel.Genders = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = true.ToString(),
                    Text = "Male"
                },
                new SelectListItem()
                {
                    Value = false.ToString(),
                    Text = "Female"
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Settings(VM_DoctorSettings viewModel)
        {
            if (viewModel != null)
            {
                viewModel.Genders = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Value = true.ToString(),
                        Text = "Male"
                    },
                    new SelectListItem()
                    {
                        Value = false.ToString(),
                        Text = "Female"
                    }
                };

                if (viewModel.Doctor != null)
                {
                    try
                    {
                        Doctor docModel = db.Doctors.FirstOrDefault(x => x.ID == viewModel.Doctor.ID);

                        if (docModel != null)
                        {
                            docModel.Name = viewModel.Doctor.Name;
                            docModel.Address = viewModel.Doctor.Address;
                            docModel.PhoneNumber = viewModel.Doctor.PhoneNumber;
                            docModel.Gender = viewModel.Doctor.Gender;
                            docModel.Specialist = viewModel.Doctor.Specialist;
                        }
                        db.SaveChanges();

                        foreach (var ws in viewModel.WorkSchedules)
                        {
                            WorkSchedule wsModel = db.WorkSchedules.FirstOrDefault(x => x.ID == ws.ID);

                            if (wsModel != null)
                            {
                                wsModel.IsAvailable = ws.IsAvailable;
                            }

                            db.SaveChanges();
                        }

                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Save Failed");
                    }
                    

                    return View(viewModel);
                }
            }

            return View(viewModel);
        }

        public JsonResult GetDoctorList()
        {
            List<VM_Doctor> viewModel = new List<VM_Doctor>();
            
            var doctorList = db.Doctors.Where(x => x.IsActive == true).ToList();

            foreach (var data in doctorList)
            {
                var obj = new VM_Doctor();
                obj.ID = data.ID;
                obj.Name = data.Name;
                obj.Email = data.AspNetUser.Email;
                obj.Address = data.Address;
                obj.Gender = data.Gender == true ? "Male" : "Female";
                obj.PhoneNumber = data.PhoneNumber;

                viewModel.Add(obj);
            }

            return Json(new { data = viewModel }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDoctorDetails(int id)
        {
            var viewModel = new Doctor();

            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Doctors.Where(x => x.ID == id).FirstOrDefault();

            if (data != null)
            {
                viewModel = data;
            }

            return Json(viewModel);
        }

        [HttpPost]
        public JsonResult EditDoctor(VM_UserDetails viewModel)
        {
            bool flag = false;

            try
            {
                if (viewModel != null)
                {
                    var model = new Doctor();

                    model = db.Doctors.FirstOrDefault(x => x.ID == viewModel.ID);

                    if (model != null)
                    {
                        model.Name = viewModel.Name;
                        model.Address = viewModel.Address;
                        model.PhoneNumber = viewModel.PhoneNumber;
                        model.Gender = viewModel.Gender;
                        
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

        [HttpPost]
        public JsonResult DeleteDoctor(int id)
        {
            bool flag = false;

            try
            {
                if (id > 0)
                {
                    var data = db.Doctors.FirstOrDefault(x => x.ID == id);

                    if (data != null)
                    {
                        data.IsActive = false;

                        db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        flag = true;
                    }
                }

            }
            catch(Exception ex)
            {
                flag = false;
            }

            if (flag)
            {
                return Json(new { success = true, responseText = "Delete Success." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Delete Failed." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}