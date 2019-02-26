using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Models
{
    public class VM_MedicineTransList
    {
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionDate { get; set; }
    }
}