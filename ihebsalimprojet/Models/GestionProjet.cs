using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ihebsalimprojet.Models
{
    
    public class GestionProjet
    {
        public DataTable GetProject()
        {
            DataTable dt = new DataTable();
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ProjectId,ProjectName from Project", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        public DataTable GetprojectByID(int ID)
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
        public int InsertProject(string projectname)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Insert into Project(ProjectName) values(@Project_Name)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Project_Name", projectname);

                return cmd.ExecuteNonQuery();
            }
        }
        public int UpdateProject(int id,string name)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Update Project SET ProjectName=@name where ProjectId=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteProject(int ID)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Delete from Project where ProjectId=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", ID);
                return cmd.ExecuteNonQuery();
            }
        }
        public int DeleteProjectAsso(int ID)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Delete from asso where ProjectId=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", ID);
                return cmd.ExecuteNonQuery();
            }
        }
        public int DeleteProjectUser(string ProjectName,string Name)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Delete from asso where ProjectId=(select ProjectId from Project where ProjectName=@ProjectName) and id=(select id from Users where Name=@Name)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProjectName", ProjectName);
                cmd.Parameters.AddWithValue("@Name", Name);
                return cmd.ExecuteNonQuery();
            }
        }
        public DataTable GetProjectUsers()
        {
            DataTable dtProjectUser = new DataTable();
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();

                string query = "SELECT P.ProjectName , U.Name FROM asso A INNER JOIN[Project] P ON A.ProjectId = P.ProjectId INNER JOIN[Users] U ON A.id = U.id";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dtProjectUser);
            }
            return dtProjectUser;
        }
        public List<UsersModel> Users { get; set; }
        public List<ProjectsModel> Projects { get; set; }
        public GestionProjet()
        {
            Users = new List<UsersModel>();
            Projects = new List<ProjectsModel>();
        }

        public class UsersModel
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public bool IsChecked { get; set; }
            public UsersModel()
            {

            }
        }
        public class ProjectsModel
        {
            public int Id { get; set; }
            public string ProjectName { get; set; }
            public bool IsChecked { get; set; }
            public ProjectsModel()
            {

            }
        }
    }
}