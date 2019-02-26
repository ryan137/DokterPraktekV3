using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Models
{
    public class VM_MedicalHistory
    {
        public int ID { get; set; }
        public int DoctorId { get; set; }
        public int ScheduleId { get; set; }
        public int PatientId { get; set; }
        public string Sickness { get; set; }
        public string DescriptionInfo { get; set; }
        public string CheckUpDate { get; set; }
        public string CheckUpPrice { get; set; }
        public List<VM_PatientMedicine> PatientMedicineList { get; set; }
    }
}