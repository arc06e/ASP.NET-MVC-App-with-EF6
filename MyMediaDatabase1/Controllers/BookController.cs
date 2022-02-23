using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyMediaDatabase1.DAL;
using MyMediaDatabase1.Models;

namespace MyMediaDatabase1.Controllers
{
    public class BookController : Controller
    {
        private MediaContext db = new MediaContext();

        // GET: Book
        public async Task<ActionResult> Index()
        {
            var books = db.Books
                //eager loading - this must be so that you can access FK
                .Include(b => b.Author)
                .OrderBy(a => a.Author.LastName); 
            return View(await books.ToListAsync());
        }

        // GET: Book/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = await db.Books.FindAsync(id);

            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public async Task<ActionResult> Create(int? id)
        {          
            if (id != null)
            {
                Book book = new Book();
                Author author = await db.Authors.FindAsync(id);
                book.AuthorID = author.ID;
                return View(book);
            }

            return View();
        }




        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        //--NOTE ON HTTPPOST METHODS FOR CREATE/EDIT--
        // all post action methods, no matter the relationship (1-*,*-*), pass in instance of class

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Books.Add(book);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Author");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(book);
        }

        // GET: Book/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "LastName", book.AuthorID);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bookToUpdate = await db.Books.FindAsync(id);
            
            if (TryUpdateModel(bookToUpdate, "",
                new string[]
                { "Title", "YearReleased", "Genre", "Length", "AuthorID"}))
            {
                
                try
                {
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index", "Author");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(bookToUpdate);
        }

        // GET: Book/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Book book = await db.Books.FindAsync(id);
                db.Books.Remove(book);
                await db.SaveChangesAsync();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Index", "Author");
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
