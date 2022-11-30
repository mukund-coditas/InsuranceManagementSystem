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
            var insurances = Session["SuggestedInsurances"];
            return View(insurances);
        }

        [HttpPost]
         public ActionResult ConfirmationOfInsurancePurchase(string InsuranceId)
        {
            var PuchasedDetails = new Purchased();
            var customer = (Customer)Session["Customer"];

            PuchasedDetails.DateOfPurchase = DateTime.Now;
            PuchasedDetails.ApprovalStatus = "Pending";
            PuchasedDetails.InsuranceId = Convert.ToInt32(InsuranceId);

            customer.Purchaseds.Add(PuchasedDetails);
            entities.Customers.Add(customer);
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

                var insurances = (from items in entities.Insurances
                                  where items.InsuranceType == "Home Insurance" &&
                                  items.SubType == homeInsurance.BuildingType
                                  select items).ToList();

                Session["Customer"] = customer;
                Session["SuggestedInsurances"] = insurances;
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

        public ActionResult YourInsurances()
        {


            int currentUserID = Convert.ToInt32(Session["CurrentUserId"]);

            var listOfCustomers = ( from cts in entities.Customers where
                          cts.UserId == currentUserID select cts).ToList();


            var result = (from customer in listOfCustomers
                       join purchased in entities.Purchaseds
                       on customer.CustomerId equals
                       purchased.CustomerId
                       join insurance in entities.Insurances
                       on purchased.InsuranceId equals insurance.InsuranceId
                       select new CustomerStatus()
                       {
                           FirstName=customer.FirstName,
                           LastName=customer.LastName,
                           MobileNumber=customer.MobileNumber.ToString(),
                           ApprovalStatus=purchased.ApprovalStatus,
                           InsuranceType=insurance.InsuranceType,
                           SubType=insurance.SubType,
                           PurchasedDate= (DateTime)purchased.DateOfPurchase

                       }).ToList();


            return View(result);


        }
        public ActionResult PurchasedSuccessfully()
        {
            return View();
        }


    }
}