using MiniProject_InsuranceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniProject_InsuranceManagementSystem.Controllers
{
    
        public class LoginRegisterController : Controller
        {
            InsuranceManagementSystemDbEntities1 entities;

            public LoginRegisterController()
            {
                entities = new InsuranceManagementSystemDbEntities1();

            }


            [HttpGet]
            public ActionResult Index()
            {
                var result = (from data in entities.Users select data).ToList();
                return View(result);
            }
            public ActionResult RolesFilterMethod()
            {
                return View();
            }

            [HttpPost]
            public ActionResult RolesFilterMethod(string button)
            {
                Session["RoleId"] = (from u in entities.Roles where u.RoleName == button select u.RoleId).First();
                return RedirectToAction("Registration");
            }


        public ActionResult Registration()
        {

            if (Session["RoleId"] != null)
            {
                User user = new User();

                return View(user);
            }
            return RedirectToAction("RolesFilterMethod");
        }



            [HttpPost]
            public ActionResult Registration(User user)
            {

                if (ModelState.IsValid)
                {
                    user.RoleId = Convert.ToInt32(Session["RoleId"]);
                    user.RegistrationDate = DateTime.Now;
                    entities.Users.Add(user);
                    entities.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }

            public ActionResult LoginUser()
            {
                return View();
            }

            [HttpPost]
            public ActionResult LoginUser(string username, string password)
            {

                try
                {
                    if (ModelState.IsValid)
                    {

                        var info = (from u in entities.Users
                                    where u.Username == username
                                    && u.Password == password
                                    select u).First();

                        string ActionName = (from r in entities.Roles where info.RoleId == r.RoleId select r.RoleName).First();
                        string ControllerName = ActionName;
                        ActionName += "ProfilePage";
                         Session["CurrentUserId"]= info.UserId;
                         Session["FirstName"] = info.FirstName;
                         Session["LastName"] = info.LastName;
                         Session["IsAuthenticated"] = true;
                        return RedirectToAction(ActionName, ControllerName);

                    }

                    return View();

                }
                catch (Exception ex)
                {

                    return RedirectToAction("Failure","SuccessFailure");
                }


            }

        public ActionResult LogoutUser()
        {
            Session.Clear();
            return RedirectToAction("LoginUser","LoginRegister");
        }

    }
    }

