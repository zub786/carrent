using CarRentManagementSystem_DAL.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teqways_Com_CarRentManagementSystem.Filters;
using CarRentManagementSystem_DomainClasses.Models;
using System.Web.Script.Serialization;

namespace Teqways_Com_CarRentManagementSystem.Controllers
{
    [IsAdminLoggedIn]
    public class BookingController : Controller
    {
        public RequestHelper.LocalCom _LocalCom = new RequestHelper.LocalCom();
        private CarRentManagementSystemEntities DbContext = new CarRentManagementSystemEntities();
        // GET: Booking
        public ActionResult Create()
        {
            try
            {
                BookingViewModel model = new BookingViewModel();
                model.Vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(_LocalCom.GetAllVehicles()).ToList().Select(y =>
                new SelectListItem
                {
                    Value = y.Id.ToString(),
                    Text = y.Name
                });
                model.Driver1List = JsonConvert.DeserializeObject<List<Driver>>(_LocalCom.GetAllDrivers()).ToList().Select(y =>
                    new SelectListItem
                    {
                        Value = y.Id.ToString(),
                        Text = y.FirstName + " " + y.LastName
                    });
                model.Driver2List = JsonConvert.DeserializeObject<List<Driver>>(_LocalCom.GetAllDrivers()).ToList().Select(y =>
                    new SelectListItem
                    {
                        Value = y.Id.ToString(),
                        Text = y.FirstName + " " + y.LastName
                    });
                model.BookingFromDate = DateTime.Now.ToShortDateString();
                return View(model);
            }

            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
        }
        // GET: Booking
        public ActionResult Index()
        {
            try
            {
                var bookings = JsonConvert.DeserializeObject<List<Booking>>(_LocalCom.GetAllBookings());
                return View(bookings);
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
                model.BookedBy_FK = Convert.ToInt32(Request.Cookies["RememberMeUserInfo"]["Id"]);
                long BookingId = _LocalCom.AddUpdateBooking(JsonConvert.SerializeObject(model));
                if (BookingId > 0)
                {
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

                model.Vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(_LocalCom.GetAllVehicles()).ToList().Select(y =>
               new SelectListItem
               {
                   Value = y.Id.ToString(),
                   Text = y.Name,
                   Selected = (y.Id == model.VehicleId_Fk)
               });
                model.Driver1List = JsonConvert.DeserializeObject<List<Driver>>(_LocalCom.GetAllDrivers()).ToList().Select(y =>
                    new SelectListItem
                    {
                        Value = y.Id.ToString(),
                        Text = y.FirstName + " " + y.LastName,
                        Selected = (y.Id == model.Driver1Id_FK)
                    });
                model.Driver2List = JsonConvert.DeserializeObject<List<Driver>>(_LocalCom.GetAllDrivers()).ToList().Select(y =>
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
        #region CashReceiving
        public ActionResult ReceiveCash()
        {
            try
            {
                return View();
            }

            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
        }
        public bool isBookingExist(long BookingId)
        {
            return DbContext.Bookings.Any(b => b.Id == BookingId);
            
        }
        public object GetBookingDetailsWithCashDetails(long BookingId)
        {
            BookingCashReceivingViewModel ifNotFoundBooking = new BookingCashReceivingViewModel();
            ifNotFoundBooking.isRecordNotFound = true;
            if (!isBookingExist(BookingId)) return JsonConvert.SerializeObject(ifNotFoundBooking);
            else
            {

                var booking = JsonConvert.DeserializeObject<Booking>(_LocalCom.GetBookingDetails(BookingId));
                var receivings = JsonConvert.DeserializeObject<List<CashReceiving>>(_LocalCom.GetBookingCashDetails(BookingId));
                BookingCashReceivingViewModel BookingDetails = new BookingCashReceivingViewModel();
                BookingDetails.CustomerName = booking.CustomerName;
                BookingDetails.BookingAmount = (int)booking.RentAmount;
                BookingDetails.TotalReceived = receivings.Sum(c => c.ReceivedAmount);
                BookingDetails.CashDetails = receivings;
                return JsonConvert.SerializeObject(BookingDetails);
            }
        }
        public object AddOrUpdateReceiving(string modelString)
        {
            BookingCashReceivingViewModel receiving = new BookingCashReceivingViewModel();
            receiving = JsonConvert.DeserializeObject<BookingCashReceivingViewModel>(modelString);
            CashReceiving transaction = new CashReceiving();
            if (receiving.EditReceivingId > 0)
            {
                transaction.Id = receiving.EditReceivingId;
            }
            
            transaction.BookingId_FK = receiving.BookingNo;
            transaction.PaymentType = receiving.PaymentType;
            transaction.ReceivedAmount = (int)receiving.NewReceiveAmount;
            transaction.RemainingAmount = (int)(receiving.BookingAmount - (receiving.TotalReceived + receiving.NewReceiveAmount));
            transaction.ReceivedOn = DateTime.Now;
            transaction.ReceivedBy_FK = Convert.ToInt32(Request.Cookies["RememberMeUserInfo"]["UserId"]);
            long BookingId = _LocalCom.AddUpdateReceiving(JsonConvert.SerializeObject(transaction));
            return GetBookingDetailsWithCashDetails(receiving.BookingNo);
        }

        
        #endregion
    }
}