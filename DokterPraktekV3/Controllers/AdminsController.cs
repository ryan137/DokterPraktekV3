using DokterPraktekV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DokterPraktekV3.Controllers
{
    public class AdminsController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();
        // GET: Admins
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/AdminList
        public ActionResult AdminList()
        {
            return View();
        }

        public JsonResult GetAdminList()
        {
            List<VM_Admin> viewModel = new List<VM_Admin>();
            
            var adminList = db.Admins.Where(x => x.IsActive == true).ToList();

            foreach (var data in adminList)
            {
                var obj = new VM_Admin();
                obj.ID = data.ID;
                obj.Name = data.Name;
                obj.Email = data.AspNetUser.Email;
                obj.Address = data.Address;
                obj.PhoneNumber = data.PhoneNumber;
                obj.Gender = data.Gender == true ? "Male" : "Female";

                viewModel.Add(obj);
            }

            return Json(new { data = viewModel }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAdminDetails(int id)
        {
            var viewModel = new Admin();

            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Admins.Where(x => x.ID == id).FirstOrDefault();

            if (data != null)
            {
                viewModel = data;
            }

            return Json(viewModel);
        }

        [HttpPost]
        public JsonResult EditAdmin(VM_UserDetails viewModel)
        {
            bool flag = false;

            try
            {
                if (viewModel != null)
                {
                    var model = new Admin();

                    model = db.Admins.FirstOrDefault(x => x.ID == viewModel.ID);

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
                return Json(new { success = true, responseText = "Edit Success"}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Edit Failed." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteAdmin(int id)
        {
            bool flag = false;

            try
            {
                if (id > 0)
                {
                    var data = db.Admins.FirstOrDefault(x => x.ID == id);

                    if (data != null)
                    {
                        data.IsActive = false;

                        db.Entry(data).State = System.Data.Entity.EntityState.Modified;
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
                return Json(new { success = true, responseText = "Delete Success." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Delete Failed." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}