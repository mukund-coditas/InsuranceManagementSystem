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
            public ActionResult Registration(string username,string password, string firstname, string lastname,string company)
            {

            try
            {
                User user = new User();
                user.Username = username;
                user.FirstName = firstname;
                user.LastName = lastname;
                user.Password = password;
                user.CompanyName = company;

                user.RoleId = Convert.ToInt32(Session["RoleId"]);
                user.RegistrationDate = DateTime.Now;
                entities.Users.Add(user);
                entities.SaveChanges();
                return RedirectToAction("LoginUser");
            }
            catch
            {
                return View();
            }
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
                         Session["Username"] = info.Username;
                         Session["IsAuthenticated"] = true;

                        return RedirectToAction(ActionName, ControllerName);

                  
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

