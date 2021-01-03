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
    public class ProjectUserController : Controller
    {
        // GET: ProjectUser
        public ActionResult Index()
        {
            

            return View("Index");
        }

        public static DataTable GetAllUsers()
        {
            DataTable Users = new DataTable();
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            using(SqlConnection con = new SqlConnection(strConString))
            {

                con.Open();
                string query = "select * from Users";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(Users);
            }
            return Users;
           
        }
        public static DataTable GetAllProjects()
        {
            DataTable Project = new DataTable();
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "SELECT *  FROM Project ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(Project);
            }
            return Project;

        }

        public ActionResult create()
        {
            var model = new Models.ProjectUserIndexModel();
            DataTable Users = new DataTable();
            Users = GetAllUsers();
            foreach (DataRow user in Users.Rows)
            {
                model.Users.Add(new Models.ProjectUserIndexModel.UsersModel
                {
                    Id = Convert.ToInt32(user["id"].ToString()),
                    UserName = Convert.ToString(user["Name"])
                });
            }
            DataTable Projects = new DataTable();
            Projects = GetAllProjects();
            foreach (DataRow project in Projects.Rows)
            {
                model.Projects.Add(new Models.ProjectUserIndexModel.ProjectsModel
                {
                    Id = Convert.ToInt32(project["ProjectId"].ToString()),
                    ProjectName = Convert.ToString(project["ProjectName"])
                });
            }
            return View("create",model);
        }
        
        public ActionResult InsertIntoAsso(int id, int ProjectId)
        {
            DataTable asso = new DataTable();
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            using(SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "insert into asso(id,ProjectId) values (id=@id,ProjectId=@ProjectId)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //var selected = new Models.ProjectUserIndexModel
            return RedirectToAction("Index");
        }
        //public List<Models.ProjectUserIndexModel.UsersModel> selectedId(ProjectUserIndexModel model)
        //{
        //    List<Models.ProjectUserIndexModel.UsersModel> selected1 = new List<Models.ProjectUserIndexModel.UsersModel>();
        //    selected1 = model.Users.Where(x => x.IsChecked == true).ToList<Models.ProjectUserIndexModel.UsersModel>();
        //    return selected1;
        //}
        //public List<Models.ProjectUserIndexModel.ProjectsModel> selectedProjectId(ProjectUserIndexModel model)
        //{
        //    List<Models.ProjectUserIndexModel.ProjectsModel> selected2 = new List<Models.ProjectUserIndexModel.ProjectsModel>();
        //    selected2 = model.Projects.Where(x => x.IsChecked == true).ToList<Models.ProjectUserIndexModel.ProjectsModel>();
        //    return selected2;
        //}

        [HttpPost]
        public ActionResult create(string SelectedUsers,string SelectedProject, int ID, FormCollection frm)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            
            GestionProjet model = new GestionProjet();
            int id = Convert.ToInt32(frm["hdnID"]);
            DataTable dt = model.GetprojectByID(ID);
            string[] Users = SelectedUsers.Split(',');
           string[] Projects = SelectedProject.Split(',');
            foreach (string User in Users)
            {
                if (User != "")
                {
                    foreach (string Project in Projects)
                    {
                        if (Project != "")
                        {
                            using (SqlConnection con = new SqlConnection(strConString))
                            {
                                con.Open();
                                string query = "insert into asso(Id,ProjectId) values (@Id,@ProjectId)";
                                SqlCommand cmd = new SqlCommand(query, con);
                                cmd.Parameters.AddWithValue("Id", Int32.Parse(User));
                                cmd.Parameters.AddWithValue("ProjectId", Int32.Parse(Project));
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            
            return RedirectToAction("Index", "ProjectUser");
        }



    }
}