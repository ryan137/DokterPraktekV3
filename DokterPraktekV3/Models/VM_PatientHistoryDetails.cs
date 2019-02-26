using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Models
{
    public class VM_PatientHistoryDetails
    {
        public string Sickness { get; set; }
        public string Description { get; set; }
        public decimal CheckUpPrice { get; set; }
        public string CheckUpDate { get; set; }
        public List<VM_PatientMedicine> PatientMedicines { get; set; }
    }
}