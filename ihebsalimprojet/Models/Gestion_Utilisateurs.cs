using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ihebsalimprojet.Models
{

    public class Gestion_Utilisateurs
    {
        //public DataTable afficherhoraire(int userId)
        //{
        //    DataTable dt = new DataTable();
        //    string strconstring = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
        //    using (SqlConnection con = new SqlConnection(strconstring))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("select username,datepointage,datepointageoff from temp where username=(select username from Users where id =@id) ", con);
        //        cmd.Parameters.AddWithValue("id", userId);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //    }
        //    return dt;
        //}
        public DataTable GetUsers()
        {
            DataTable dt = new DataTable();
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Users", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        /// <summary>  
        /// Get student detail by Student id  
        /// </summary>  
        /// <param name="intStudentID"></param>  
        /// <returns></returns>  
        public DataTable GetUsersByID(int ID)
        {
            DataTable dt = new DataTable();

            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Users where id=" + ID, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        public DataTable GetDateByID(int ID)
        {
            DataTable dt = new DataTable();

            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from temp where id=" + ID, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        public DataTable GetTempByName(string username)
        {
            DataTable dt = new DataTable();

            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from temp where username=" + username, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        /// <summary>  
        /// Update the student details  
        /// </summary>  
        /// <param name="intStudentID"></param>  
        /// <param name="strStudentName"></param>  
        /// <param name="strGender"></param>  
        /// <param name="intAge"></param>  
        /// <returns></returns>  
        public int UpdateUsers(int ID, string username, string Password, string Name, string Last_Name, int RoleID)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Update Users SET username=@username, Password=@password , Name=@name, Last_Name=@last_name, RoleId=@roleid where id=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", Password);
                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@last_name", Last_Name);
                cmd.Parameters.AddWithValue("@roleid", RoleID);
                cmd.Parameters.AddWithValue("@id", ID);
                return cmd.ExecuteNonQuery();
            }
        }
        public int UpdateDate(int ID, string datepointage , string datepointageoff)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "UPDATE temp SET datepointage = cast(@datepointage as datetime),datepointageoff = cast(@datepointageoff as datetime) WHERE ID = @ID ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("datepointage", datepointage);
                cmd.Parameters.AddWithValue("datepointageoff", datepointageoff); 
                cmd.Parameters.AddWithValue("@ID", ID);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>  
        /// Insert Student record into DB  
        /// </summary>  
        /// <param name="strStudentName"></param>  
        /// <param name="strGender"></param>  
        /// <param name="intAge"></param>  
        /// <returns></returns>  
        public int InsertUsers(string username, string Password, string Name, string Last_Name, int RoleID)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Insert into Users (username,Password,Name,Last_Name,RoleId) values(@username,@password,@name ,@last_name,@roleid)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", Password);
                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@last_name", Last_Name);
                cmd.Parameters.AddWithValue("@roleid", RoleID);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>  
        /// Delete student based on ID  
        /// </summary>  
        /// <param name="intStudentID"></param>  
        /// <returns></returns>  
        public int DeleteUser(int ID)
        {
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Delete from Users where id=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", ID);
                return cmd.ExecuteNonQuery();
            }
        }
        
        public List<tempmodel> temp { get; set; }
       

        public Gestion_Utilisateurs()
        {
            temp = new List<tempmodel>();
            

        }
        public class tempmodel
        {
            public int ID { get; set; }
            public string username { get; set; }

            public Nullable<DateTime> datepointage { get; set; }
            public Nullable<DateTime> datepointageoff { get; set; }
            public TimeSpan datediff { get; set; }
            
            public tempmodel()
            {
                

            }
        }
        
         
            
        
    }
}