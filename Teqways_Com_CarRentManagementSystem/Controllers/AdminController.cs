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
    [IsAdminLoggedIn ]
    public class AdminController : Controller
    {
        private CarRentManagementSystem_DAL.DAL.CarRentManagementSystemEntities DbContext = new CarRentManagementSystem_DAL.DAL.CarRentManagementSystemEntities();
        public RequestHelper.LocalCom _LocalCom = new RequestHelper.LocalCom();

        public AdminController()
        {
            TempData["AlertFlag"] = 0;
        }
        // GET: User
        public ActionResult DriversIndex()
        {
            int flag = Convert.ToInt32(TempData["AlertFlag"]);
            ViewBag.AlertFlag = flag;
            return View(JsonConvert.DeserializeObject<List<DriverViewModel>>(_LocalCom.GetAllDrivers()));
        }

        [HttpGet]
        public ActionResult CreateDriver()
        {
            return View("_CreateDriver");
        }
        [HttpPost]
        public ActionResult CreateDriver(DriverViewModel driver)
        {
            driver.IsActive = true;
            _LocalCom.AddNewDriver(driver);
            TempData["AlertFlag"] = 1;
            return RedirectToAction("DriversIndex");
        }
        [HttpGet]
        public ActionResult EditDriver(int DriverId)
        {
            Driver driver = DbContext.Drivers.Where(i => i.Id == DriverId).FirstOrDefault();
            DriverViewModel driverToEdit = new DriverViewModel();
            driverToEdit.Id = driver.Id;
            driverToEdit.FirstName = driver.FirstName;
            driverToEdit.LastName = driver.LastName;
            driverToEdit.CNIC = driver.CNIC;
            driverToEdit.ContactNo = driver.ContactNo;
            driverToEdit.LicenseNo = driver.LicenseNo;
            driverToEdit.LicenseType = driver.LicenseType;
            driverToEdit.ExperienceInYears = driver.ExperienceInYears;
            driverToEdit.City= driver.City;
            driverToEdit.Address = driver.Address;
            driverToEdit.IsActive = driver.IsActive;
            return View("_EditDriver", driverToEdit);
        }
        [HttpPost]
        public ActionResult EditDriver(DriverViewModel driver)
        {
            Driver driverToEdit = DbContext.Drivers.Where(i => i.Id == driver.Id).FirstOrDefault();
            driverToEdit.FirstName = driver.FirstName;
            driverToEdit.LastName = driver.LastName;
            driverToEdit.CNIC = driver.CNIC;
            driverToEdit.ContactNo = driver.ContactNo;
            driverToEdit.LicenseNo = driver.LicenseNo;
            driverToEdit.LicenseType = driver.LicenseType;
            driverToEdit.ExperienceInYears = driver.ExperienceInYears;
            driverToEdit.City = driver.City;
            driverToEdit.Address = driver.Address;
            driverToEdit.IsActive = driver.IsActive;
            DbContext.SaveChanges();
            // Default Flag
            TempData["AlertFlag"] = 2;
            return RedirectToAction("DriversIndex");
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
        public ActionResult ChangeDriverStatus(int DriverId)
        {
            Driver driver = DbContext.Drivers.Where(i => i.Id == DriverId).FirstOrDefault();
            if (driver != null)
            {
                if (driver.IsActive == true)
                {
                    // For Deactivate flag
                    TempData["AlertFlag"] = 3;
                    driver.IsActive = false;
                    DbContext.SaveChanges();
                    return RedirectToAction("DriversIndex");
                }
                else
                {
                    // For Activate flag
                    TempData["AlertFlag"] = 4;
                    driver.IsActive = true;
                    DbContext.SaveChanges();
                    return RedirectToAction("DriversIndex");
                }
            }
            // Default Flag
            TempData["AlertFlag"] = 0;
            return RedirectToAction("DriversIndex");
        }
        [HttpGet]
        public ActionResult VehiclesIndex()
        {
            int flag = Convert.ToInt32(TempData["AlertFlag"]);
            ViewBag.AlertFlag = flag;
            return View(JsonConvert.DeserializeObject<List<VehicleViewModel>>(_LocalCom.GetAllVehicles()));
        }

        [HttpGet]
        public ActionResult CreateVehicle()
        {
            return View("_CreateVehicle");
        }
        [HttpPost]
        public ActionResult CreateVehicle(VehicleViewModel Vehicle)
        {
            Vehicle.IsActive = true;
            _LocalCom.AddNewVehicle(Vehicle);
            TempData["AlertFlag"] = 1;
            return RedirectToAction("VehiclesIndex");
        }
        [HttpGet]
        public ActionResult EditVehicle(int VehicleId)
        {
            Vehicle Vehicle = DbContext.Vehicles.Where(i => i.Id == VehicleId).FirstOrDefault();
            VehicleViewModel VehicleToEdit = new VehicleViewModel();
            VehicleToEdit.Id = Vehicle.Id;
            VehicleToEdit.Name = Vehicle.Name;
            VehicleToEdit.Model = Vehicle.Model;
            VehicleToEdit.Color = Vehicle.Color;
            VehicleToEdit.Number = Vehicle.Number;
            VehicleToEdit.RegistredIn = Vehicle.RegistredIn;
            VehicleToEdit.IsActive = Vehicle.IsActive;
            return View("_EditVehicle", VehicleToEdit);
        }
        [HttpPost]
        public ActionResult EditVehicle(VehicleViewModel Vehicle)
        {
            Vehicle VehicleToEdit = DbContext.Vehicles.Where(i => i.Id == Vehicle.Id).FirstOrDefault();
            VehicleToEdit.Name = Vehicle.Name;
            VehicleToEdit.Model = Vehicle.Model;
            VehicleToEdit.Color = Vehicle.Color;
            VehicleToEdit.Number = Vehicle.Number;
            VehicleToEdit.RegistredIn = Vehicle.RegistredIn;
            VehicleToEdit.IsActive = Vehicle.IsActive;
            DbContext.SaveChanges();
            // Default Flag
            TempData["AlertFlag"] = 2;
            return RedirectToAction("VehiclesIndex");
        }


        public bool IsVehicleExist(string VehicleNumber)
        {
            Vehicle user = DbContext.Vehicles.Where(i => i.Number == VehicleNumber).FirstOrDefault();
            if (user != null)
                return true;
            else
                return false;
        }
        public bool IsVehicleExistForEdit(string VehicleNumber, int VehicleId)
        {
            Vehicle user = DbContext.Vehicles.Where(i => i.Number == VehicleNumber && i.Id != VehicleId).FirstOrDefault();
            if (user != null)
                return true;
            else
                return false;
        }
        public ActionResult ChangeVehicleStatus(int VehicleId)
        {
            Vehicle vehicle = DbContext.Vehicles.Where(i => i.Id == VehicleId).FirstOrDefault();
            if (vehicle != null)
            {
                if (vehicle.IsActive == true)
                {
                    // For Deactivate flag
                    TempData["AlertFlag"] = 3;
                    vehicle.IsActive = false;
                    DbContext.SaveChanges();
                    return RedirectToAction("VehiclesIndex");
                }
                else
                {
                    // For Activate flag
                    TempData["AlertFlag"] = 4;
                    vehicle.IsActive = true;
                    DbContext.SaveChanges();
                    return RedirectToAction("VehiclesIndex");
                }
            }
            // Default Flag
            TempData["AlertFlag"] = 0;
            return RedirectToAction("VehiclesIndex");
        }
    }
}