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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult First()
        {
            ViewBag.Message = "Welcome Here.";

            return View();
        }
    }
}