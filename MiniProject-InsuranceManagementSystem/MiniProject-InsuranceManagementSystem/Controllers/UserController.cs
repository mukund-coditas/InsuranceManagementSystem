using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniProject_InsuranceManagementSystem.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserProfilePage()
        {
            if (Session.Count < 1)
            {
                return RedirectToAction("AccessDenied","SuccessFailure");
            }


            ViewBag.FirstName = Session["FirstName"];
            ViewBag.LastName = Session["LastName"];
            return View();
        }
    }
}