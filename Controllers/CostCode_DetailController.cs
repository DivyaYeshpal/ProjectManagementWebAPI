using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using ProjectManagementWebAPI.Models;

namespace ProjectManagementWebAPI.Controllers
{
    public class CostCode_DetailController : ApiController
    {
        public IHttpActionResult GetIndex()
        {
            IList<CostCode_DetailViewModel> CostCodeDetail = null;
            using (var ctx = new ProjectManagementSystemEntities())
            {
                CostCodeDetail = ctx.CostCode_Detail.
                                Select(c => new CostCode_DetailViewModel()
                                {
                                    CC_ID = c.CC_ID,
                                    CC_Name = c.CC_Name,
                                    Prj_ID = c.Prj_ID,
                                    RequestedDate = c.RequestedDate,
                                    BudgetAmount = c.BudgetAmount
                                }).ToList<CostCode_DetailViewModel>();
                if (CostCodeDetail == null)
                {
                    return NotFound();
                }
                else
                    return Ok(CostCodeDetail);
            }
        }
        public IHttpActionResult GetProjectDetailsByID(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            using(var ctx=new ProjectManagementSystemEntities())
            {
                var existingProject = ctx.CostCode_Detail
                                    .Where(c => c.CC_ID == id)
                                    .Select(s => new CostCode_DetailViewModel()
                                    {
                                        CC_ID=s.CC_ID,
                                        CC_Name = s.CC_Name,
                                        BudgetAmount=s.BudgetAmount,
                                        Prj_ID=s.Prj_ID,
                                        RequestedDate=s.RequestedDate
                                    }).FirstOrDefault<CostCode_DetailViewModel>();
                if(existingProject==null)
                {
                    return NotFound();
                }
                return Ok(existingProject);
            }
        }
        public IHttpActionResult Put(CostCode_DetailViewModel cc)
        {
            using(var ctx=new ProjectManagementSystemEntities())
            {
                var existingPrj = ctx.CostCode_Detail.Where(c => c.CC_ID == cc.CC_ID)
                                .FirstOrDefault<CostCode_Detail>();
                if(existingPrj!=null)
                {
                    existingPrj.CC_ID = cc.CC_ID;
                    existingPrj.CC_Name = cc.CC_Name;
                    existingPrj.BudgetAmount = cc.BudgetAmount;
                    existingPrj.Prj_ID = cc.Prj_ID;
                    existingPrj.RequestedDate = cc.RequestedDate;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            if(id<=0)
            {
                return BadRequest("Not a valid CostCodeID");
            }
            using(var ctx=new ProjectManagementSystemEntities())
            {
                var deletedPrj = ctx.CostCode_Detail
                                .Where(c => c.CC_ID == id)
                                .FirstOrDefault();
                ctx.Entry(deletedPrj).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();
        }
        //public IHttpActionResult GetProjectDetails()
        //{
        //    IList<Project_DetailViewModel> prj = null;
        //    using (var ctx = new ProjectManagementSystemEntities())
        //    {
        //        prj = ctx.Project_Detail
        //            .Select(s => new Project_DetailViewModel()
        //            {
        //                Prj_ID = s.Prj_ID,
        //                Prj_Name = s.Prj_Name,
        //            }).ToList<Project_DetailViewModel>();
        //        //var drpList = new SelectList(prj, "Prj_ID", "Prj_Name");
        //        //ViewData["PrjDtl"] = drpList;
        //        if (prj != null)
        //        {
        //            return Ok(prj);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //}
            public IHttpActionResult Create(CostCode_DetailViewModel cc)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            //var drpList = new SelectList(cc.ProjectDetailList.tol, "Prj_ID", "Prj_Name");
            using (var ctx=new ProjectManagementSystemEntities())
            {
                ctx.CostCode_Detail.Add(new CostCode_Detail()
                {
                    CC_Name = cc.CC_Name,
                    Prj_ID = cc.Prj_ID,
                    RequestedDate = cc.RequestedDate,
                    BudgetAmount = cc.BudgetAmount
                });
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}
