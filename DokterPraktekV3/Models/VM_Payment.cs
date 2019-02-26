using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Models
{
    public class VM_Payment
    {
        public int MedicalHistoryId { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public decimal TotalFee { get; set; }
        public string AppointmentDate { get; set; }
    }
}