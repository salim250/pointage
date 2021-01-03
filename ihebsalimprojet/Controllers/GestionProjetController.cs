using ihebsalimprojet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ihebsalimprojet.Controllers
{
    [Authorize]
    public class GestionProjetController : Controller
    {
        // GET: GestionProjet
        public ActionResult Index()
        {
            GestionProjet model = new GestionProjet();
            DataTable dt = model.GetProject();

            return View("Index", dt);
        }
        public ActionResult create()
        {
            return View("create");
        }

        /// <returns></returns>  
        public ActionResult InsertRecord(FormCollection frm, string action)
        {
            if (action == "Submit")
            {
                GestionProjet model = new GestionProjet();

                string name = frm["name"];

                int status = model.InsertProject(name);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int ID)
        {
            GestionProjet model = new GestionProjet();
            DataTable dt = model.GetprojectByID(ID);
            return View("Edit", dt);
        }
        public ActionResult Edit1(int ID)
        {
            GestionProjet model = new GestionProjet();
            DataTable dt = model.GetprojectByID(ID);
            return RedirectToAction("createUsers",dt);
        }

        public ActionResult UpdateRecord(FormCollection frm, string action)
        {
            if (action == "Submit")
            {
                GestionProjet model = new GestionProjet();
                string name = frm["txtName"];
                int id = Convert.ToInt32(frm["hdnID"]);
                int status = model.UpdateProject(id,name);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(int ID)
        {
            GestionProjet model = new GestionProjet();
            GestionProjet model1 = new GestionProjet();
            model.DeleteProject(ID);
            model.DeleteProjectAsso(ID);
            return RedirectToAction("Index");
        }
        public ActionResult Delete1(string ProjectName,string Name)
        {
            GestionProjet model = new GestionProjet();
            GestionProjet model1 = new GestionProjet();
            model.DeleteProjectUser(ProjectName, Name);
            return RedirectToAction("GetAllProjectUser");
        }
        public ActionResult GetAllProjectUser()
        {
            GestionProjet model = new GestionProjet();
            DataTable dt = model.GetProjectUsers();
            return View("GetAllProjectUser",dt);
        }
        public static DataTable GetAllUsers()
        {
            DataTable Users = new DataTable();
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            using (SqlConnection con = new SqlConnection(strConString))
            {

                con.Open();
                string query = "select * from Users";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(Users);
            }
            return Users;

        }
        public static DataTable GetAllProjects(int ID)
        {
            DataTable dt = new DataTable();

            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Project where ProjectId=" + ID, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;

        }
        public ActionResult createUsers(int ID, FormCollection frm)
        {
            GestionProjet model = new GestionProjet();
            int id = Convert.ToInt32(frm["hdnID"]);
            
            
            DataTable Users = new DataTable();
            Users = GetAllUsers();
            foreach (DataRow user in Users.Rows)
            {
                model.Users.Add(new Models.GestionProjet.UsersModel
                {
                    Id = Convert.ToInt32(user["id"].ToString()),
                    UserName = Convert.ToString(user["Name"])
                });
            }
            DataTable Projects = new DataTable();
            Projects = GetAllProjects(ID);
            foreach (DataRow project in Projects.Rows)
            {
                model.Projects.Add(new Models.GestionProjet.ProjectsModel
                {
                    Id = Convert.ToInt32(project["ProjectId"].ToString()),
                    ProjectName = Convert.ToString(project["ProjectName"])
                });
            }
            return View("createUsers", model);
        }
        [HttpPost]
        public ActionResult createUsers(string SelectedUsers, string Project)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            var model = new GestionProjet();
            
            
            string[] Users = SelectedUsers.Split(',');
            string[] Projects = Project.Split(',');
            model.DeleteProjectAsso(Convert.ToInt32( Projects[1]));
            foreach (string User in Users)
            {
                if (User != "")
                {
                    foreach (string project in Projects)
                    {
                        if (project != "")
                        {
                            using (SqlConnection con = new SqlConnection(strConString))
                            {
                                con.Open();
                                string query = "insert into asso(Id,ProjectId) values (@Id,@ProjectId)";
                                SqlCommand cmd = new SqlCommand(query, con);
                                cmd.Parameters.AddWithValue("Id", Int32.Parse(User));
                                cmd.Parameters.AddWithValue("ProjectId", Int32.Parse(project));
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Index", "GestionProjet");
        }
    }
}