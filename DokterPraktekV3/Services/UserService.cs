using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Services
{
    public class UserService
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();
        public AspNetUser GetUserDetailsByUserId(string userId)
        {
            var userDetails = db.AspNetUsers.Where(x => x.Id == userId).FirstOrDefault();

            return userDetails; 
        }
    }
}