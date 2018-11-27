using DokterPraktekV3.Models;
using DokterPraktekV3.Services;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DokterPraktekV3.Controllers
{
    public class HomeController : Controller
    {
        private ScheduleService scheduleService = new ScheduleService();
        private UserService userService = new UserService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DoctorSchedules(string currentFilter, string searchString, int? page)
        {
            var doctorId = User.Identity.GetUserId();

            if (doctorId != null)
            {
                var viewModel = new List<Schedule>();
                /*call all data from service*/
                ViewBag.DoctorName = userService.GetUserDetailsByUserId(doctorId).UserName;
                viewModel = scheduleService.GetDoctorSchedulesByDoctorId(doctorId);
                /*search data from name patient*/
                if (searchString != null)
                    page = 1;
                else
                    searchString = currentFilter;

                ViewBag.CurrentFilter = searchString;
                int pageNumber = (page ?? 1);
                int pageSize = 10;
                return View(viewModel.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View();
            }
            
        }
    }
}