using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LegendaryQuestion.Models;

namespace LegendaryQuestion.Controllers
{

    public class HomeController : Controller
    {
        private QuestionDBContext db = new QuestionDBContext();
        public ActionResult Index()
        {
            var queriesSearch = from q in db.Queries
                                select q;

            
            return View(queriesSearch);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Query query = db.Queries.Find(id);
            if (query == null)
            {
                return HttpNotFound();
            }
            return View(query);
        }

        // GET: Queries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Queries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Subject,Question")] Query query)
        {
            if (ModelState.IsValid)
            {
                db.Queries.Add(query);
                db.SaveChanges();
                return RedirectToAction("SalesForceSend", new { Name = query.Name, Subject = query.Subject, Question = query.Question });
            }

            return View(query);
        }

        // GET: Queries/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                var accessToken = Session["AccessToken"].ToString();
                var apiVersion = Session["ApiVersion"].ToString();
                var instanceUrl = Session["InstanceUrl"].ToString();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Query query = db.Queries.Find(id);
                if (query == null)
                {
                    return HttpNotFound();
                }
                return View(query);
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Queries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Query query = db.Queries.Find(id);
            db.Queries.Remove(query);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult SalesForceSend([Bind(Include = "ID,Name,Subject,Question")] Query query)
        {

            ViewBag.Question = query.Question;
            ViewBag.Name = query.Name;
            ViewBag.Subject = query.Subject;
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}