using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstProject_CRUD_.Models;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace FirstProject_CRUD_.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {

            return View();  
        }
        [HttpPost]
        public ActionResult Index(UserLogin user)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            MySqlConnection sqlConn = new MySqlConnection(mainconn);
            string userName = user.name;
            string userPassword = user.password;
            string query = "select* from user where name = @name and password = @password";
            sqlConn.Open();
            MySqlCommand cmd = new MySqlCommand(query, sqlConn);
            cmd.Parameters.AddWithValue("@name", user.name);
            cmd.Parameters.AddWithValue("@password", user.password);
            //MySqlDataReader sdr = cmd.ExecuteReader();
            /* while (sdr.Read())
             {
                 users.Add(new UserClass()
                 {
                     userId = Convert.ToInt32(sdr.GetValue(0)),
                     username = sdr.GetValue(1).ToString(),
                     userMail = sdr.GetValue(2).ToString()

                 });
             }*/
            /*while (sdr.Read())
            {
                int userId = Convert.ToInt32(sdr.GetValue(0));
            }*/
            int RecordCount = Convert.ToInt32(cmd.ExecuteScalar());
            sqlConn.Close();
            if (RecordCount >0)
            {
               Session["userId"] = "abc";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // user.loginErrorMessage = "Wrong userName or Password";
                ViewData["Message"]= "Wrong userName or Password";
                //return View();
                return View("Index",user);
            }
        }

    }
}