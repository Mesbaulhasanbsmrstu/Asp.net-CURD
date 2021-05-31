using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstProject_CRUD_.Models;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.Http;

namespace FirstProject_CRUD_.Controllers
{
    public class OperationController : Controller
    {
        public static string api_link = "https://localhost:44383/";
       
        // GET: Login
        public ActionResult Index()
        {
            if (Session["userId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(EmployeeAttribute employee)
        {
            if (Session["userId"] != null)
            {
                int user_id = Convert.ToInt32(Session["pppp"]);
                user_id = user_id;
                var respone_Message = String.Empty;
                List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();
                string requestParams = string.Empty;

                // Converting Request Params to Key Value Pair.  
                allIputParams.Add(new KeyValuePair<string, string>("user_id", user_id.ToString()));
                allIputParams.Add(new KeyValuePair<string, string>("name", employee.name));
                allIputParams.Add(new KeyValuePair<string, string>("age", employee.age));
                allIputParams.Add(new KeyValuePair<string, string>("address", employee.address));
                allIputParams.Add(new KeyValuePair<string, string>("phone", employee.phone));


                // URL Request Query parameters.  
                requestParams = new FormUrlEncodedContent(allIputParams).ReadAsStringAsync().Result;


                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Clear();
                    client.BaseAddress = new Uri(api_link);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var responseMessage = await client.GetAsync("api/Account/addEmployee?" + requestParams);


                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var resultmessage = responseMessage.Content.ReadAsStringAsync().Result;
                        respone_Message = JsonConvert.DeserializeObject<string>(resultmessage);


                    }
                    else
                    {
                        respone_Message = null;
                        //return View();

                    }

                }
                if (respone_Message != null)
                {
                    ModelState.Clear();
                    ViewData["Message"] = "Add Successfully";
                    
                    return View();
                }
                else
                {
                    ViewData["Message"] = "Fail to Add";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpGet]
        public  ActionResult getEmployee()
         {
            if (Session["userId"] != null)
            {
                int user_id = Convert.ToInt32(Session["pppp"]);
                user_id = user_id;
                List<Emplyee> emplyees = new List<Emplyee>();
                string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                MySqlConnection sqlConn = new MySqlConnection(mainconn);
                string query = "select* from emplyee where user_id=@id";
                //where user_id=@id
                sqlConn.Open();
                MySqlCommand cmd = new MySqlCommand(query, sqlConn);
                cmd.Parameters.AddWithValue("@id", user_id);
                MySqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {


                    emplyees.Add(new Emplyee()
                    {
                        id = Convert.ToInt32(sdr.GetValue(0)),
                        name = sdr.GetValue(1).ToString(),
                        age = sdr.GetValue(2).ToString(),
                        address = sdr.GetValue(3).ToString(),
                        phone = sdr.GetValue(4).ToString(),

                    });


                }
                //JsonResult<List<Emplyee>> employee = emplyees.
                sqlConn.Close();

                return View(emplyees);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            }
 [HttpGet]
        public async Task<ActionResult> delete(int id)
        {
if (Session["userId"] != null)
            {
                int user_id = Convert.ToInt32(Session["pppp"]);
                user_id = user_id;
                var respone_Message = String.Empty;
                List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();
                string requestParams = string.Empty;

                // Converting Request Params to Key Value Pair.  
                allIputParams.Add(new KeyValuePair<string, string>("id", id.ToString()));
              


                // URL Request Query parameters.  
                requestParams = new FormUrlEncodedContent(allIputParams).ReadAsStringAsync().Result;


                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Clear();
                    client.BaseAddress = new Uri(api_link);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var responseMessage = await client.GetAsync("api/UserEmployee/delete?" + requestParams);


                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var resultmessage = responseMessage.Content.ReadAsStringAsync().Result;
                        respone_Message = JsonConvert.DeserializeObject<string>(resultmessage);


                    }
                    else
                    {
                        respone_Message = null;
                        //return View();

                    }

                }
                if (respone_Message =="yess")
                {
                  
                    return RedirectToAction("getEmployee", "Operation");

                
                }
                else
                {
                    return RedirectToAction("Index", "Operation");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult edit(EmployeeAttribute e)
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> edit_data(EmployeeAttribute e)
        {
            if (Session["userId"] != null)
            {
               
                var respone_Message = String.Empty;
                List<KeyValuePair<string,string>> allIputParams = new List<KeyValuePair<string, string>>();
                string requestParams = string.Empty;

                // Converting Request Params to Key Value Pair.  
                allIputParams.Add(new KeyValuePair<string, string>("id", e.id.ToString()));
                allIputParams.Add(new KeyValuePair<string, string>("name", e.name));
                allIputParams.Add(new KeyValuePair<string, string>("age", e.age));
                allIputParams.Add(new KeyValuePair<string, string>("address", e.address));
                allIputParams.Add(new KeyValuePair<string, string>("phone", e.phone));


                // URL Request Query parameters.  
                requestParams = new FormUrlEncodedContent(allIputParams).ReadAsStringAsync().Result;


                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Clear();
                    client.BaseAddress = new Uri(api_link);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var responseMessage = await client.GetAsync("api/UserEmployee/edit?" + requestParams);


                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var resultmessage = responseMessage.Content.ReadAsStringAsync().Result;
                        respone_Message = JsonConvert.DeserializeObject<string>(resultmessage);

                        return RedirectToAction("getEmployee", "Operation");

                    }
                    else
                    {
                        respone_Message = null;
                        try
                        {
                            var resultmessage = responseMessage.Content.ReadAsStringAsync().Result;
                            respone_Message = JsonConvert.DeserializeObject<string>(resultmessage);
                            ModelState.Clear();
                            ViewData["Message"] = respone_Message;
                            return RedirectToAction("edit", "Operation");
                        }
                        catch(Exception ex)
                        {
                            ViewData["Message"] =ex.Message;
                            return RedirectToAction("edit", "Operation");
                        }
                        //return View();

                    }

                }
              
                //return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }



        }

  }
    
