using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FirstProject_CRUD_.Models
{
    public class EmployeeAttribute
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Enter Name!!")]
        [Display(Name = "Enter Name:")]
        public string name { get; set; }
        [Required(ErrorMessage = "Enter Age!!")]
        [Display(Name = "Enter Age:")]
        public string age { get; set; }
        [Required(ErrorMessage = "Enter Adress!!")]
        [Display(Name = "Enter Address:")]
        public string address { get; set; }
        [Required(ErrorMessage = "Enter Phone!!")]
        [Display(Name = "Enter phone:")]
        public string phone { get; set; }
    
    }
}