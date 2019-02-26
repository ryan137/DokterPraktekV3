using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Models
{
    public class VM_SelectDoctor
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public List<WorkSchedule> WorkSchedule { get; set; }
    }
}