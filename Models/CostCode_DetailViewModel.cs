using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementWebAPI.Models
{
    public class CostCode_DetailViewModel
    {
        public IEnumerable<string> ProjectDetailList()
        {
            ProjectManagementSystemEntities prj = new ProjectManagementSystemEntities();
            return prj.Project_Detail.Select(c=>c.Prj_Name);
        }
        public IEnumerable<string> PrjDetail
        {
            get
            {
                ProjectManagementSystemEntities prj = new ProjectManagementSystemEntities();
                return prj.Project_Detail.Select(c=>c.Prj_Name);
            }
        }
        public Gender StudentGender { get; set; }
        public enum Gender
        {
            Male,
            Female
        }
        public decimal CC_ID { get; set; }
        public string CC_Name { get; set; }
        public Nullable<decimal> Prj_ID { get; set; }
        public Nullable<System.DateTime> RequestedDate { get; set; }
        public Nullable<decimal> BudgetAmount { get; set; }
        public Nullable<byte> TypeofPrj { get; set; }
        public Nullable<int> PrjPlatform { get; set; }
        public string PrjPlatformids { get; set; }

    }
}