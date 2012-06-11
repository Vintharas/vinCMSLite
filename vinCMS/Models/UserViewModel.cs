using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace vinCMS.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "You must type in an username")]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You must type in a password")]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}