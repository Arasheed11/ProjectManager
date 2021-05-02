using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMachine.Models
{
    public class ApplicationUsers: IdentityUser
    {


        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string MobileNumber { get; set; }  
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string ConfirmPassword { get; set; } 
        public bool Status { get; set; }

    }
}
