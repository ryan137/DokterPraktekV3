using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Models
{
    public class VM_Nota
    {
        public int HistoryID { get; set; }
        public string CheckUpDate { get; set; }
        public string PatientName { get; set; }
        public decimal Amount { get; set; }
        public decimal Paid { get; set; }
        public decimal CheckUpPrice { get; set; }
        public decimal MedicinesPrice { get; set; }
        public List<VM_PatientMedicine> PatientMedicines { get; set; }
    }
}