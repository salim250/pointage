using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ihebsalimprojet.Models;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;

namespace ihebsalimprojet.Controllers
{
    public class AuthentificationController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        
        // GET: Authentification

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        void connectionString()
        {
            con.ConnectionString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
        }
        [HttpPost]  
        public ActionResult Index(Authentification auth )
        {

            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = " select * from Users where username='"+auth.Name+"'and Password='"+auth.Password+"'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int roleId = Convert.ToInt32(dr["RoleId"]);
                var username = dr["username"].ToString();
                con.Close();
                connectionString();
                com = new SqlCommand();
                con.Open();
                com.Connection = con;
                
                com.CommandText = " select * from Roles where RoleId=" + roleId;
                dr = com.ExecuteReader();
                //SqlDataReader dr2 = com.ExecuteReader();
                if (dr.Read())
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    
                    
                    //con.Close();
                    if (dr["Name"].ToString().Equals("Admin"))
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            ViewBag.Title = User.Identity;
                        }
                        con.Close();

                        return RedirectToAction("Index", "Gestion_Utilisateurs");
                    }
                    else
                    {
                        con.Close();
                        return RedirectToAction("Index", "Developper");
                    }
                }
            }
           
            con.Close();
            return RedirectToAction("Index", "Authentification");



        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Authentification");
        }
    }
}