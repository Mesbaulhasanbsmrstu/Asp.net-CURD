using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FirstProject_CRUD_.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Enter UserName!!")]
        [Display(Name = "Enter UserName:")]
       
        public string name { get; set; }
        [Required(ErrorMessage = "Enter Password!!")]
        [Display(Name = "Enter password:")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string loginErrorMessage { get; set; }
    }
}