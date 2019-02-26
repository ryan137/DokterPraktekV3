﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DokterPraktekV3;

namespace DokterPraktekV3.Controllers
{
    public class PatientMedicinesController : Controller
    {
        private DokterPraktekEntities db = new DokterPraktekEntities();
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
