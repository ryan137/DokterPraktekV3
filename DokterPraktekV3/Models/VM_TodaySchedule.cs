using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Models
{
    public class VM_TodaySchedule
    {
        public int ScheduleId { get; set; }
        public int PatientId { get; set; }
        public int? BookingNumber { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string DateSchedule { get; set; }
        public string BookingStatus { get; set; }
    }
}