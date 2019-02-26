using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DokterPraktekV3.Models
{
    public class VM_DoctorSettings
    {
        public Doctor Doctor { get; set; }
        public List<WorkSchedule> WorkSchedules { get; set; }
        public List<SelectListItem> Genders { get; set; }
    }
}