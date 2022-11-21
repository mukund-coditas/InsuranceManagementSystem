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
            InsuranceManagementSystemDbEntities entities;

            public LoginRegisterController()
            {
                entities = new InsuranceManagementSystemDbEntities();

            }


            [HttpGet]
            public ActionResult Index()
            {
                var result = (from data in entities.Users select data).ToList();
                return View(result);
            }

            public ActionResult successed()
            {

                return View();
            }

            public ActionResult failure()
            {
                return View();
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

                User user = new User();

                return View(user);
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

                        return RedirectToAction(ActionName, ControllerName);

                    }

                    return View();

                }
                catch (Exception ex)
                {

                    return RedirectToAction("failure");
                }


            }

        }
    }

