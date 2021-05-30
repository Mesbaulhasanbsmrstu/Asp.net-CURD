using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FirstProject_CRUD_.Models
{
    public class User
    {
        [Required(ErrorMessage ="Enter UserName!!")]
        [Display(Name ="Enter UserName:")]
        [StringLength(maximumLength:7,MinimumLength =3,ErrorMessage ="Name maximum length 7 and minimum length 3")]
        public string name { get; set; }
        [Required(ErrorMessage = "Enter Email!!")]
        [Display(Name = "Enter email:")]
        [EmailAddress]
        public string email { get; set; }
        [Required(ErrorMessage = "Enter Password!!")]
        [Display(Name = "Enter password:")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required(ErrorMessage = "Enter RePassword!!")]
        [Display(Name = "Enter re-password:")]
        [DataType(DataType.Password)]
        [Compare("password")]
        public string repassword { get; set; }
        [Required(ErrorMessage = "Select Gender!!")]
        [Display(Name = "Gender:")]
        public int gender { get; set; }
        [Required(ErrorMessage = "Upload Profile Image!!")]
        [Display(Name = "Profile image:")]
        public string image { get; set; }

    }
}