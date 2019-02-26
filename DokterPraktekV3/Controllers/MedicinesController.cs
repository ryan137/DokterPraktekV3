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
    public class MedicinesController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();

        public ActionResult MedicineList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddMedicine(VM_Medicine viewModel)
        {
            bool flag = false;
            try
            {
                if (viewModel != null)
                {
                    var model = new Medicine();
                    var medTransModel = new MedicineTransaction();
                    var userId = User.Identity.GetUserId();
                    var doctorId = db.Doctors.FirstOrDefault(x => x.UserID == userId).ID;

                    if (doctorId > 0)
                    {
                        model.DoctorID = doctorId;
                        model.Name = viewModel.MedicineName;
                        model.Price = viewModel.MedicinePrice;
                        model.Quantity = viewModel.Quantity;
                        model.DateIn = DateTime.Now;
                        model.ExpireDate = Convert.ToDateTime(viewModel.ExpireDate);

                        db.Medicines.Add(model);
                        db.SaveChanges();

                        medTransModel.DoctorID = doctorId;
                        medTransModel.MedicineID = model.ID;
                        medTransModel.Quantity = model.Quantity;
                        medTransModel.TransactionStatus = true;
                        medTransModel.TransactionDate = DateTime.Now;

                        db.MedicineTransactions.Add(medTransModel);
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
                return Json(new { success = true, responseText = "Add Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Add Failed"}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDoctorMedicineList()
        {
            List<VM_Medicine> viewModel = new List<VM_Medicine>();

            var userId = User.Identity.GetUserId();
            var doctor = db.Doctors.FirstOrDefault(x => x.UserID == userId);

            if (doctor != null)
            {
                var medicineList = db.Medicines.Where(x => x.DoctorID == doctor.ID).ToList();

                foreach (var med in medicineList)
                {
                    var vm = new VM_Medicine();
                    vm.MedicineId = med.ID;
                    vm.MedicineName = med.Name;
                    vm.MedicinePrice = med.Price;
                    vm.Quantity = GetMedcineQuantity(med.ID);
                    vm.ExpireDate = med.ExpireDate.ToString("dd-MM-yyyy");

                    viewModel.Add(vm);
                }

            }

            return Json(new { data = viewModel }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMedicineDetails(int id)
        {
            VM_Medicine viewModel = new VM_Medicine();
            
            if (id != 0)
            {
                var medicineModel = db.Medicines.Where(x => x.ID == id).FirstOrDefault();
                
                if (medicineModel != null)
                {
                    viewModel.MedicineId = medicineModel.ID;
                    viewModel.MedicineName = medicineModel.Name;
                    viewModel.MedicinePrice = medicineModel.Price;
                    viewModel.Quantity = medicineModel.Quantity;
                    viewModel.ExpireDate = medicineModel.ExpireDate.ToString("yyyy-mm-dd");
                }
            }

            return Json(viewModel);
        }

        [HttpPost]
        public JsonResult EditMedicine(VM_Medicine viewModel)
        {
            bool flag = false;

            try
            {
                if (viewModel != null)
                {
                    var model = new Medicine();

                    model = db.Medicines.FirstOrDefault(x => x.ID == viewModel.MedicineId);

                    if (model != null)
                    {
                        model.Name = viewModel.MedicineName;
                        model.Price = viewModel.MedicinePrice;
                        model.ExpireDate = viewModel.ExpDate;

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

        private int GetMedcineQuantity(int medicineId)
        {
            try
            {
                int qty;

                var inQty = db.MedicineTransactions.Where(x => x.MedicineID == medicineId && x.TransactionStatus == true).ToList();
                var outQty = db.MedicineTransactions.Where(x => x.MedicineID == medicineId && x.TransactionStatus == false).ToList();

                var dataIn = inQty != null ? inQty.Sum(x => x.Quantity) : 0;
                var dataOut = outQty != null ? outQty.Sum(x => x.Quantity) : 0;

                qty = dataIn - dataOut; 

                return qty;
            }
            catch(Exception ex)
            {
                return 0;
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
