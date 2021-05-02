using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Controllers;
using Project_Manager.Models;

namespace WebApplication3.Controllers
{
    public class AccController : Controller
    {
        public object Welcome { get; private set; }

      public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(login avm)
        {
            LoginEntities2 login = new LoginEntities2();
            var Signin = login.logins.Where(m => m.username == avm.username && m.password == avm.password).FirstOrDefault();
            if (Signin!=null)
            {
                Session["UserName"] = avm.username;
                return RedirectToAction("WorkSpaces", "WorkSpace");
            }    
            return View();
        }
        public ActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount(login accviewmodel)
        {
            LoginEntities2 Login = new LoginEntities2();
            Login.logins.Add(accviewmodel);
            try
            {
                Login.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return View("Login");
        }
    }
}