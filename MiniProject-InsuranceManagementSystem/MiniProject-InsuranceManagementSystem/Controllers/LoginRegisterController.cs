using MiniProject_InsuranceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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


         


        public ActionResult Registration()
        {
                User user = new User();

                return View(user);
            
        }



            [HttpPost]
            public ActionResult Registration(string username,string password, string firstname, string lastname,string company,string CurrentRoleId)
            {

            try
            {
                var userDetails = (from u in entities.Users  where u.Username == username select u).FirstOrDefault();

                if (userDetails != null)
                {
                    ViewBag.UsernameAlreadyTaken = true;
                    ViewBag.Password=password;
                    ViewBag.FirstName=firstname;
                    ViewBag.LastName=lastname;
                    return View();
                }

                User user = new User();
                user.Username = username;
                user.FirstName = firstname;
                user.LastName = lastname;
                user.Password = password;
                user.CompanyName = company;

                user.RoleId = Convert.ToInt32(CurrentRoleId);
                user.RegistrationDate = DateTime.Now;
                entities.Users.Add(user);
                entities.SaveChanges();

                return RedirectToAction("RegisteredSuccessfully");
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
                   
                        var info = (from u in entities.Users  where u.Username == username && u.Password == password select u).FirstOrDefault();

                        string ActionName = (from r in entities.Roles where info.RoleId == r.RoleId select r.RoleName).FirstOrDefault();
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
                ViewBag.LoginFailed = true;

                return View();
                }


            }

        public ActionResult LogoutUser()
        {
            Session.Clear();
            return RedirectToAction("LoginUser","LoginRegister");
        }
        public ActionResult RegisteredSuccessfully()
        {
            return View();
        }
    }
    }

