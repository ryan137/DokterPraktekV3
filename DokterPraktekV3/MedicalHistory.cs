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
    
    public partial class MedicalHistory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MedicalHistory()
        {
            this.PatientMedicines = new HashSet<PatientMedicine>();
            this.Payments = new HashSet<Payment>();
        }
    
        public int ID { get; set; }
        public string DoctorID { get; set; }
        public int PatientID { get; set; }
        public string Sickness { get; set; }
        public string DescriptionInfo { get; set; }
        public System.DateTime CheckUpDate { get; set; }
        public decimal CheckUpPrice { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Patient Patient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientMedicine> PatientMedicines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
