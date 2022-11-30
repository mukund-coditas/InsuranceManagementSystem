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

        public ActionResult HomeInsurancePendingRequests()
        {
            var result = (from customer in entities.Customers
                          join purchased in entities.Purchaseds
                          on customer.CustomerId equals
                          purchased.CustomerId
                          join insurance in entities.Insurances
                          on purchased.InsuranceId equals insurance.InsuranceId
                          join homeInurance in entities.HomeInsurances
                          on customer.CustomerId equals homeInurance.CustomerId
                          where purchased.ApprovalStatus == "Pending"
                          select new CustomerRequestHomeInsurance()
                          {
                              FirstName = customer.FirstName,
                              LastName = customer.LastName,
                              MobileNumber = customer.MobileNumber.ToString(),
                              PurchasedId = purchased.PurchaseId,
                              InsuranceType = insurance.InsuranceType,
                              SubType = insurance.SubType,
                              PurchasedDate = (DateTime)purchased.DateOfPurchase,
                              HouseValuation= homeInurance.Valuation,
                              City=homeInurance.City,
                              PlanDuration=homeInurance.PlanDuration,
                              HouseNumber=homeInurance.HouseNumber,
                              FloorArea= (int)homeInurance.FloorArea           
                              
                          }).ToList();

            return View(result);
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

                var result = (from p in entities.Purchaseds
                              where p.PurchaseId == purchasedId
                              select p).SingleOrDefault();

                result.ApprovalStatus = ApprovalStatus;

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
            var result = (from customer in entities.Customers
                          join purchased in entities.Purchaseds
                          on customer.CustomerId equals
                          purchased.CustomerId
                          join insurance in entities.Insurances
                          on purchased.InsuranceId equals insurance.InsuranceId
                          join homeInurance in entities.HomeInsurances
                          on customer.CustomerId equals homeInurance.CustomerId
                          where purchased.ApprovalStatus == "Approved"
                          select new CustomerStatus()
                          {
                              FirstName = customer.FirstName,
                              LastName = customer.LastName,
                              MobileNumber = customer.MobileNumber.ToString(),
                              InsuranceType = insurance.InsuranceType,
                              SubType = insurance.SubType,
                              PurchasedDate = (DateTime)purchased.DateOfPurchase,
                              
                          }).ToList();

            return View(result);
        }
    }
}