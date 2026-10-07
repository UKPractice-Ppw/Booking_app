using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Booking_Application.Models;

namespace Booking_Application.Controllers
{
    public class BookTypeController : Controller
    {
        private Entities db = new Entities();

        // GET: BookType
        public ActionResult Index()
        {
            return View(db.Tbl_bookType.ToList());
        }

        // GET: BookType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_bookType tbl_bookType = db.Tbl_bookType.Find(id);
            if (tbl_bookType == null)
            {
                return HttpNotFound();
            }
            return View(tbl_bookType);
        }

        // GET: BookType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "booktype_id,booktype_name")] Tbl_bookType tbl_bookType)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_bookType.Add(tbl_bookType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_bookType);
        }

        // GET: BookType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_bookType tbl_bookType = db.Tbl_bookType.Find(id);
            if (tbl_bookType == null)
            {
                return HttpNotFound();
            }
            return View(tbl_bookType);
        }

        // POST: BookType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "booktype_id,booktype_name")] Tbl_bookType tbl_bookType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_bookType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_bookType);
        }

        // GET: BookType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_bookType tbl_bookType = db.Tbl_bookType.Find(id);
            if (tbl_bookType == null)
            {
                return HttpNotFound();
            }
            return View(tbl_bookType);
        }

        // POST: BookType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_bookType tbl_bookType = db.Tbl_bookType.Find(id);
            db.Tbl_bookType.Remove(tbl_bookType);
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
    }
}
