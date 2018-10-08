using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using theraphy.Models;
using Newtonsoft.Json;

using theraphy.Controllers;

namespace theraphy.Controllers
{
  
    public class HomeController : Controller
    {            
        Database1Entities3 db = new Database1Entities3();
        
        public ActionResult Index()
        {
        
            information();
            return View();
        }

        public ActionResult aboutus()
        {
            information();
            var ab = db.about_us.FirstOrDefault();
            ViewBag.about = ab;
            return View();
        }

        public ActionResult servicelist()
        {
            information();
            var ser = db.services.Where(i=>i.IS_ACTIVE==true).ToList();
            ViewBag.service = ser;
            return View();
        }
        public ActionResult servicedetail()
        {
            information();
              var ser = db.services.Where(i => i.IS_ACTIVE == true).ToList();
            ViewBag.service = ser;
            return View();
        }
        public ActionResult news()
        {
            information();
            var new1 = db.news.Where(i => i.IS_ACTIVE == true).ToList();
            ViewBag.news = new1;
            return View();
        }


        public ActionResult contact()
        {
            information();
            return View();
        }
        [HttpPost]
        public ActionResult contact(CONTACT data)
        {
            information();
            data.CREATED_DATE = DateTime.Now;
            var cont = db.CONTACTs.Add(data);
            db.SaveChanges();
            return View();
        }

        public ActionResult Home()
        {
            try
            {
                var info = db.configurations.FirstOrDefault();
                ViewBag.information = info;
                var carouse = db.carousels.Where(i => i.IS_ACTIVE == true).ToList();
                ViewBag.carousel = carouse;
                var ser = db.services.Where(i => i.IS_ACTIVE == true).ToList();
                ViewBag.service   = ser;
                var team1 = db.our_team.Where(i => i.IS_ACTIVE == true).ToList();
                ViewBag.team = team1;
                var patie = db.patients.Where(i => i.IS_ACTIVE == true).ToList();
                ViewBag.patient = patie;
                var new1 = db.news.Where(i => i.IS_ACTIVE == true).ToList();
                ViewBag.news = new1;
            }
            catch (Exception e)
            {
                throw e;
            }
            return View();
        }

        public JsonResult getcarousel()
        { 
            db.Configuration.ProxyCreationEnabled = false;
            var carousel = db.carousels.ToList();
          //  ViewBag.carou = carousel;     

                return Json(new { data = carousel }, JsonRequestBehavior.AllowGet);
        }
        public void information()
        {
            try
            {
                var info = db.configurations.FirstOrDefault();
                ViewBag.information = info;
                var ser = db.services.ToList();
                ViewBag.service = ser;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}