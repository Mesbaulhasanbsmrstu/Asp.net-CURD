using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;

using MySql.Data.MySqlClient;
using FirstProject_CRUD_.Models;
using System.Configuration;
using System.IO;

namespace FirstProject_CRUD_.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user, HttpPostedFileBase file)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            MySqlConnection sqlConn = new MySqlConnection(mainconn);
            //string query = "select* from user";
            string query = "insert into user(name,email,password,gender,image) values(@name,@email,@password,@gender,@image)";
            sqlConn.Open();
            MySqlCommand cmd = new MySqlCommand(query, sqlConn);
            cmd.Parameters.AddWithValue("@name", user.name);
            cmd.Parameters.AddWithValue("@email", user.email);
            cmd.Parameters.AddWithValue("@password", user.password);
            cmd.Parameters.AddWithValue("@gender", user.gender);
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/UserImage/"), filename);
                file.SaveAs(imgpath);
                // cmd.Parameters.AddWithValue("@image", imgpath);
                cmd.Parameters.AddWithValue("@image", "~/UserImage/" + file.FileName);
            }

            MySqlDataReader sdr = cmd.ExecuteReader();

            sqlConn.Close();
            if (cmd != null)
            {
                return RedirectToAction("Index", "Login");
            }
            else { 
            ViewData["Message"] = "User " + user.name + " Registration fail";
            return View();
        }
        }
    }
}