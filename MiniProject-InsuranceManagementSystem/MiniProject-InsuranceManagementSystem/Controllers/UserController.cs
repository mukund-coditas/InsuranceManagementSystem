using MiniProject_InsuranceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;

namespace MiniProject_InsuranceManagementSystem.Controllers
{
    public class UserController : Controller
    {

        InsuranceManagementSystemDbEntities1 entities;

        public UserController()
        {
            entities = new InsuranceManagementSystemDbEntities1();

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfilePage()
        {
            if (Session["IsAuthenticated"]!=null && (bool)Session["IsAuthenticated"])
            {
                ViewBag.FirstName = Session["FirstName"];
                ViewBag.LastName = Session["LastName"];
                return View();
            }

            return RedirectToAction("AccessDenied", "SuccessFailure");


        }

       
         public ActionResult FillCustomerDetails()
        {
            var customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public ActionResult FillCustomerDetails(Customer customer)
        {


            if (ModelState.IsValid)
            {
                var InsuranceType = Request.Form["insurances"].ToString();
                customer.UserId = Convert.ToInt32(Session["CurrentUserId"]);
                Session["Customer"] = customer;
                return RedirectToAction(InsuranceType);
            }

            return View();
        }
            
        

        public ActionResult PurchasedInsurances()
        {
            return View();
        }

        public ActionResult PurchaseNewInsurance()
        {
            return View();
        }

        public ActionResult HomeInsurance()
        {
            HomeInsurance homeInsurance = new HomeInsurance();

            return View(homeInsurance);
        }

        [HttpPost]
        public ActionResult HomeInsurance(HomeInsurance homeInsurance)
        {
            var customer = (Customer)Session["Customer"];
            homeInsurance.BuildingType = Request.Form["building-type"].ToString();
            customer.HomeInsurances.Add(homeInsurance);
            entities.Customers.Add(customer);
            entities.SaveChanges();
            return View();
        }

        public ActionResult HealthInsurance()
        {
            return View();
        }

        public ActionResult LifeInsurance()
        {
            return View();
        }

        public ActionResult AutomobileInsurance()
        {
            return View();
        }

        public ActionResult PensionPlans()
        {
            return View();
        }

        public ActionResult TravelInsurance()
        {
            return View();
        }

    }
}