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
    public class AuthorController : Controller
    {
        private Entities db = new Entities();
        private DbHandler hnd = new DbHandler();

        // GET: Author
        public ActionResult Index()
        {
            var tbl_author = db.Tbl_author.Include(t => t.Tbl_bookType);
            return View(tbl_author.ToList());
        }

        // GET: Author/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_author tbl_author = db.Tbl_author.Find(id);
            if (tbl_author == null)
            {
                return HttpNotFound();
            }
            return View(tbl_author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name");
            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "author_id,author_name,author_email,booktype_id,phone,password")] Tbl_author tbl_author)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_author.Add(tbl_author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", tbl_author.booktype_id);
            return View(tbl_author);
        }

        // GET: Author/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Tbl_author auth)
        {
            if(ModelState.IsValid)
            {
                if(hnd.Login_author(auth)!=0)
                {
                    return RedirectToAction("Index","Book");
                }
                else
                {
                    return View(auth);
                }
            }
            return View(auth);
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_author tbl_author = db.Tbl_author.Find(id);
            if (tbl_author == null)
            {
                return HttpNotFound();
            }
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", tbl_author.booktype_id);
            return View(tbl_author);
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "author_id,author_name,author_email,booktype_id,phone,password")] Tbl_author tbl_author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", tbl_author.booktype_id);
            return View(tbl_author);
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_author tbl_author = db.Tbl_author.Find(id);
            if (tbl_author == null)
            {
                return HttpNotFound();
            }
            return View(tbl_author);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_author tbl_author = db.Tbl_author.Find(id);
            db.Tbl_author.Remove(tbl_author);
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
