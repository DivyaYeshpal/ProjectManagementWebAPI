using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectManagementWebAPI.Models;
namespace ProjectManagementWebAPI.Controllers
{
    public class Project_DetailController : ApiController
    {
        public IHttpActionResult GetIndex()
        {
           IList<Project_DetailViewModel> ProjectDetail = null;
            using (var ctx = new ProjectManagementSystemEntities())
            {
                ProjectDetail = ctx.Project_Detail.
                                Select(s => new Project_DetailViewModel()
                                {
                                    Prj_ID = s.Prj_ID,
                                    Prj_Name = s.Prj_Name,
                                    Date_Of_Creation = s.Date_Of_Creation
                                }).ToList<Project_DetailViewModel>();
                if(ProjectDetail.Count==0)
                {
                    return NotFound();
                }
                return Ok(ProjectDetail);
            }
        }
        public IHttpActionResult GetDetailByID(int ID)
        {
            Project_DetailViewModel prjdet = null;
            using (var ctx=new ProjectManagementSystemEntities())
            {
                prjdet = ctx.Project_Detail.
                            Where(s => s.Prj_ID == ID)
                            .Select(s => new Project_DetailViewModel()
                            {
                                Prj_ID = s.Prj_ID,
                                Prj_Name = s.Prj_Name,
                                Date_Of_Creation = s.Date_Of_Creation
                            }).FirstOrDefault<Project_DetailViewModel>();
                if (prjdet==null)
                {
                    return NotFound();
                }
                return Ok(prjdet);
            }
        }
        public IHttpActionResult PostProjectDetail(Project_Detail prj)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            using(var ctx = new ProjectManagementSystemEntities())
            {
                ctx.Project_Detail.Add(new Project_Detail()
                {
                    Prj_ID = prj.Prj_ID,
                    Prj_Name=prj.Prj_Name,
                    Date_Of_Creation=prj.Date_Of_Creation
                });
                ctx.SaveChanges();
            }
            return Ok();
        }
        public IHttpActionResult PutProjectDetails(Project_Detail prj)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            using(var ctx=new ProjectManagementSystemEntities())
            {
                var existingProjects = ctx.Project_Detail.Where(s => s.Prj_ID == prj.Prj_ID)
                                        .FirstOrDefault<Project_Detail>();
                if (existingProjects != null)
                {
                    existingProjects.Prj_Name = prj.Prj_Name;
                    existingProjects.Date_Of_Creation = prj.Date_Of_Creation;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
        public IHttpActionResult DeleteProjectDetail(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            using (var ctx =new ProjectManagementSystemEntities())
            {
                var ExistingStudent = ctx.Project_Detail.Where(s => s.Prj_ID == id)
                                    .FirstOrDefault<Project_Detail>();
                ctx.Entry(ExistingStudent).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}

