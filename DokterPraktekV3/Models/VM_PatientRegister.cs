using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Models
{
    public class VM_PatientRegister
    {
        public int DoctorID { get; set; }
        public int PatientNumber { get; set; }
        public string PatientName { get; set; }
        public string PatientAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public DateTime DateSchedule { get; set; }
        public int BookingNumber { get; set; }
    }
}