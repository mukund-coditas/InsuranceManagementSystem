﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniProject_InsuranceManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }



       public ActionResult AdminProfilePage()
          {
            if (Session.Count < 1)
            {
                return RedirectToAction("AccessDenied", "SuccessFailure");
            }
            return View();

          }
    }
}