using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstProject_CRUD_.Models
{
    public class Emplyee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string age { get; set; }

        public string address { get; set; }
        public string phone { get; set; }
    }
}