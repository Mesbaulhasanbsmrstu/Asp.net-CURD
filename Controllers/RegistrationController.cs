using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using FirstProject_CRUD_.Models;
using System.Configuration;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FirstProject_CRUD_.Controllers
{
    public class RegistrationController : Controller
    {
        public static string api = "https://localhost:44383/";
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(User user, HttpPostedFileBase file)
        {
            //check email

            var checkmail = String.Empty;
            int id;
            List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();
            string requestParams = string.Empty;

            // Converting Request Params to Key Value Pair.  
            allIputParams.Add(new KeyValuePair<string, string>("email", user.email));


            // URL Request Query parameters.  
            requestParams = new FormUrlEncodedContent(allIputParams).ReadAsStringAsync().Result;

            var responseMessage = new HttpResponseMessage();
            //string response;
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(api);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                 responseMessage = await client.GetAsync("api/Account/checkMail?" + requestParams);
            }


                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultmessage = responseMessage.Content.ReadAsStringAsync().Result;
                    checkmail = JsonConvert.DeserializeObject<string>(resultmessage);
               
                }
                else
                {
                checkmail = null;
                   
                    //return View();

                }
                if(checkmail=="invalid")
            {
                ViewData["Message"] = "Already Registered";
                return View();
            }
            else if(checkmail == "valid")
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
                cmd.ExecuteNonQuery();
               
             
                //MySqlDataReader sdr = cmd.ExecuteReader();

                sqlConn.Close();
                if (cmd != null)
                {
                    id = Convert.ToInt32(cmd.LastInsertedId);
                    Session["user_id"] = id*13;
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewData["Message"] = "User " + user.name + " Registration fail";
                    return View();
                }
            }
            else
            {
                ViewData["Message"] = "User  Registration fail";
                return View();
            }
            


           
               
            }
        }
    }
