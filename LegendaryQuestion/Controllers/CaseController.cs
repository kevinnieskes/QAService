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

            try
            {
                var accessToken = Session["AccessToken"].ToString();
                var apiVersion = Session["ApiVersion"].ToString();
                var instanceUrl = Session["InstanceUrl"].ToString();
                var client = new ForceClient(instanceUrl, accessToken, apiVersion);
                //var cases = await client.QueryAsync<CaseModel>("SELECT CaseNumber, Status, Subject, Description FROM Case WHERE CaseNumber='" + caseID + "'");
                var cases = await client.QueryAsync<CaseModel>("SELECT Id, CaseNumber, SuppliedName, Subject, Description, Status FROM Case ORDER BY CaseNumber");
                return View(cases.records);
            }
            catch
            {
                return RedirectToAction("Error");
            }

        }
        public async Task<ActionResult> Create([Bind(Include = "CaseNumber, Type, Subject, Description")] CaseModel caseModel)
        {
            var accessToken = Session["AccessToken"].ToString();
            var apiVersion = Session["ApiVersion"].ToString();
            var instanceUrl = Session["InstanceUrl"].ToString();

            var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            if (caseModel.Status == null && caseModel.Subject == null)
            {
                return View(caseModel);
            }
            await client.CreateAsync("Case", caseModel);
            return RedirectToAction("Index");
            
        }

        public async Task<ActionResult> Edit(string Id)
        {


            var accessToken = Session["AccessToken"].ToString();
            var apiVersion = Session["ApiVersion"].ToString();
            var instanceUrl = Session["InstanceUrl"].ToString();
            var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            CaseModel1 caseModel = new CaseModel1() { Status = "Closed"};
            await client.UpdateAsync("Case", Id, caseModel);
            //var cases = await client.QueryAsync<CaseModel>("SELECT CaseNumber, Status, Subject, Description FROM Case WHERE CaseNumber='" + caseID + "'");
            //var cases = await client.QueryAsync<CaseModel>("SELECT id, CaseNumber, SuppliedName, Subject, Description, Status FROM Case WHERE id='" + Id + "'");
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Open(string Id)
        {


            var accessToken = Session["AccessToken"].ToString();
            var apiVersion = Session["ApiVersion"].ToString();
            var instanceUrl = Session["InstanceUrl"].ToString();
            var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            CaseModel1 caseModel = new CaseModel1() { Status = "New" };
            await client.UpdateAsync("Case", Id, caseModel);
            //var cases = await client.QueryAsync<CaseModel>("SELECT CaseNumber, Status, Subject, Description FROM Case WHERE CaseNumber='" + caseID + "'");
            //var cases = await client.QueryAsync<CaseModel>("SELECT id, CaseNumber, SuppliedName, Subject, Description, Status FROM Case WHERE id='" + Id + "'");
            return RedirectToAction("Index");
        }
        public ActionResult Error()
        {
            return View();
        }

    }
}
