using CarRentManagementSystem_DomainClasses.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teqways_Com_CarRentManagementSystem.RequestHelper
{
    public class LocalCom
    {
        private CarRentManagementSystem_DAL.DAL.LocalDBHelper _DBHepler = new CarRentManagementSystem_DAL.DAL.LocalDBHelper();
        public string Login(string Email, string Password, string RememberMe)
        {
            return _DBHepler.Login(Email, Password, RememberMe);
        }

        public string AddNewUser(UserViewModel User)
        {
            return _DBHepler.AddNewUser(JsonConvert.SerializeObject(User));
        }
        public string AddNewDriver(DriverViewModel driver)
        {
            return _DBHepler.AddNewDriver(JsonConvert.SerializeObject(driver));
        }
        public string UpdateUserDetails(UserViewModel User)
        {
            return _DBHepler.UpdateUserDetails(JsonConvert.SerializeObject(User));
        }
        public string GetAllUsers()
        {
            return _DBHepler.GetAllUsers();
        }
        public string GetAllDrivers()
        {
            return _DBHepler.GetAllDrivers();
        }


        public string AddNewVehicle(VehicleViewModel Vehicle)
        {
            return _DBHepler.AddNewVehicle(JsonConvert.SerializeObject(Vehicle));
        }
        //public string UpdateVehicleDetails(VehicleViewModel Vehicle)
        //{
        //    return _DBHepler.UpdateVehicleDetails(JsonConvert.SerializeObject(Vehicle));
        //}
        public string GetAllVehicles()
        {
            return _DBHepler.GetAllVehicles();
        }
        public long AddUpdateBooking(string model)
        {
            return _DBHepler.AddUpdateBooking(model);
        }
        public long AddUpdateReceiving(string model)
        {
            return _DBHepler.AddUpdateReceiving(model);
        }
        public string GetBookingDetails(long id)
        {
            return _DBHepler.GetBookingDetails(id);
        }
        public string GetBookingCashDetails(long id)
        {
            return _DBHepler.GetBookingCashDetails(id);
        }
        public string GetAllBookings()
        {
            return _DBHepler.GetAllBookings();
        }

    }
}