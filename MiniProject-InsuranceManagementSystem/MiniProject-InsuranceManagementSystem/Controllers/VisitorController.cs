using MiniProject_InsuranceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniProject_InsuranceManagementSystem.Controllers
{
    public class VisitorController : Controller
    {
        // GET: Visitor

        InsuranceManagementSystemDbEntities1 entities;

        public VisitorController()
        {
            entities = new InsuranceManagementSystemDbEntities1();

        }

        [HttpGet]
        public ActionResult Index()
        {
            var insurances = entities.Insurances;
            return View(insurances);
        }
        public ActionResult VisitorDashboard()
        {
            return View();

        }

        //public ActionResult Search()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Search(string userChoice)
        //{

        //    var insurances = entities.Insurances;
            

        //    string[] keywords = userChoice.Split(", ");

        //    foreach (string keyword in keywords)
        //    {
        //        insurances = insurances.Where(p => (p.InsuranceType.Contains(keyword))
        //                                      || (p.InsuranceProvider.Contains(keyword));
                                              
        //    }
        //    //TempData["Products"]= JsonSerializer.Serialize(products);
        //    ViewBag.productsearched = userChoice;
        //    return View(products);


        //}
    }
}