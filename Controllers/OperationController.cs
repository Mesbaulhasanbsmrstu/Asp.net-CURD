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
    }
}