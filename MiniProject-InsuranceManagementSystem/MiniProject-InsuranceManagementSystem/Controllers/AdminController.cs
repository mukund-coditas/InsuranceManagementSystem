using MiniProject_InsuranceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniProject_InsuranceManagementSystem.Controllers
{
    public class AdminController : Controller
    {


        InsuranceManagementSystemDbEntities1 entities;

        public AdminController()
        {
            entities = new InsuranceManagementSystemDbEntities1();

        }


        public ActionResult Index()
        {
            return View();
        }

       public ActionResult AdminProfilePage()
          {

            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                ViewBag.FirstName = Session["FirstName"];
                ViewBag.LastName = Session["LastName"];
                ViewBag.Username = Session["Username"];
                return View();
            }

            return RedirectToAction("AccessDenied", "SuccessFailure");


          }

        public ActionResult AutomobileInsurancePendingRequests()
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {

                var pendingRequestsForHomeInsurance = entities.sp_getAutomobileInsurancePendingRequests();

                return View(pendingRequestsForHomeInsurance);
            }
            return RedirectToAction("AccessDenied", "SuccessFailure");

        }
        public ActionResult HomeInsurancePendingRequests()
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                var pendingRequestsForHomeInsurance = entities.sp_getHomeInsurancePendingRequests();

            //var pendingRequestsForHomeInsurance = (from customer in entities.Customers
            //              join purchased in entities.Purchaseds
            //              on customer.CustomerId equals
            //              purchased.CustomerId
            //              join insurance in entities.Insurances
            //              on purchased.InsuranceId equals insurance.InsuranceId
            //              join homeInurance in entities.HomeInsurances
            //              on customer.CustomerId equals homeInurance.CustomerId
            //              where purchased.ApprovalStatus == "Pending"
            //              select new CustomerRequestHomeInsurance()
            //              {
            //                  FirstName = customer.FirstName,
            //                  LastName = customer.LastName,
            //                  MobileNumber = customer.MobileNumber.ToString(),
            //                  PurchasedId = purchased.PurchaseId,
            //                  InsuranceType = insurance.InsuranceType,
            //                  SubType = insurance.SubType,
            //                  PurchasedDate = (DateTime)purchased.DateOfPurchase,
            //                  HouseValuation= homeInurance.Valuation,
            //                  City=homeInurance.City,
            //                  PlanDuration=homeInurance.PlanDuration,
            //                  HouseNumber=homeInurance.HouseNumber,
            //                  FloorArea= (int)homeInurance.FloorArea           

            //              }).ToList();

            return View(pendingRequestsForHomeInsurance);
          }           
        return RedirectToAction("AccessDenied", "SuccessFailure");

    }

    public ActionResult VerifyRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyRequest(string PurchasedId, string ApprovalStatus)
        {
            try
            {
                int purchasedId = Convert.ToInt32(PurchasedId);

                var currentRequest = (from p in entities.Purchaseds where p.PurchaseId == purchasedId  select p).SingleOrDefault();

                currentRequest.ApprovalStatus = ApprovalStatus;

                entities.SaveChanges();

                return RedirectToAction("ApprovedAlertDialog");
            }

            catch
            {
                return RedirectToAction("Failure","SuccessFailure");
            }
        }


       public ActionResult ChooseInsuranceType()
        {
            return View();
        }

        public ActionResult ApprovedAlertDialog()
        {
            return View();
        }

        public ActionResult AllVerifiedRequests()
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                var listOfVerifiedRequests = entities.sp_getAllVerifiedRequests();

                //var listOfVerifiedRequests = (from customer in entities.Customers
                //              join purchased in entities.Purchaseds
                //              on customer.CustomerId equals
                //              purchased.CustomerId
                //              join insurance in entities.Insurances
                //              on purchased.InsuranceId equals insurance.InsuranceId
                //              where purchased.ApprovalStatus == "Approved"
                //              select new CustomerStatus()
                //              {
                //                  FirstName = customer.FirstName,
                //                  LastName = customer.LastName,
                //                  MobileNumber = customer.MobileNumber.ToString(),
                //                  InsuranceType = insurance.InsuranceType,
                //                  SubType = insurance.SubType,
                //                  PurchasedDate = (DateTime)purchased.DateOfPurchase,

                //              }).ToList();

                return View(listOfVerifiedRequests);
            }
            return RedirectToAction("AccessDenied", "SuccessFailure");

        }
    }
}