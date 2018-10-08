using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using theraphy.Models;
using theraphy.Controllers;
using System.IO;

namespace theraphy.Controllers
{
    public class AdminController : Controller
    {
       
        Database1Entities3 db = new Database1Entities3();
       
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(login data)
        {
            try
            {
                var users = db.logins.Where(i => i.EMAIL == data.EMAIL && i.PASSWORD == data.PASSWORD).FirstOrDefault();
               
                if (users == null)
                {
                  
                    TempData["msg"] = "Email id or Password is Wrong";
                   
                    return RedirectToAction("Login");
                }
                else if (users.IS_ACTIVE == false)
                {
                    TempData["msg"] = "Your Account is Deactive Please Contact to administrator";
                    return RedirectToAction("Login");
                }

                else
                {
                    Session["login"] = users.ID;
                    Session["login2"] = users.NAME;
                    if (users.ID > 0)
                    {
                        users.LAST_LOGIN = DateTime.Now;
                    }
                    db.SaveChanges();
                    return RedirectToAction("configuration");
                }
                  
               
                
                
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [AllowAnonymous]
        public ActionResult logout()
        {
            try
            {
                Session.Abandon();
                Session.Clear();
            }
            catch (Exception e)
            {

                throw e;
            }

            return RedirectToAction("Login");
        }

        public JsonResult delete(int id, string carousel_del, string configuration_del,string news_del, string ourteam_del,string patient_del,string service_del, string contact_del,string admin_del,string about_del)
        {
            try
            {
                if (configuration_del != null)
                {
                    var notification = db.configurations.Where(i => i.ID == id).SingleOrDefault();
                    db.configurations.Remove(notification);                   
                    db.SaveChanges();
                    return Json(new { data = notification }, JsonRequestBehavior.AllowGet);

                }
               
                else if (carousel_del != null)
                {
                    var caro = db.carousels.Where(i => i.ID == id).SingleOrDefault();                
                    db.carousels.Remove(caro);
                    db.SaveChanges();
                    return Json(new { data = caro }, JsonRequestBehavior.AllowGet);
                }

                else if (news_del != null)
                {
                    var news = db.news.Where(i => i.ID == id).SingleOrDefault();               
                    db.news.Remove(news);
                    db.SaveChanges();
                    return Json(new { data = news }, JsonRequestBehavior.AllowGet);
                }
                else if (ourteam_del != null)
                {
                    var team = db.our_team.Where(i => i.ID == id).SingleOrDefault();                 
                    db.our_team.Remove(team);
                    db.SaveChanges();
                    return Json(new { data = team }, JsonRequestBehavior.AllowGet);
                }
                else if (patient_del != null)
                {
                    var patien = db.patients.Where(i => i.ID == id).SingleOrDefault();
                    db.patients.Remove(patien);
                    db.SaveChanges();
                    return Json(new { data = patien }, JsonRequestBehavior.AllowGet);
                }
                else if (service_del != null)
                {
                    var servi = db.services.Where(i => i.ID == id).SingleOrDefault();
                    db.services.Remove(servi);
                    db.SaveChanges();
                    return Json(new { data = servi }, JsonRequestBehavior.AllowGet);
                }
                else if (contact_del != null)
                {
                    var cont = db.CONTACTs.Where(i => i.ID == id).SingleOrDefault();
                    db.CONTACTs.Remove(cont);
                    db.SaveChanges();
                    return Json(new { data = cont }, JsonRequestBehavior.AllowGet);
                }
                else if (admin_del != null)
                {
                    var cont = db.logins.Where(i => i.ID == id).SingleOrDefault();
                    db.logins.Remove(cont);
                    db.SaveChanges();
                    return Json(new { data = cont }, JsonRequestBehavior.AllowGet);
                }
                else if (about_del != null)
                {
                    var cont = db.about_us.Where(i => i.ID == id).SingleOrDefault();
                    db.about_us.Remove(cont);
                    db.SaveChanges();
                    return Json(new { data = cont }, JsonRequestBehavior.AllowGet);
                }
                return Json("");
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public ActionResult configuration()
        {
            var confi = db.configurations.FirstOrDefault();
            ViewBag.configur = confi;
            return View(confi);
        }

       
        [HttpPost]
        public ActionResult configuration(configuration data, HttpPostedFileBase[] files)
        {
            var login = Convert.ToInt16(Session["login"]);
            var conf = db.configurations.FirstOrDefault();
                if(conf != null)
                {
                    conf.UPDATED_DATE = DateTime.Now;
                conf.UPDATED_BY = login;

                if (data.EMAIL != null)
                    {
                        conf.EMAIL = data.EMAIL;
                    }
                    if (data.CONTACT != null)
                    {
                        conf.CONTACT = data.CONTACT;
                    }
                    if (data.TIMING != null)
                    {
                        conf.TIMING = data.TIMING;
                    }
                    if (data.CLINIC_ADDRESS != null)
                    {
                        conf.CLINIC_ADDRESS = data.CLINIC_ADDRESS;
                    }
                    if (data.CLINIC_EMAIL != null)
                    {
                        conf.CLINIC_EMAIL = data.CLINIC_EMAIL;
                    }
                    if (data.CLINIC_CONTACT != null)
                    {
                        conf.CLINIC_CONTACT = data.CLINIC_CONTACT;
                    }
                    if (data.LOGO_IMAGE != null)
                    {
                        conf.LOGO_IMAGE = data.LOGO_IMAGE;
                    }
                    if (data.FACEBOOK != null)
                    {
                        conf.FACEBOOK = data.FACEBOOK;
                    }
                    if (data.TWITTER != null)
                    {
                        conf.TWITTER = data.TWITTER;
                    }
                    if (data.GOOGLE_PLUS != null)
                    {
                        conf.GOOGLE_PLUS = data.GOOGLE_PLUS;
                    }
                    if (data.LINKED_IN != null)
                    {
                        conf.LINKED_IN = data.LINKED_IN;
                    }
                    if (data.YOUTUBE != null)
                    {
                        conf.YOUTUBE = data.YOUTUBE;
                    }

                    if (data.IS_ACTIVE != null)
                    {
                        var active = db.configurations.ToList();
                        active.ForEach(i => i.IS_ACTIVE = false);               
                        conf.IS_ACTIVE = data.IS_ACTIVE;                                                                    
                    }
                    db.SaveChanges();
                }
            
            //else
            //{ 
            //data.CREATED_DATE = DateTime.Now;

            //    if (data.LOGO_IMAGE != null)
            //    {
            //        Models.configuration imgs = new configuration();
            //        imgs.LOGO_IMAGE = data.LOGO_IMAGE;
            //        HttpFileCollectionBase file = Request.Files;
            //        for (int i = 0; i < file.Count; i++)
            //        {
            //            HttpPostedFileBase Image = file[i];
            //            var filename = "";
            //            if (Image != null)
            //            {
            //                filename = Path.GetFileName(Image.FileName);
            //                var path3 = Path.Combine(Server.MapPath("~/images"), filename);
            //                Image.SaveAs(path3);
            //               data.LOGO_IMAGE = filename;
            //                db.SaveChanges();
            //            }
            //        }
            //    }
            //    var time = db.configurations.Add(data);
            //db.SaveChanges();
            //}
            return RedirectToAction("configuration");
            
        }

        public JsonResult getconfiguration(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.configurations.Where(i => i.ID == id).SingleOrDefault();
                return Json(new { data = confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        public ActionResult carousel()
        {
            var carou = db.carousels.ToList();
            ViewBag.carousel = carou;
            return View();
        }

        [HttpPost]
        public ActionResult carousel(carousel data, HttpPostedFileBase[] files)
        {
            var login = Convert.ToInt16(Session["login"]);
            if (data.ID > 0)
            {
                var conf = db.carousels.Where(i => i.ID == data.ID).SingleOrDefault();
                if (conf != null)
                {
                    conf.UPDATED_DATE = DateTime.Now;
                    conf.UPDATED_BY = login;
                    if (data.IMAGE != null)
                    {
                        
                        Models.carousel imgs = new carousel();
                        imgs.IMAGE = data.IMAGE;
                        HttpFileCollectionBase file = Request.Files;
                        for (int i = 0; i < file.Count; i++)
                        {
                            HttpPostedFileBase Image = file[i];
                            var filename = "";
                            if (Image != null)
                            {
                                filename = Path.GetFileName(Image.FileName);
                                var path3 = Path.Combine(Server.MapPath("~/images"), filename);
                                Image.SaveAs(path3);
                                data.IMAGE = filename;
                                conf.IMAGE = data.IMAGE;
                            }
                        }
                        
                    }
                    if (data.DESCRIPTION != null)
                    {
                        conf.DESCRIPTION = data.DESCRIPTION;
                    }
                    if (data.IS_ACTIVE != null)
                    {
                        conf.IS_ACTIVE = data.IS_ACTIVE;
                    }
                    db.SaveChanges();
                }
            }
            else
            {               

                if (data.IMAGE != null)
                {
                    Models.carousel imgs = new carousel();
                    imgs.IMAGE = data.IMAGE;
                    HttpFileCollectionBase file = Request.Files;
                    for (int i = 0; i < file.Count; i++)
                    {
                        HttpPostedFileBase Image = file[i];
                        var filename = "";
                        if (Image != null)
                        {
                            filename = Path.GetFileName(Image.FileName);
                            var path3 = Path.Combine(Server.MapPath("~/images"), filename);
                            Image.SaveAs(path3);
                            data.IMAGE = filename;
                            db.SaveChanges();
                        }
                    }
                }
                data.CREATED_DATE = DateTime.Now;
                data.CREATED_BY = login;
                data.IS_DELETED = false;             
                var time = db.carousels.Add(data);
                db.SaveChanges();
            }
            return RedirectToAction("carousel");

        }

        public JsonResult getcarousel(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.carousels.Where(i => i.ID == id).SingleOrDefault();
                return Json(new { data = confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        public ActionResult news()
        {
            var news = db.news.ToList();
            ViewBag.news1 = news;
            return View();
        }

        [HttpPost]
        public ActionResult news(news data, HttpPostedFileBase[] files)
        {
            var login = Convert.ToInt16(Session["login"]);
           
            if (data.ID > 0)
            {
                var conf = db.news.Where(i => i.ID == data.ID).SingleOrDefault();
                if (conf != null)
                {
                    conf.UPDATED_DATE = DateTime.Now;
                    conf.UPDATED_BY = login;
                    conf.UPDATED_BY = login;
                    if (data.NEWS1 != null)
                    {
                        conf.NEWS1 = data.NEWS1;
                    }
                    if (data.IMAGE != null)
                    {

                        Models.carousel imgs = new carousel();
                        imgs.IMAGE = data.IMAGE;
                        HttpFileCollectionBase file = Request.Files;
                        for (int i = 0; i < file.Count; i++)
                        {
                            HttpPostedFileBase Image = file[i];
                            var filename = "";
                            if (Image != null)
                            {
                                filename = Path.GetFileName(Image.FileName);
                                var path3 = Path.Combine(Server.MapPath("~/images"), filename);
                                Image.SaveAs(path3);
                                data.IMAGE = filename;
                                conf.IMAGE = data.IMAGE;
                            }
                        }

                    }

                    if (data.NEWS_DESCRIPTION != null)
                    {
                        conf.NEWS_DESCRIPTION = data.NEWS_DESCRIPTION;
                    }
                    if (data.IS_ACTIVE != null)
                    {
                        conf.IS_ACTIVE = data.IS_ACTIVE;
                    }
                    db.SaveChanges();
                }
            }
            else
            {
                
                if (data.IMAGE != null)
                {
                    Models.news imgs = new news();
                    imgs.IMAGE = data.IMAGE;
                    HttpFileCollectionBase file = Request.Files;
                    for (int i = 0; i < file.Count; i++)
                    {
                        HttpPostedFileBase Image = file[i];
                        var filename = "";
                        if (Image != null)
                        {
                            filename = Path.GetFileName(Image.FileName);
                            var path3 = Path.Combine(Server.MapPath("~/images"), filename);
                            Image.SaveAs(path3);
                            data.IMAGE = filename;
                            db.SaveChanges();
                        }
                    }
                }
                data.CREATED_DATE = DateTime.Now;
                data.CREATED_BY = login;
                data.IS_DELETED = false;
            
                var time = db.news.Add(data);
                db.SaveChanges();
            }
            return RedirectToAction("news");

        }

        public JsonResult getnews(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.news.Where(i => i.ID == id).SingleOrDefault();
                return Json(new { data = confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public ActionResult ourteam()
        {
            
            var team = db.our_team.ToList();
            ViewBag.team = team;
            return View();
        }

        [HttpPost]
        public ActionResult ourteam(our_team data, HttpPostedFileBase[] files)
        {
            var login = Convert.ToInt16(Session["login"]);
            if (data.ID > 0)
            {
                var conf = db.our_team.Where(i => i.ID == data.ID).SingleOrDefault();
                if (conf != null)
                {
                    conf.UPDATED_DATE = DateTime.Now;
                    conf.UPDATED_BY = login;
                    if (data.NAME != null)
                    {
                        conf.NAME = data.NAME;
                    }
                    if (data.IMAGE != null)
                    {

                        Models.carousel imgs = new carousel();
                        imgs.IMAGE = data.IMAGE;
                        HttpFileCollectionBase file = Request.Files;
                        for (int i = 0; i < file.Count; i++)
                        {
                            HttpPostedFileBase Image = file[i];
                            var filename = "";
                            if (Image != null)
                            {
                                filename = Path.GetFileName(Image.FileName);
                                var path3 = Path.Combine(Server.MapPath("~/images"), filename);
                                Image.SaveAs(path3);
                                data.IMAGE = filename;
                                conf.IMAGE = data.IMAGE;
                            }
                        }
                        
                    }
                    conf.DEGREE = data.DEGREE;       
                      conf.EXPERIENCE = data.EXPERIENCE;
                    conf.SPECIALITY = data.SPECIALITY;
                    if (data.IS_ACTIVE != null)
                    {
                        conf.IS_ACTIVE = data.IS_ACTIVE;
                    }

                    db.SaveChanges();
                }
            }
            else
            {
              

                if (data.IMAGE != null)
                {
                    Models.news imgs = new news();
                    imgs.IMAGE = data.IMAGE;
                    HttpFileCollectionBase file = Request.Files;
                    for (int i = 0; i < file.Count; i++)
                    {
                        HttpPostedFileBase Image = file[i];
                        var filename = "";
                        if (Image != null)
                        {
                            filename = Path.GetFileName(Image.FileName);
                            var path3 = Path.Combine(Server.MapPath("~/images"), filename);
                            Image.SaveAs(path3);
                            data.IMAGE = filename;
                            db.SaveChanges();
                        }
                    }
                }
                data.CREATED_DATE = DateTime.Now;
                data.CREATED_BY = login;
                data.IS_DELETED = false;
               
                var time = db.our_team.Add(data);
                db.SaveChanges();
            }
            return RedirectToAction("ourteam");

        }

        public JsonResult getourteam(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.our_team.Where(i => i.ID == id).SingleOrDefault();
                return Json(new { data = confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        public ActionResult patient()
        {
            var patie = db.patients.ToList();
            ViewBag.patient = patie;
            return View();
        }

        [HttpPost]
        public ActionResult patient(patient data)
        {
            var login = Convert.ToInt16(Session["login"]);
            if (data.ID > 0)
            {
                var conf = db.patients.Where(i => i.ID == data.ID).SingleOrDefault();
                if (conf != null)
                {
                    conf.UPDATED_DATE = DateTime.Now;
                    conf.UPDATED_BY = login;
                    if (data.PATIENT_NAME != null)
                    {
                        conf.PATIENT_NAME = data.PATIENT_NAME;
                    }
                    conf.PATIENT_OF = data.PATIENT_OF;
                    conf.PATIENT_TESTIMONIAL = data.PATIENT_TESTIMONIAL;
                    conf.PATIENT_AGE = data.PATIENT_AGE;
                     if (data.IS_ACTIVE != null)
                    {
                        conf.IS_ACTIVE = data.IS_ACTIVE;
                    }
                    db.SaveChanges();
                }
            }
            else
            {
                data.CREATED_DATE = DateTime.Now;
                data.CREATED_BY = login;
                data.IS_DELETED = false;
              
                var time = db.patients.Add(data);
                db.SaveChanges();
            }
            return RedirectToAction("patient");

        }

        public JsonResult getpatient(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.patients.Where(i => i.ID == id).SingleOrDefault();
                return Json(new { data = confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public ActionResult service()
        {
            var ser = db.services.ToList();
            ViewBag.service = ser;
            return View();
        }

        [HttpPost]
        public ActionResult service(service data)
        {
            var login = Convert.ToInt16(Session["login"]);
            if (data.ID > 0)
            {
                var conf = db.services.Where(i => i.ID == data.ID).SingleOrDefault();
                if (conf != null)
                {
                    conf.UPDATED_DATE = DateTime.Now;
                    conf.UPDATED_BY = login;
                    if (data.SERVICE_TYPE != null)
                    {
                        conf.SERVICE_TYPE = data.SERVICE_TYPE;
                    }
                    if (data.IMAGE != null)
                    {

                        Models.carousel imgs = new carousel();
                        imgs.IMAGE = data.IMAGE;
                        HttpFileCollectionBase file = Request.Files;
                        for (int i = 0; i < file.Count; i++)
                        {
                            HttpPostedFileBase Image = file[i];
                            var filename = "";
                            if (Image != null)
                            {
                                filename = Path.GetFileName(Image.FileName);
                                var path3 = Path.Combine(Server.MapPath("~/images"), filename);
                                Image.SaveAs(path3);
                                data.IMAGE = filename;
                                conf.IMAGE = data.IMAGE;
                            }
                        }

                    }
                    conf.SERVICE_NAME = data.SERVICE_NAME;
                    conf.DESCRIPTION = data.DESCRIPTION;
                    if (data.IS_ACTIVE != null)
                    {
                        conf.IS_ACTIVE = data.IS_ACTIVE;
                    }
                    db.SaveChanges();
                }
            }
            else
            {
               
                if (data.IMAGE != null)
                {
                    Models.news imgs = new news();
                    imgs.IMAGE = data.IMAGE;
                    HttpFileCollectionBase file = Request.Files;
                    for (int i = 0; i < file.Count; i++)
                    {
                        HttpPostedFileBase Image = file[i];
                        var filename = "";
                        if (Image != null)
                        {
                            filename = Path.GetFileName(Image.FileName);
                            var path3 = Path.Combine(Server.MapPath("~/images"), filename);
                            Image.SaveAs(path3);
                            data.IMAGE = filename;
                            db.SaveChanges();
                        }
                    }
                }
                data.CREATED_DATE = DateTime.Now;
                data.CREATED_BY = login;
               
                data.IS_DELETED = false;
                var time = db.services.Add(data);
                db.SaveChanges();
            }
            return RedirectToAction("service");

        }

        public JsonResult getservice(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.services.Where(i => i.ID == id).SingleOrDefault();
                return Json(new { data = confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public ActionResult contact()
        {
            var con = db.CONTACTs.ToList();
            ViewBag.contact = con;
            return View();
        }

        [HttpPost]
        public ActionResult contact(CONTACT data)
        {
            if (data.ID > 0)
            {
                var conf = db.CONTACTs.Where(i => i.ID == data.ID).SingleOrDefault();
                if (conf != null)
                {
                    conf.UPDATED_DATE = DateTime.Now;
                    if (data.NAME != null)
                    {
                        conf.NAME = data.NAME;
                    }
                    conf.PHONE = data.PHONE;
                    conf.EMAIL = data.EMAIL;
                    conf.SUBJECT = data.SUBJECT;
                    conf.MESSAGE = data.MESSAGE;

                    db.SaveChanges();
                }
            }
            else
            {
                data.CREATED_DATE = DateTime.Now;
              
                var time = db.CONTACTs.Add(data);
                db.SaveChanges();
            }
            return RedirectToAction("contact");

        }

        public JsonResult getcontact(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.CONTACTs.Where(i => i.ID == id).SingleOrDefault();
                return Json(new { data = confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        public ActionResult admin()
        {
            var adm = db.logins.ToList();
            ViewBag.admin = adm;
            return View();
        }

        [HttpPost]
        public ActionResult admin(login data)
        {
            var login = Convert.ToInt16(Session["login"]);
            if (data.ID > 0)
            {
                var conf = db.logins.Where(i => i.ID == data.ID).SingleOrDefault();
                if (conf != null)
                {
                    conf.UPDATED_DATE = DateTime.Now;
                    if(data.ID !=2)
                    { 
                    conf.UPDATED_BY = login;
                    }
                    if (data.NAME != null)
                    {
                        conf.NAME = data.NAME;
                    }
                   
                    conf.EMAIL = data.EMAIL;
                    conf.USERNAME = data.USERNAME;
                    if (data.PASSWORD != null)
                    {
                        conf.PASSWORD = data.PASSWORD;
                    }
                    if (data.IS_ACTIVE != null)
                    {
                        conf.IS_ACTIVE = data.IS_ACTIVE;
                    }
                    db.SaveChanges();
                }
            }
            else
            {
                data.CREATED_DATE = DateTime.Now;
                data.CREATED_BY = login;
                data.IS_DELETED = false;
             
                var time = db.logins.Add(data);
                db.SaveChanges();
            }
            return RedirectToAction("admin");

        }

        public JsonResult getadmin(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.logins.Where(i => i.ID == id).SingleOrDefault();
                return Json(new { data = confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public ActionResult about()
        {
            var aboutus = db.about_us.FirstOrDefault();
            ViewBag.about = aboutus;
            return View(aboutus);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult about(about_us data)
        {
            var login = Convert.ToInt16(Session["login"]);
            var conf = db.about_us.FirstOrDefault();
                if (conf != null)
                {
                    conf.UPDATED_DATE = DateTime.Now;
                conf.UPDATED_BY = login;
                    if (data.ABOUT_US1 != null)
                    {
                        conf.ABOUT_US1 = data.ABOUT_US1;
                    }
                    if (data.DESCRIPTION != null)
                    {
                      conf.DESCRIPTION = data.DESCRIPTION;
                    }
                db.SaveChanges();
                }
            
           
            return RedirectToAction("about");

        }

        public JsonResult getabout()
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.about_us.FirstOrDefault();
                return Json(new { data = confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public ActionResult tabledata()
        {
            return View();
        }
        [HttpPost]
        public ActionResult tabledata(carousel data)
        {

            return Redirect("tabledata");
        }


    }
}