using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ihebsalimprojet.Controllers
{
    public class AuthentificationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Verif(string Login,string password)
        {
            bool res = Models.Authentication.VerifPassword(Login, password);
            if(res)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Authentification");
        }
    }
}