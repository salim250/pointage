using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ihebsalimprojet.Models
{
    public class Developper
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
        public int getData(string state,int TacheId)
        {
          
                var id = HttpContext.Current.User.Identity.ToString();
                DataTable dt = new DataTable();
                string conn = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
                using(SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Data(id,DatePointage,state,TacheId) values (@id,@DatePointage,@state,@TacheId)", con);
                    cmd.Parameters.AddWithValue("id",id);
                    cmd.Parameters.AddWithValue("DatePointage", DateTime.Now);
                    cmd.Parameters.AddWithValue("state", state);
                    cmd.Parameters.AddWithValue("TacheId", TacheId);
                    return cmd.ExecuteNonQuery();

                }
                
            
            
        }
        //public DataTable GetTache(int ID)
        //{
        //    DataTable dt = new DataTable();

        //    string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

        //    using (SqlConnection con = new SqlConnection(strConString))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("Select TacheName from Tache where ProjectId=" + ID, con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //    }
        //    return dt;
        //}
        public DataTable inserttache()
        {
            
            DataTable dt = new DataTable();

            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Data(Datenow) values (@Datenow)", con);
                cmd.Parameters.AddWithValue("@Datenow", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        public int InsertTache(string Tachename,int ID)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Insert into Tache(ProjectId,TacheName) values(@ProjectId,@tachename)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@tachename", Tachename);
                cmd.Parameters.AddWithValue("@ProjectId", ID);
                return cmd.ExecuteNonQuery();
            }
        }
        public int getTache(int TacheId)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "select T.TacheId,T.TachName from Tache T inner join Project P on T.ProjectId = P.ProjectId where T.TacheId = @TacheId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TacheId", TacheId);
                return cmd.ExecuteNonQuery();
            }
        }
        

        //public List<ProjectsModel> Projects { get; set; }
        //public List<SelectListItem> listProject { get; set; }
        //public Developper()
        //{
        //    listProject = new List<SelectListItem>();
        //Projects = new List<ProjectsModel>();
        //}
        //public class ProjectsModel
        //{
        //    public int Id { get; set; }
        //    public string ProjectName { get; set; }
            
        //    public ProjectsModel()
        //    {

        //    }
        //}
    }
}