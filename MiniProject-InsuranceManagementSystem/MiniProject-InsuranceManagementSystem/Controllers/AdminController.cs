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

        public ActionResult PendingRequests()
        {
            var result = (from customer in entities.Customers
                          join purchased in entities.Purchaseds
                          on customer.CustomerId equals
                          purchased.CustomerId
                          join insurance in entities.Insurances
                          on purchased.InsuranceId equals insurance.InsuranceId
                          where purchased.ApprovalStatus == "Pending"
                          select new CustomerRequest()
                          {
                              FirstName = customer.FirstName,
                              LastName = customer.LastName,
                              MobileNumber = customer.MobileNumber.ToString(),
                              CustomerId = customer.CustomerId,
                              InsuranceType = insurance.InsuranceType,
                              SubType = insurance.SubType,
                              PurchasedDate = (DateTime)purchased.DateOfPurchase

                          }).ToList();


            return View(result);
        }

        public ActionResult VerifyRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyRequest(int CustomerId)
        {

            return View();

        }

       public ActionResult ChooseInsuranceType()
        {
            return View();
        }
    }
}