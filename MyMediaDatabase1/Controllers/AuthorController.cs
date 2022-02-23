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
    public class AuthorController : Controller
    {
        private MediaContext db = new MediaContext();

        // GET: Author
        public async Task<ActionResult> Index()
        {
            var authors = await db.Authors.ToListAsync();

            // select the result first (author) and then sort the field you want (Books).
            authors = authors.Select(d => new Author
            {
                ID = d.ID,
                LastName = d.LastName,
                FirstName = d.FirstName,
                Books = d.Books.OrderBy(e => e.YearReleased).ToList()
            }).ToList();

            return View(authors);

        }

        // GET: Author/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Author author = await db.Authors.FindAsync(id);

            var distinct = new HashSet<string>(author.Books.Select(c => c.Genre));
            distinct.Distinct().ToList();

            ViewBag.Books = distinct;

            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }


        //--NOTE on Model Binding
        //model binding converts client request data
        //(form values, route data, query string parameters, HTTP headers)
        //into objects that the controller can handle.
        //As a result, your controller logic doesn't have to do the work
        //of figuring out the incoming request data;
        //it simply has the data as parameters to its action methods.

        //Model binder refers to the ASP.NET MVC functionality that makes it easier for you to work
        //with data submitted by a form; a model binder converts posted form values to CLR types
        //and passes them to the action method in parameters.
        //In this case (HttpPost Create), the model binder instantiates an Author entity for you
        //using property values from the Form collection.

        // the BindAttribute attribute limits the fields that the model binder uses
        // when it creates an Author instance--prevents over-posting

        // POST: Author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LastName,FirstName,DateBorn,DateDied,Nationality")] Author author)
        {
            try 
            { 
                if (ModelState.IsValid)
                {
                    db.Authors.Add(author);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(author);
        }

        // GET: Author/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = await db.Authors.FindAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        //prevents overposting/cross-site request forgery attacks. 
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var authorToUpdate = await db.Authors.FindAsync(id);

            //You can prevent overposting in edit scenarios by reading the entity
            //from the database first (above) and then calling TryUpdateModel,
            //passing in an explicit allowed properties list.
            if (TryUpdateModel(authorToUpdate, "",
                new string[] 
                { "FirstName", "LastName", "DateBorn", "DateDied", "Nationality" }))
            {
                try
                {
                   await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(authorToUpdate);
        }

        // GET: Author/Delete/5
                                            //an optional parameter that indicates whether the method
                                            //was called after a failure to save changes.
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
                                            //This parameter is false when the HttpGet Delete method is called
                                            //without a previous failure.When it is called by
                                            //the HttpPost Delete method in response to a database update
                                            //error, the parameter is true
                                            //and an error message is passed to the view.
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            Author author = await db.Authors.FindAsync(id);

            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Author author = await db.Authors.FindAsync(id);
                db.Authors.Remove(author);
                db.SaveChanges();
                //When SaveChanges is called, a SQL DELETE command is generated
                //cf note on improving performance in high-volume app in 'updating the delet page'
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }


        //To close database connections and free up the resources they hold as soon as possible,
        //dispose the context instance when you are done with it.
        //That is why the scaffolded code provides a Dispose method at the end of the Controller class

        //The base Controller class already implements the IDisposable interface,
        //so this code simply adds an override to the Dispose(bool) method to explicitly dispose
        //the context instance.
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
