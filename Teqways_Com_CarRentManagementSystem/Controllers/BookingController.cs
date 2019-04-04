using CarRentManagementSystem_DAL.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teqways_Com_CarRentManagementSystem.Filters;
using CarRentManagementSystem_DomainClasses.Models;

namespace Teqways_Com_CarRentManagementSystem.Controllers
{
    [IsAdminLoggedIn]
    public class BookingController : Controller
    {
        public RequestHelper.LocalCom _LocalCom = new RequestHelper.LocalCom();

        // GET: Booking
        public ActionResult Create()
        {
            try
            {
                ViewBag.VehicleId_Fk = JsonConvert.DeserializeObject<List<Vehicle>>(_LocalCom.GetAllVehicles()).ToList().Select(y =>
                new SelectListItem
                {
                    Value = y.Id.ToString(),
                    Text = y.Name
                });
                ViewBag.Driver1Id_FK = JsonConvert.DeserializeObject<List<Driver>>(_LocalCom.GetAllDrivers()).ToList().Select(y =>
                    new SelectListItem
                    {
                        Value = y.Id.ToString(),
                        Text = y.FirstName + " " + y.LastName
                    });
                ViewBag.Driver2Id_FK = JsonConvert.DeserializeObject<List<Driver>>(_LocalCom.GetAllDrivers()).ToList().Select(y =>
                    new SelectListItem
                    {
                        Value = y.Id.ToString(),
                        Text = y.FirstName + " " + y.LastName
                    });
                return View();
            }

            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
        }

        [HttpPost]
        public ActionResult Create(BookingViewModel model)
        {
            try
            {
                model.BookedBy_FK = Convert.ToInt32(Request.Cookies["RememberMeUserInfo"]["EmailId"]);
                long BookingId = _LocalCom.AddUpdateBooking(JsonConvert.SerializeObject(model));
                if (BookingId > 0) {
                    TempData["AlertFlag"] = 1;
                    return RedirectToAction("Edit", new { id = BookingId });
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            try
            {
                
                BookingViewModel model = JsonConvert.DeserializeObject<BookingViewModel>(_LocalCom.GetBookingDetails(id));

                ViewBag.VehicleId_Fk = JsonConvert.DeserializeObject<List<Vehicle>>(_LocalCom.GetAllVehicles()).ToList().Select(y =>
               new SelectListItem
               {
                   Value = y.Id.ToString(),
                   Text = y.Name,
                   Selected = (y.Id == model.VehicleId_Fk)
               });
                ViewBag.Driver1Id_FK = JsonConvert.DeserializeObject<List<Driver>>(_LocalCom.GetAllDrivers()).ToList().Select(y =>
                    new SelectListItem
                    {
                        Value = y.Id.ToString(),
                        Text = y.FirstName + " " + y.LastName,
                        Selected = (y.Id == model.Driver1Id_FK)
                    });
                ViewBag.Driver2Id_FK = JsonConvert.DeserializeObject<List<Driver>>(_LocalCom.GetAllDrivers()).ToList().Select(y =>
                    new SelectListItem
                    {
                        Value = y.Id.ToString(),
                        Text = y.FirstName + " " + y.LastName,
                        Selected = (y.Id == model.Driver2Id_FK)
                    });
                return View(model);
            }

            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
        }
        [HttpPost]
        public ActionResult Edit(BookingViewModel model)
        {
            try
            {
                long BookingId = _LocalCom.AddUpdateBooking(JsonConvert.SerializeObject(model));
                if (BookingId > 0)
                {
                    TempData["AlertFlag"] = 2;
                    return RedirectToAction("Edit", new { id = BookingId });
                }
                return View();
            }

            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
        }
    }
}