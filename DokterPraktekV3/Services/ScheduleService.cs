using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DokterPraktekV3.Services
{
    public class ScheduleService
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();
        public List<Schedule> GetDoctorSchedulesByDoctorId(string doctorId)
        {
            var scheduleList = new List<Schedule>();

            scheduleList = db.Schedules.Where(x => x.DoctorID == doctorId && x.DateSchedule == DateTime.Today.Date).ToList();

            return scheduleList;
        }
    }
}