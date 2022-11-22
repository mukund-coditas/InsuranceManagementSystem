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

        InsuranceManagementSystemDbEntities entities;

        public AgentController()
        {
            entities = new InsuranceManagementSystemDbEntities();

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgentProfilePage()
        {
            if (Session.Count < 1)
            {
                return RedirectToAction("AccessDenied", "SuccessFailure");
            }
            return RedirectToAction("AddInsurance");
        }

        public ActionResult AddInsurance()
        {  
            if (Session.Count < 1)
            {
                return RedirectToAction("AccessDenied","SuccessFailure");
            }
            Insurance insurance = new Insurance();
            return View(insurance);
   
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