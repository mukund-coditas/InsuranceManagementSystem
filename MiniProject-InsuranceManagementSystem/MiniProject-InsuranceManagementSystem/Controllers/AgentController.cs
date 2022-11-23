using MiniProject_InsuranceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniProject_InsuranceManagementSystem.Controllers
{
    public class AgentController : Controller
    {
        
        InsuranceManagementSystemDbEntities1 entities;

        public AgentController()
        {
            entities = new InsuranceManagementSystemDbEntities1();

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgentProfilePage()
        {

            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                return RedirectToAction("AddInsurance");

            }

            return RedirectToAction("AccessDenied", "SuccessFailure");
        }

        public ActionResult AddInsurance()
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                Insurance insurance = new Insurance();
                return View(insurance);
            }
            return RedirectToAction("AccessDenied", "SuccessFailure");
            
   
        }

        [HttpPost]
        public ActionResult AddInsurance(Insurance insurance)
        {
            try
            {
                insurance.InsuranceType = Request.Form["insurances"].ToString();
                insurance.UserId = Convert.ToInt32(Session["CurrentUserId"]);
                insurance.ReleaseDate = DateTime.Now;
                entities.Insurances.Add(insurance);
                entities.SaveChanges();
                return RedirectToAction("Index", "Visitor");
            }
            catch
            {
                return RedirectToAction("Failure", "SuccessFailure");
            }
            
            
        }
    }
}