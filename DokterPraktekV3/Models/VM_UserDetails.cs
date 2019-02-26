using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Models
{
    public class VM_UserDetails
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
    }
}