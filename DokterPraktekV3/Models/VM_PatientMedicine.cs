using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Models
{
    public class VM_PatientMedicine
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int MedicineQuantity { get; set; }
        public string Dose { get; set; }
        public string ExpiredDate { get; set; }
    }
}