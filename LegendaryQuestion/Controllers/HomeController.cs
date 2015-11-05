﻿using System;
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
        public ActionResult Create([Bind(Include = "ID,AskedBy,AskedWhen,Question")] Query query)
        {
            if (ModelState.IsValid)
            {
                db.Queries.Add(query);
                db.SaveChanges();
                return RedirectToAction("SalesForceSend");
            }

            return View(query);
        }

        public ActionResult SendToSalesForce(Query query)
        {

            //return HttpUtility.HtmlEncode("Hello " + query.AskedBy + ", it is: " + query.AskedWhen + " " + query.Question + " is a good question. please wait.");
            return RedirectToAction("Index");
        }

        // GET: Queries/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Queries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AskedBy,AskedWhen,Question,Answer")] Query query)
        {
            if (ModelState.IsValid)
            {
                db.Entry(query).State = EntityState.Modified;
                db.SaveChanges();

            }
            return View(query);
        }

        // GET: Queries/Delete/5
        public ActionResult Delete(int? id)
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

        public ActionResult SalesForceSend()
        {
            return View();
        }
    }
}