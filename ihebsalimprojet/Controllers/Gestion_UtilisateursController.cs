
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ihebsalimprojet.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml.Schema;
using Microsoft.Ajax.Utilities;

namespace ihebsalimprojet.Controllers
{
   [Authorize]
    public class Gestion_UtilisateursController : Controller
    {
        // GET: GestionProjet
        
        public ActionResult Index()
        {
            Gestion_Utilisateurs model = new Gestion_Utilisateurs();
            DataTable dt = model.GetUsers();
            
            return View("Index",dt);
        }
        public DataTable suivihoraire(int StudentID)
        {

            Gestion_Utilisateurs model = new Gestion_Utilisateurs();
            DataTable dt = model.GetUsersByID(StudentID);
            return dt;
            
        }
        public DataTable GetAlltemp(string username,string search)
        {
            string[] ss;
            bool test;
            string query;
            if(search == null)
            {
                search = DateTime.Now.ToString("dd/MM/yyyy");               
                ss = search.Split('/');
                test = false;
            }
            else
            {
                ss = search.Split('-');
                test = true;
            }
            
            
            DataTable temp = new DataTable();
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            using (SqlConnection con = new SqlConnection(strConString))
            {

                con.Open();
                if (test == false)
                {
                    query = "select id,username,datepointage,datepointageoff from temp where username=@username and (DATEPART(yy, datepointage) = " + Convert.ToInt32(ss[2]) + " AND DATEPART(mm, datepointage) =" + Convert.ToInt32(ss[1]) + " AND DATEPART(dd, datepointage) =" + Convert.ToInt32(ss[0]) + ") or DATEPART(dd, datepointage) is null or DATEPART(mm, datepointage) is null or DATEPART(yy, datepointage) is null";
                }
                else
                {
                    query = "select id,username,datepointage,datepointageoff from temp where username=@username and (DATEPART(yy, datepointage) = " + Convert.ToInt32(ss[0]) + " AND DATEPART(mm, datepointage) =" + Convert.ToInt32(ss[1]) + " AND DATEPART(dd, datepointage) =" + Convert.ToInt32(ss[2]) + ") or DATEPART(dd, datepointage) is null or DATEPART(mm, datepointage) is null or DATEPART(yy, datepointage) is null";

                }
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("username", username);
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(temp);
            }
            return temp;

        }
        public ActionResult afficherhoraire(string username,string search)
        {
            
            var model = new Models.Gestion_Utilisateurs();
            DataTable temp = new DataTable();
            
            temp = GetAlltemp(username,search);
            foreach (DataRow tp in temp.Rows)
            {

                if (string.IsNullOrEmpty(tp["datepointageoff"].ToString()))
                {
                    model.temp.Add(new Models.Gestion_Utilisateurs.tempmodel
                    {
                        ID = Convert.ToInt32(tp["id"]),
                        username = Convert.ToString(tp["username"]),
                        datepointage = Convert.ToDateTime(tp["datepointage"].ToString()),
                        datepointageoff = DateTime.MinValue,
                        datediff = TimeSpan.Zero



                    });
                }
                else
                {
                    model.temp.Add(new Models.Gestion_Utilisateurs.tempmodel
                    {
                        ID = Convert.ToInt32(tp["id"]),
                        username = Convert.ToString(tp["username"]),
                        datepointage = Convert.ToDateTime(tp["datepointage"].ToString()),
                        datepointageoff = Convert.ToDateTime(tp["datepointageoff"].ToString()),
                        datediff = Convert.ToDateTime(tp["datepointageoff"].ToString()) - Convert.ToDateTime(tp["datepointage"].ToString())



                    });
                }
                
                
            }
            
            ModelState.Clear();
            
            return View(model);
            
            
        }
        
        public ActionResult create()
        {
            return View("create");
        }

        /// <returns></returns>  
        /// 
        
        public ActionResult InsertRecord(FormCollection frm, string action)
        {
            if (action == "Submit")
            {
                Gestion_Utilisateurs model = new Gestion_Utilisateurs();
                string login = frm["login"];
                string password = frm["password"];
                string name = frm["name"];
                string lastname = frm["lastname"];
                int roleid = Convert.ToInt32(frm["roleid"]);
                int status = model.InsertUsers(login,password,name, lastname, roleid);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
       
        public ActionResult Edit(int StudentID)
        {
            Gestion_Utilisateurs model = new Gestion_Utilisateurs();
            DataTable dt = model.GetUsersByID(StudentID);
            return View("Edit", dt);
        }
        public ActionResult Editdate(int ID)
        {
            var model = new Models.Gestion_Utilisateurs();
            DataTable temp = new DataTable();

            temp = model.GetDateByID(ID);
            foreach (DataRow tp in temp.Rows)
            {
                if (string.IsNullOrEmpty(tp["datepointageoff"].ToString()))
                {
                    model.temp.Add(new Models.Gestion_Utilisateurs.tempmodel
                    {
                        ID = Convert.ToInt32(tp["id"]),
                        username = Convert.ToString(tp["username"]),
                        datepointage = Convert.ToDateTime(tp["datepointage"].ToString()),
                        datepointageoff = null,
                        datediff = TimeSpan.Zero



                    });
                }
                else
                {
                    model.temp.Add(new Models.Gestion_Utilisateurs.tempmodel
                    {
                        ID = Convert.ToInt32(tp["id"]),
                        username = Convert.ToString(tp["username"]),
                        datepointage = Convert.ToDateTime(tp["datepointage"].ToString()),
                        datepointageoff = Convert.ToDateTime(tp["datepointageoff"].ToString()),
                        datediff = Convert.ToDateTime(tp["datepointageoff"].ToString()) - Convert.ToDateTime(tp["datepointage"].ToString())



                    });
                }

            }

            return View("Editdate",model);
        }
        public ActionResult Updatedate(FormCollection frm, string action,string username1,string search2)
        {
            if (action == "Submit")
            {
                Gestion_Utilisateurs model = new Gestion_Utilisateurs();
                int id = Convert.ToInt32(frm["hdnID"]);
                string datepointage = frm["datepointage"];
                string datepointageoff = frm["datepointageoff"];
                
                int status = model.UpdateDate(id, datepointage, datepointageoff);

                return RedirectToAction("afficherhoraire", new { username = username1, search = search2 });
            }
            else
            {
                return RedirectToAction("afficherhoraire",new { username = username1, search = search2 });
            }
        }

        public ActionResult UpdateRecord(FormCollection frm, string action)
        {
            if (action == "Submit")
            {
                Gestion_Utilisateurs model = new Gestion_Utilisateurs();
                string name = frm["txtName"];
                int id = Convert.ToInt32(frm["hdnID"]);
                string login = frm["login"];
                string password = frm["password"];
                string lastname = frm["lastname"];
                int roleid = Convert.ToInt32(frm["roleid"]);
                int status = model.UpdateUsers(id, login,password,name,lastname, roleid);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        
        public ActionResult Delete(int StudentID)
        {
            Gestion_Utilisateurs model = new Gestion_Utilisateurs();
            model.DeleteUser(StudentID);
            return RedirectToAction("Index");
        }
    }
}