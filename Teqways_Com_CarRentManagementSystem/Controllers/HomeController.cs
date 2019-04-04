using CarRentManagementSystem_DAL.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Teqways_Com_CarRentManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private CarRentManagementSystem_DAL.DAL.CarRentManagementSystemEntities DbContext = new CarRentManagementSystem_DAL.DAL.CarRentManagementSystemEntities();
        public RequestHelper.LocalCom _LocalCom = new RequestHelper.LocalCom();

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();

        }
       

    }
}