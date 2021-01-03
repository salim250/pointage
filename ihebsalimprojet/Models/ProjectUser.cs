using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ihebsalimprojet.Models
{
    public class ProjectUser
    {
        public System.Data.SqlClient.SqlCommand SelectCommand { get; set; }
        public static DataTable GetAllProjectUser()
        {
            DataTable dtProjectUser = new DataTable();
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using(SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();

                string query = "SELECT P.ProjectName , U.Name FROM asso A INNER JOIN[Project] P ON A.ProjectId = P.ProjectId INNER JOIN[Users] U ON A.id = U.id";
                
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dtProjectUser);
            }
            return dtProjectUser;
        }
        public static void InsertProjectUserRelation(int projectid, int id)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            using(SqlConnection con = new SqlConnection(strConString))
            {
                

                string query = "insert into asso values (ProjectId=@ProjectId,id=@id)";
                SqlCommand cmd = new SqlCommand(query,con);

                cmd.Parameters.AddWithValue("@ProjectId", projectid);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }
        public static List<int> GetAllUsersByProjectID(int ProjectId)
        {
            List<int> UsersIDs = new List<int>();
            //Get the Database Connection string from web.config file
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                //Create SqlParameter to hold Lectuere ID
                

                //Create SqlCommand to execute the Stored Procedure
                SqlCommand cmd = new SqlCommand("SELECT [id] FROM [asoo] WHERE[ProjectId] = @ProjectId", con);
                
                //Add the parameter to the SqlCommand object
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);

                //Open Sql Connection
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    UsersIDs.Add(Convert.ToInt32(dr["UsersIDs"]));
                }
            }
            return UsersIDs;
        }
        public static void DeleteBookAuthorRelationByBookID(int ProjectId)
        {
            //Get the Database Connection string from web.config file
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                //Create SqlParameter to hold Lectuere ID
               

                //Create SqlCommand to execute the Stored Procedure
                SqlCommand cmd = new SqlCommand("DELETE FROM [asso] WHERE[ProjectId] = @ProjectId", con);
                
                //Add the parameter to the SqlCommand object
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);

                //Open Sql Connection
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}