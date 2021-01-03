using ihebsalimprojet.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;


namespace ihebsalimprojet.Controllers
{
    
    [Authorize]
    public class DevelopperController : Controller
    {
        // GET: Developper
       
        
        public ActionResult Index()
        {
            GetProject();
            return View("Index"/*,model*/);
        }
       
        public JsonResult get(int tacheId)
        {   
            var id = System.Web.HttpContext.Current.User.Identity.Name;
            DataTable dt = new DataTable();
            string conn = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into temp(datepointage,username,TacheId) values (@date,@name,@tacheid)",con);
            cmd.Parameters.AddWithValue("date",DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            cmd.Parameters.AddWithValue("name", id.ToString());
            cmd.Parameters.AddWithValue("TacheId", tacheId);
            
            cmd.ExecuteNonQuery();
            return Json("success");
        }
        public JsonResult set()
        {

            var id = System.Web.HttpContext.Current.User.Identity.Name;
            DataTable dt = new DataTable();
            string conn = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("update temp set datepointageoff=@datepointageoff where id=(select max(id) from temp) ", con);
            cmd.Parameters.AddWithValue("datepointageoff", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            
            
            cmd.ExecuteNonQuery();
            return Json("success");
        }
        //public JsonResult gettask(int taskId)
        //{
        //    // Get All Task by TaskID and UserName
        //    // Calculate Task Times (TimeSpent) if Any.
        //    // return => [{StartDate:"",EndDate:"",TimeSpent:""},{StartDate:"",EndDate:"",TimeSpent:""}...]
        //    //
        //    // 
        //}
        public ActionResult GetProjects()
        {
            Developper model = new Developper();
            DataTable dt = model.GetProject();
            return View("getProjects",dt);
        }
        public void GetProject()
        {
            DataTable dt = new DataTable();
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            using (SqlConnection con = new SqlConnection(strConString))
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Select ProjectId,ProjectName from Project", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow drow in dt.Rows)
            {
                list.Add(new SelectListItem { Text = drow["ProjectName"].ToString(), Value = drow["ProjectId"].ToString() });
            }
            ViewData["ProjectList"] = list;
            
        }
        public JsonResult GetTache(int ProjectId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            DataTable dt = new DataTable();
            string strConString = "data source=DESKTOP-6HSRCAQ\\MSSQLSERVER01; database=Ernst; integrated security=SSPI;";
            using (SqlConnection con = new SqlConnection(strConString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select T.TacheId,T.TacheName from Tache T inner join Project P on T.ProjectId = P.ProjectId where T.ProjectId = @ProjectId", con);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            
            foreach (DataRow drow in dt.Rows)
            {
                list.Add(new SelectListItem { Text = drow["TacheName"].ToString(), Value = drow["TacheId"].ToString() });
            }
            return Json(new SelectList(list, "Value", "Text"));
        }
        public ActionResult createTache(int ID)
        {
            Developper model = new Developper();
            DataTable dt = model.GetprojectByID(ID);
            return View("InsertTache", dt);
        }
        public ActionResult InsertTache(FormCollection frm, string action)
        {
            if(action == "Submit")
            {
                Developper model = new Developper();
                string tachename = frm["tachename"];
                int ID = Convert.ToInt32(frm["hdnID"]);
                int status = model.InsertTache(tachename,ID);
                return RedirectToAction("getProjects");
            }
            else
            {
                return RedirectToAction("getProjects");
            }
        }
    }
}