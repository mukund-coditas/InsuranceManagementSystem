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
                ViewBag.Username = Session["Username"];
                return View();
            }

            return RedirectToAction("AccessDenied", "SuccessFailure");

        }

       
         public ActionResult FillCustomerDetails()
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                var customer = new Customer();
                return View(customer);
            }
            return RedirectToAction("UserProfilePage");
        }

        [HttpPost]
        public ActionResult FillCustomerDetails(Customer customer)
        {

            if (ModelState.IsValid)
            {
                customer.UserId = Convert.ToInt32(Session["CurrentUserId"]);
                Session["Customer"] = customer;
                return RedirectToAction("ChooseInsurance");

            }

            return View();
        }
            
        

        public ActionResult PurchasedInsurances()
        {
            return View();
        }


        public ActionResult PurchaseNewInsurance()
        {
            var listofSuggestedInsurancePolicies = Session["SuggestedInsurances"];
            return View(listofSuggestedInsurancePolicies);
        }

        [HttpPost]
         public ActionResult ConfirmationOfInsurancePurchase(string InsuranceId)
        {

            var PuchasedDetails = new Purchased();
            var currentCustomer = (Customer)Session["Customer"];

            PuchasedDetails.DateOfPurchase = DateTime.Now;
            PuchasedDetails.ApprovalStatus = "Pending";
            PuchasedDetails.InsuranceId = Convert.ToInt32(InsuranceId);

            currentCustomer.Purchaseds.Add(PuchasedDetails);
            entities.Customers.Add(currentCustomer);
            entities.SaveChanges();

            return RedirectToAction("PurchasedSuccessfully");
        }

        public ActionResult HomeInsurance()
        {
            if (Session["Customer"] != null)
           {
                HomeInsurance homeInsurance = new HomeInsurance();

                return View(homeInsurance);
           }
            return RedirectToAction("FillCustomerDetails");
        }

        [HttpPost]
        public ActionResult HomeInsurance(HomeInsurance homeInsurance)
        {

            if (ModelState.IsValid)
            {
                var customer = (Customer)Session["Customer"];
                homeInsurance.BuildingType = Request.Form["building-type"].ToString();
                customer.HomeInsurances.Add(homeInsurance);

                var listofSuggestedInsurancePolicies = (from item in entities.Insurances  where item.InsuranceType == "Home Insurance" &&
                                                         item.SubType == homeInsurance.BuildingType select item).ToList();

                Session["Customer"] = customer;
                Session["SuggestedInsurances"] = listofSuggestedInsurancePolicies;

                return RedirectToAction("PurchaseNewInsurance");
            }
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
            if (Session["Customer"] != null)
            {
                AutomobileInsurance automobileInsurance = new AutomobileInsurance();

                return View(automobileInsurance);
            }

            return RedirectToAction("FillCustomerDetails");
        }


        [HttpPost]
        public ActionResult AutomobileInsurance(AutomobileInsurance automobileInsurance)
        {

            if (ModelState.IsValid)
            {

                var customer = (Customer)Session["Customer"];
                automobileInsurance.VehicleType = Request.Form["vehicle-type"].ToString();

                customer.AutomobileInsurances.Add(automobileInsurance);

                var listofSuggestedInsurancePolicies = (from item in entities.Insurances  where item.InsuranceType == "Automobile Insurance" &&
                                                     item.SubType == automobileInsurance.VehicleType select item).ToList();

                Session["Customer"] = customer;
                Session["SuggestedInsurances"] = listofSuggestedInsurancePolicies;
                return RedirectToAction("PurchaseNewInsurance");

            }

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


        [HttpPost]
        public ActionResult TravelInsurance(string age)
        {

            try
            {
                TravelInsurance travelInsurance = new TravelInsurance();

                var customer = (Customer)Session["Customer"];

                travelInsurance.Age = Convert.ToInt32(age);
                travelInsurance.HealthCondition = Request.Form["health-condition"].ToString();
                travelInsurance.DestinationCountry = Request.Form["destinationCountry"].ToString();
                travelInsurance.SourceCountry = Request.Form["sourceCountry"].ToString();
                travelInsurance.InsuranceAmount = 0;
                travelInsurance.StartDate = DateTime.Parse(Request.Form["startDate"].ToString());
                travelInsurance.EndDate = DateTime.Parse(Request.Form["endDate"].ToString());

                customer.TravelInsurances.Add(travelInsurance);

                var listofSuggestedInsurancePolicies = (from item in entities.Insurances where item.InsuranceType == "Travel Insurance" &&
                                                       item.SubType == travelInsurance.HealthCondition select item).ToList();

                Session["Customer"] = customer;
                Session["SuggestedInsurances"] = listofSuggestedInsurancePolicies;
                return RedirectToAction("PurchaseNewInsurance");
            }
            catch
            {
                return RedirectToAction("Failure", "SuccessFailure");
            }

            
        }

            public ActionResult YourInsurances()
           {

            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {

                int currentUserID = Convert.ToInt32(Session["CurrentUserId"]);

                var listofYourInsurances = entities.sp_getYourInsurances(currentUserID);

                return View(listofYourInsurances);

           }


           return RedirectToAction("AccessDenied", "SuccessFailure");

         }

        public ActionResult PurchasedSuccessfully()
           {
                return View();
            }

  
        public ActionResult ChooseInsurance()

        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                return View();

            }

            return RedirectToAction("AccessDenied", "SuccessFailure");

        }
    }
}