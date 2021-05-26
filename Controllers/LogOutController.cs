using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstProject_CRUD_.Controllers
{
    public class LogOutController : Controller
    {
        // GET: LogOut
        public ActionResult Index()
        {
            Session["tokenNumber"] = null;
            Session["userId"] = null;

            return RedirectToAction("Index", "Home");
            //return View();
        }
    }
}