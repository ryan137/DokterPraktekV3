using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Models
{
    public class VM_Medicine
    {
        public int DoctorId { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public decimal MedicinePrice { get; set; }
        public int Quantity { get; set; }
        public string ExpireDate { get; set; }
        public DateTime ExpDate { get; set; }
    }
}