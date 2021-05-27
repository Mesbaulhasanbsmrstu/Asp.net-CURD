using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstProject_CRUD_.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("First", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult About()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Index", "Login");

            }
            else { 
            ViewBag.Message = "Your application description page.";

            return View();
        }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult First()
        {
            if (Session["userId"] == null)
            {
                ViewBag.Message = "Welcome Here.";

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
      
        
    }
}