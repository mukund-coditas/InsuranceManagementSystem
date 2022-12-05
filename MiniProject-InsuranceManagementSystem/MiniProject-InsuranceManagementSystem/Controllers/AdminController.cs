﻿using MiniProject_InsuranceManagementSystem.Models;
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

                return View(listOfVerifiedRequests);
            }

            return RedirectToAction("AccessDenied", "SuccessFailure");

        }
    }
}