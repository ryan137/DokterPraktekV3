//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DokterPraktekV3
{
    using System;
    using System.Collections.Generic;
    
    public partial class PatientMedicine
    {
        public int ID { get; set; }
        public int MedicalHistoryID { get; set; }
        public int MedicineID { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public decimal MedicalPrice { get; set; }
    
        public virtual MedicalHistory MedicalHistory { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}
