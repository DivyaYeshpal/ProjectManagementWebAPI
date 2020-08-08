using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementWebAPI.Models
{
    public class Project_DetailViewModel
    {
        public decimal Prj_ID { get; set; }
        public string Prj_Name { get; set; }
        public Nullable<System.DateTime> Date_Of_Creation { get; set; }

    }
}