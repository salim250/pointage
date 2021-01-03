using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ihebsalimprojet.Models
{
    public class ProjectUserIndexModel
    {
        public List<UsersModel> Users { get; set; }
        public List<ProjectsModel> Projects { get; set; }
        public ProjectUserIndexModel()
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