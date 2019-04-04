using CarRentManagementSystem_DAL.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Teqways_Com_CarRentManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private CarRentManagementSystem_DAL.DAL.CarRentManagementSystemEntities DbContext = new CarRentManagementSystem_DAL.DAL.CarRentManagementSystemEntities();
        public RequestHelper.LocalCom _LocalCom = new RequestHelper.LocalCom();

        [HttpGet]
        public ActionResult Login()
        {

            HttpCookie _CheckCookie = Request.Cookies["RememberMeUserInfo"];
            if (_CheckCookie != null)
            {
                if (_CheckCookie["UserRoleId"] == "1")
                {
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    Session.Clear();
                    return View();
                }
            }
            else
            {
                Session.Clear();
                return View();
            }


        }
        [HttpPost]
        public ActionResult LoginUser(string Email, string Password, string RememberMe)
        {

            try
            {
                User LoginResult;
                string Result = _LocalCom.Login(Email, Password, RememberMe);
                if (Result == "2" || Result == "3" || Result == "4")
                {
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    LoginResult = JsonConvert.DeserializeObject<User>(Result);
                    if (RememberMe == "true")
                    {
                        HttpCookie _CheckCookie = Request.Cookies["RememberMeUserInfo"];
                        if (_CheckCookie != null)
                        {
                            _CheckCookie.Expires = DateTime.Now.AddDays(-10);
                            Response.Cookies.Add(_CheckCookie);
                        }
                        HttpCookie _LoginCookie = new HttpCookie("RememberMeUserInfo");
                        _LoginCookie.Values.Add("UserId", LoginResult.Id.ToString());
                        _LoginCookie.Values.Add("UserName", LoginResult.FirstName + " " + LoginResult.LastName);
                        _LoginCookie.Values.Add("EmailId", LoginResult.EmailAddress);
                        _LoginCookie.Values.Add("RememberMe", "yes");
                        _LoginCookie.Expires = DateTime.Now.AddDays(200);
                        Response.Cookies.Add(_LoginCookie);
                    }
                    else
                    {
                        HttpCookie _CheckCookie = Request.Cookies["RememberMeUserInfo"];
                        if (_CheckCookie != null)
                        {
                            _CheckCookie.Expires = DateTime.Now.AddDays(-10);
                            Response.Cookies.Add(_CheckCookie);
                        }
                        HttpCookie _LoginCookie = new HttpCookie("RememberMeUserInfo");
                        _LoginCookie.Values.Add("UserId", LoginResult.Id.ToString());
                        _LoginCookie.Values.Add("UserName", LoginResult.FirstName + " " + LoginResult.LastName);
                        _LoginCookie.Values.Add("EmailId", LoginResult.EmailAddress);
                        _LoginCookie.Values.Add("RememberMe", "no");
                        _LoginCookie.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(_LoginCookie);
                    }

                }
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/');
                return Json(baseUrl + "/Home/Dashboard", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorDetails = ex.InnerException.ToString();
                return View("ErrorPage");
            }
        }
        public ActionResult Logout()
        {

            HttpCookie _LoginCookie = Request.Cookies["RememberMeUserInfo"];
            if (_LoginCookie != null)
            {
                _LoginCookie.Expires = DateTime.Now.AddDays(-10);
                Response.Cookies.Add(_LoginCookie);
            }
            Session.Clear();
            return View("Login");
        }

    }
}