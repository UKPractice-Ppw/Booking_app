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
    public class BookController : Controller
    {
        private Entities db = new Entities();

        // GET: Book
        public ActionResult Index(int? booktype_id)
        {
            // Check if Author is logged in
            if (Session["auth_id"] == null)
            {
                return RedirectToAction("Login", "Author");
            }

            int loggedInAuthorId = Convert.ToInt32(Session["auth_id"]);

            // Filter books by logged-in author
            var tbl_book = db.Tbl_book
                             .Include(t => t.Tbl_author)
                             .Include(t => t.Tbl_bookType)
                             .Include(t => t.Tbl_publisher)
                             .Where(b => b.author_id == loggedInAuthorId);

            // Filter by selected Book Type (if chosen from search)
            if (booktype_id.HasValue && booktype_id.Value > 0)
            {
                tbl_book = tbl_book.Where(b => b.booktype_id == booktype_id.Value);
            }

            // Populate Book Type dropdown list for the view
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", booktype_id);

            return View(tbl_book.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["auth_id"] == null) return RedirectToAction("Login", "Author");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_book tbl_book = db.Tbl_book.Find(id);
            if (tbl_book == null)
            {
                return HttpNotFound();
            }
            return View(tbl_book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            if (Session["auth_id"] == null) return RedirectToAction("Login", "Author");

            ViewBag.author_id = new SelectList(db.Tbl_author, "author_id", "author_name", Session["auth_id"]);
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name");
            ViewBag.pub_id = new SelectList(db.Tbl_publisher, "pub_id", "pub_name");
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "book_id,book_name,booktype_id,author_id,pub_id,book_pub_date,ISBN_No,Price")] Tbl_book tbl_book)
        {
            if (Session["auth_id"] == null) return RedirectToAction("Login", "Author");

            // Ensure created book is mapped to logged in author
            tbl_book.author_id = Convert.ToInt32(Session["auth_id"]);

            if (ModelState.IsValid)
            {
                db.Tbl_book.Add(tbl_book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.author_id = new SelectList(db.Tbl_author, "author_id", "author_name", tbl_book.author_id);
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", tbl_book.booktype_id);
            ViewBag.pub_id = new SelectList(db.Tbl_publisher, "pub_id", "pub_name", tbl_book.pub_id);
            return View(tbl_book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["auth_id"] == null) return RedirectToAction("Login", "Author");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_book tbl_book = db.Tbl_book.Find(id);
            if (tbl_book == null)
            {
                return HttpNotFound();
            }
            ViewBag.author_id = new SelectList(db.Tbl_author, "author_id", "author_name", tbl_book.author_id);
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", tbl_book.booktype_id);
            ViewBag.pub_id = new SelectList(db.Tbl_publisher, "pub_id", "pub_name", tbl_book.pub_id);
            return View(tbl_book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "book_id,book_name,booktype_id,author_id,pub_id,book_pub_date,ISBN_No,Price")] Tbl_book tbl_book)
        {
            if (Session["auth_id"] == null) return RedirectToAction("Login", "Author");

            if (ModelState.IsValid)
            {
                db.Entry(tbl_book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.author_id = new SelectList(db.Tbl_author, "author_id", "author_name", tbl_book.author_id);
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", tbl_book.booktype_id);
            ViewBag.pub_id = new SelectList(db.Tbl_publisher, "pub_id", "pub_name", tbl_book.pub_id);
            return View(tbl_book);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["auth_id"] == null) return RedirectToAction("Login", "Author");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_book tbl_book = db.Tbl_book.Find(id);
            if (tbl_book == null)
            {
                return HttpNotFound();
            }
            return View(tbl_book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["auth_id"] == null) return RedirectToAction("Login", "Author");

            Tbl_book tbl_book = db.Tbl_book.Find(id);
            db.Tbl_book.Remove(tbl_book);
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