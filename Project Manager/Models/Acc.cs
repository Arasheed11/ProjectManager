using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Manager.Models
{
    public class Acc
    {
        [Display(Name = "Username")]
        public string Username
        {
            get;
            set;
        }
        [Display(Name = "Password")]

        public string Password
        {
            get;
            set;
        }
    }
}