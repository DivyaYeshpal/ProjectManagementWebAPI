using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ProjectManagementWebAPI.Models;
namespace ProjectManagementWebAPI.Controllers
{
    public class CostCodeDetailController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<CostCode_DetailViewModel> costcodedetail = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44355/api/");
                var ResponseTask = client.GetAsync("CostCode_Detail");
                ResponseTask.Wait();
                var Result = ResponseTask.Result;
                if (Result.IsSuccessStatusCode)

                {
                    var readTask = Result.Content.ReadAsAsync<IList<CostCode_DetailViewModel>>();
                    readTask.Wait();
                    costcodedetail = readTask.Result;
                }
                else
                {
                    costcodedetail = Enumerable.Empty<CostCode_DetailViewModel>();
                    ModelState.AddModelError(string.Empty, "Server Error, Please contact administrator");
                }
            }
            return View(costcodedetail);
        }
       //public ActionResult ListDropDown()
       // {
            
       //     //return cc.CC_Name
       // }
        public ActionResult Create()
        {
            IList<CostCode_DetailViewModel> costcodedetail = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44355/api/");
                var ResponseTask = client.GetAsync("CostCode_Detail");
                ResponseTask.Wait();
                var Result = ResponseTask.Result;
                if (Result.IsSuccessStatusCode)
                {
                    var readTask = Result.Content.ReadAsAsync<IList<CostCode_DetailViewModel>>();
                    readTask.Wait();
                    costcodedetail = readTask.Result;
                    ViewBag.prj = new SelectList("costcodedetail", "Prj_ID", "Prj_Name");
                }
                else
                {
                    costcodedetail = null;
                    ModelState.AddModelError(string.Empty, "Server Error, Please contact administrator");
                }
            }
            return View(ViewBag.prj);
        }
        [HttpPost]
        public ActionResult Create(CostCode_DetailViewModel cc)
        {
            //var drpList = new SelectList(cc.PrjDetails, "Prj_ID", "Prj_Name");
            //ViewData["PrjDtl"] = drpList;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44355/api/");
                var PostTask = client.PostAsJsonAsync<CostCode_DetailViewModel>("CostCode_Detail", cc);
                PostTask.Wait();
                var result = PostTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Saved Successfully";
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server error");
            return View(cc);
        }
    }
}
