using CarRentManagementSystem_DAL.DAL;
using CarRentManagementSystem_DomainClasses.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teqways_Com_CarRentManagementSystem.Filters;

namespace Teqways_Com_CarRentManagementSystem.Controllers
{
    [IsAdminLoggedIn]
    public class UserController : Controller
    {
        private CarRentManagementSystem_DAL.DAL.CarRentManagementSystemEntities DbContext = new CarRentManagementSystem_DAL.DAL.CarRentManagementSystemEntities();
        public RequestHelper.LocalCom _LocalCom = new RequestHelper.LocalCom();
        public UserController()
        {
            TempData["AlertFlag"] = 0;
        }
        // GET: User
        public ActionResult Index()
        {
            int flag = Convert.ToInt32(TempData["AlertFlag"]);
            ViewBag.AlertFlag = flag;
            return View(JsonConvert.DeserializeObject<List<UserViewModel>>(_LocalCom.GetAllUsers()));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("_Create");
        }
        [HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            user.CreatedOn = DateTime.UtcNow;
            user.CreatedBy_FK = Convert.ToInt32(Request.Cookies["RememberMeUserInfo"]["UserId"]);
            user.UserName = user.FirstName + " " + user.LastName;
            user.IsActive = true;
            _LocalCom.AddNewUser(user);
            TempData["AlertFlag"] = 1;
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult EditUser(int UserId)
        {
            User user = DbContext.Users.Where(i => i.Id == UserId).FirstOrDefault();
            UserViewModel userToEdit = new UserViewModel();
            userToEdit.Id = user.Id;
            userToEdit.FirstName = user.FirstName;
            userToEdit.LastName = user.LastName;
            userToEdit.EmailAddress = user.EmailAddress;
            userToEdit.Password = user.Password;
            return View("_EditUser", userToEdit);
        }
        [HttpPost]
        public ActionResult EditUser(UserViewModel User)
        {
            try
            {
                User user = DbContext.Users.Where(i => i.Id == User.Id).FirstOrDefault();
                user.FirstName = User.FirstName;
                user.LastName = User.LastName;
                user.EmailAddress = User.EmailAddress;
                user.Password = User.Password;
                user.ModifiedOn = DateTime.UtcNow;
                user.ModifiedBy_FK = Convert.ToInt32(Request.Cookies["RememberMeUserInfo"]["UserId"]);
                user.UserName = User.FirstName + " " + User.LastName;
                DbContext.SaveChanges();
                TempData["AlertFlag"] = 2;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ViewBag.ErrorDetails = ex.InnerException.ToString();
                    return View("ErrorPage");
                }
                else
                {
                    ViewBag.ErrorDetails = ex.Message.ToString();
                    return View("ErrorPage");
                }
            }
        }

        public bool IsUserExist(string EmailAddress)
        {
            User user = DbContext.Users.Where(i => i.EmailAddress == EmailAddress).FirstOrDefault();
            if (user != null)
                return true;
            else
                return false;
        }
        public bool IsUserExistForEdit(string EmailAddress, int UserId)
        {
            User user = DbContext.Users.Where(i => i.EmailAddress == EmailAddress && i.Id != UserId).FirstOrDefault();
            if (user != null)
                return true;
            else
                return false;
        }
        public ActionResult ChangeStatus(int UserId)
        {
            User user = DbContext.Users.Where(i => i.Id == UserId).FirstOrDefault();
            if (user != null)
            {
                if (user.IsActive == true)
                {
                    // For Deactivate flag
                    TempData["AlertFlag"] = 3;
                    user.IsActive = false;
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    // For Activate flag
                    TempData["AlertFlag"] = 4;
                    user.IsActive = true;
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            // Default Flag
            TempData["AlertFlag"] = 0;
            return RedirectToAction("Index");
        }
    }
}