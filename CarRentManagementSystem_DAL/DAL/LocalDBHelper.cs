using CarRentManagementSystem_DomainClasses.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarRentManagementSystem_DAL.DAL
{
    public class LocalDBHelper
    {
        private CarRentManagementSystemEntities DbContext = new CarRentManagementSystemEntities();

        public string Login(string Email, string Password, string RememberMe)
        {

            var TempUser = DbContext.Users.FirstOrDefault(i => i.EmailAddress == Email);

            if (TempUser != null)
            {
                if (TempUser.IsActive == true)
                {
                    var TempUserCheckPassword = DbContext.Users.FirstOrDefault(i => i.EmailAddress == Email && i.Password == Password);

                    if (TempUserCheckPassword != null)
                    {
                        User u = new User();
                        u.Id = TempUserCheckPassword.Id;
                        u.FirstName = TempUserCheckPassword.FirstName;
                        u.LastName = TempUserCheckPassword.LastName;
                        u.UserName = TempUserCheckPassword.UserName;
                        u.EmailAddress = TempUserCheckPassword.EmailAddress;
                        u.Password = TempUserCheckPassword.Password;

                        // Authenticated
                        string result = JsonConvert.SerializeObject(u);
                        return result;
                    }
                    else
                    {
                        // Incorrect Password
                        return "2";
                    }
                }
                else
                {
                    // User is not active 
                    return "4";
                }
            }
            else
            {
                // User not found against given email id
                return "3";
            }

        }

        public string AddNewUser(string user)
        {
            UserViewModel u = JsonConvert.DeserializeObject<UserViewModel>(user);
            User DBUser = new User();
            DBUser.FirstName = u.FirstName;
            DBUser.LastName = u.LastName;
            DBUser.UserName = u.UserName;
            DBUser.EmailAddress = u.EmailAddress;
            DBUser.Password = u.Password;
            DBUser.IsActive = u.IsActive;
            DBUser.CreatedBy_FK = u.CreatedBy_FK;
            DBUser.PasswordHash = u.PasswordHash;
            if (!DbContext.Users.Any(i => i.EmailAddress == u.EmailAddress))
            {
                DbContext.Users.Add(DBUser);
                DbContext.SaveChanges();
                return "Done";
            }
            else
            {
                return "Exist";
            }
        }
        public string UpdateUserDetails(string user)
        {

            UserViewModel u = JsonConvert.DeserializeObject<UserViewModel>(user);
            User DBUser = new User();
            DBUser = DbContext.Users.FirstOrDefault(i => i.Id == u.Id || i.Id == u.Id);

            if (DBUser.EmailAddress != null && DBUser.EmailAddress != "")
            {
                if (DbContext.Users.Any(i => i.EmailAddress == u.EmailAddress && i.Id != u.Id))
                {
                    return "exist";
                }
                else
                {
                    DBUser.UserName = u.UserName;
                    DBUser.EmailAddress = u.EmailAddress;
                    DBUser.Password = u.Password;
                    DBUser.IsActive = u.IsActive;
                    DBUser.PasswordHash = u.Password + "-HASH";
                    DbContext.SaveChanges();
                    return "Done";
                }
            }
            else
            {
                return "User Not found";
            }
        }
        public string GetAllUsers()
        {
            return JsonConvert.SerializeObject(DbContext.Users.ToList());
        }
        public string GetAllDrivers()
        {
            return JsonConvert.SerializeObject(DbContext.Drivers.ToList());
        }
        public string AddNewDriver(string driver)
        {
            DriverViewModel v = JsonConvert.DeserializeObject<DriverViewModel>(driver);
            Driver DBUser = new Driver();
            DBUser.FirstName = v.FirstName;
            DBUser.LastName = v.LastName;
            DBUser.ContactNo = v.ContactNo;
            DBUser.CNIC = v.CNIC;
            DBUser.Address = v.Address;
            DBUser.LicenseNo = v.LicenseNo;
            DBUser.LicenseType = v.LicenseType;
            DBUser.City = v.City;
            DBUser.ExperienceInYears = v.ExperienceInYears;
            DbContext.Drivers.Add(DBUser);
            DbContext.SaveChanges();
            return "Done";
        }

        public string AddNewVehicle(string Vehicle)
        {
            VehicleViewModel v = JsonConvert.DeserializeObject<VehicleViewModel>(Vehicle);
            Vehicle DBVehicle = new Vehicle();
            DBVehicle.Name = v.Name;
            DBVehicle.Model = v.Model;
            DBVehicle.RegistredIn = v.RegistredIn;
            DBVehicle.Color = v.Color;
            DBVehicle.Number = v.Number;
            DBVehicle.IsActive = true;
            if (!DbContext.Vehicles.Any(i => i.Number == v.Number))
            {
                DbContext.Vehicles.Add(DBVehicle);
                DbContext.SaveChanges();
                return "Done";
            }
            else
            {
                return "Exist";
            }
        }

        public string GetAllVehicles()
        {
            return JsonConvert.SerializeObject(DbContext.Vehicles.ToList());
        }

        public long AddUpdateBooking(string model)
        {
            Booking Booking = JsonConvert.DeserializeObject<Booking>(model);
            if (DbContext.Bookings.Any(i => i.Id == Booking.Id))
            {
                DbContext.Entry(Booking).State = EntityState.Modified;
                DbContext.SaveChanges();
                return Booking.Id;
            }
            DbContext.Bookings.Add(Booking);
            DbContext.SaveChanges();

            return Booking.Id;
        }
        public long AddUpdateReceiving(string model)
        {
            CashReceiving Receiving = JsonConvert.DeserializeObject<CashReceiving>(model);
            if (DbContext.CashReceivings.Any(i => i.Id == Receiving.Id))
            {
                DbContext.Entry(Receiving).State = EntityState.Modified;
                DbContext.SaveChanges();
                return Receiving.Id;
            }
            DbContext.CashReceivings.Add(Receiving);
            DbContext.SaveChanges();

            return Receiving.Id;
        }

        public string GetBookingDetails(long id)
        {
            return JsonConvert.SerializeObject(DbContext.Bookings.FirstOrDefault(i => i.Id == id));
        }
        public string GetBookingCashDetails(long id)
        {
            return JsonConvert.SerializeObject(DbContext.CashReceivings.Where(i => i.BookingId_FK == id));
        }
        public string GetAllBookings()
        {
            return JsonConvert.SerializeObject(DbContext.Bookings.ToList());
        }
        

    }
}