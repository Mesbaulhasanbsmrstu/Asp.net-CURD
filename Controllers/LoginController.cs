using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstProject_CRUD_.Models;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace FirstProject_CRUD_.Controllers
{
    public class LoginController : Controller
    {
        public static string api_link = "https://localhost:44383/";
        // GET: Login
        public ActionResult Index()
        {

            return View();  
        }
        [HttpPost]
        public async Task<ActionResult> Index(UserLogin user)
        {
            /*string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            MySqlConnection sqlConn = new MySqlConnection(mainconn);
            string userName = user.name;
            string userPassword = user.password;
            string query = "select* from user where name = @name and password = @password";
            sqlConn.Open();
            MySqlCommand cmd = new MySqlCommand(query, sqlConn);
            cmd.Parameters.AddWithValue("@name", user.name);
            cmd.Parameters.AddWithValue("@password", user.password);
            
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
            }*/

            var tokenbased = String.Empty;
            List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();
            string requestParams = string.Empty;

            // Converting Request Params to Key Value Pair.  
            allIputParams.Add(new KeyValuePair<string, string>("email", user.email));
            allIputParams.Add(new KeyValuePair<string, string>("password", user.password));

            // URL Request Query parameters.  
            requestParams = new FormUrlEncodedContent(allIputParams).ReadAsStringAsync().Result;


            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(api_link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await client.GetAsync("api/Account/Login?"+requestParams);


                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultmessage = responseMessage.Content.ReadAsStringAsync().Result;
                    tokenbased = JsonConvert.DeserializeObject<string>(resultmessage);
                    Session["tokenNumber"] = tokenbased;
                    Session["userId"] = user.email;

                }
                else
                {
                   
                    Session["tokenNumber"] = null;
                    Session["userId"] = null;
                    //return View();
                  
                }

            }
            if (Session["userId"] == null)
            {
                ViewData["Message"] = "Wrong UserEmail or Password";
                //return View();
                return View("Index", user);
            }
            else
            {
                return RedirectToAction("Authentication", "Login");
            }
        }

            public async Task<ActionResult> Authentication()
            {
                var returnMessage = String.Empty;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.BaseAddress = new Uri(api_link);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["tokenNumber"].ToString() + ":" + Session["userId"].ToString());
                    var responseMessage = await client.GetAsync("Account/authentication");
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var resultmessage = responseMessage.Content.ReadAsStringAsync().Result;
                        returnMessage = JsonConvert.DeserializeObject<string>(resultmessage);
                        // Session["tokenNumber"] = tokenbased;

                    }



                }
                if(returnMessage=="valid")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Message"] = "Wrong userName or Password";
                //return View();
                return View("Index");
            }
           
        }

    }
}