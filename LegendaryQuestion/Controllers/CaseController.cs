using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LegendaryQuestion.Models;
using Salesforce.Force;
using WebApplication9.Models;
using System.Threading.Tasks;

namespace WebApllication9.Controllers
{
    public class CaseController : Controller
    {

        public async Task<ActionResult> Index()
        {
            var accessToken = Session["AccessToken"].ToString();
            var apiVersion = Session["ApiVersion"].ToString();
            var instanceUrl = Session["InstanceUrl"].ToString();

            var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            var cases = await client.QueryAsync<CaseModel>("SELECT CaseNumber, Type, Subject, Description FROM Case");
            return View(cases.records);
        }
        //public ActionResult Create()
        //{
        //    return View();
        //}
        public async Task<ActionResult> Create([Bind(Include = "CaseNumber, Type, Subject, Description")] CaseModel caseModel)
        {
            var accessToken = Session["AccessToken"].ToString();
            var apiVersion = Session["ApiVersion"].ToString();
            var instanceUrl = Session["InstanceUrl"].ToString();

            var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            if (caseModel.Type == null && caseModel.Subject == null)
            {
                return View(caseModel);
            }
            await client.CreateAsync("Case", caseModel);
            return RedirectToAction("Index");
            
        }
    }
}
