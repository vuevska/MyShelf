using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyShelf.Models;
using MyShelf.ViewModels;

namespace MyShelf.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Author).Include(b => b.Genre);
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [Authorize(Roles = Models.RoleName.Administrator)]
        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name");
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Models.RoleName.Administrator)]
        public ActionResult Create([Bind(Include = "BookId,Title,AuthorId,GenreId,CoverUrl,NumPages,PublicationDate,Publisher,ISBN13,AverageRating,Description,Price")] Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(RetryLimitExceededException)
            {
                Console.WriteLine("Problem");
                ModelState.AddModelError("", "Unable to save changes. Tru again, and if the problem persists, see your administrator");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", book.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", book.GenreId);
            return View(book);
        }

        [Authorize(Roles = Models.RoleName.Administrator)]
        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", book.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", book.GenreId);
            return View(book);
        }

        [Authorize(Roles = Models.RoleName.Administrator)]
        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Title,AuthorId,GenreId,CoverUrl,NumPages,PublicationDate,Publisher,ISBN13,AverageRating,Description,Price")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", book.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", book.GenreId);
            return View(book);
        }

        [Authorize(Roles = Models.RoleName.Administrator)]
        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [Authorize(Roles = Models.RoleName.Administrator)]
        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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

        //GET: Books/Search/query
        public ActionResult Search(string query)
        {
            var books = from entry in db.Books select entry;
            if(!String.IsNullOrEmpty(query))
            {
                List<Book> searchResult = books.Where(book => book.Title.Contains(query)).ToList();
                if(searchResult.Count != 0)
                {
                    ViewBag.Query = query;
                    return View(searchResult);
                }
                else
                {
                    return View("BookNotFound");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateCart(int book_id, int quantity)
        {
            if (ModelState.IsValid)
            {
                BookItemViewModel bookItemViewModel = new BookItemViewModel();
                
                Book book = db.Books.Find(book_id);
                bookItemViewModel.BookId = book_id;
                bookItemViewModel.Book = book;
                bookItemViewModel.Quantity = quantity;
                string user_id = User.Identity.GetUserId();
                ShoppingCart cart = db.ShoppingCarts.Where(c => c.UserId == user_id).SingleOrDefault();
                BookItemViewModel duplicate_book_item = cart.BookItems.Where(b => b.BookId == bookItemViewModel.BookId).SingleOrDefault();
                if (duplicate_book_item == null)
                {
                    bookItemViewModel.ShoppingCartId = cart.ShoppingCartId;
                    bookItemViewModel.ShoppingCart = cart;
                    cart.BookItems.Add(bookItemViewModel);
                    db.BookItemViewModels.Add(bookItemViewModel);
                }
                else
                {
                    duplicate_book_item.Quantity += quantity;
                    db.Entry(duplicate_book_item).State = EntityState.Modified;
                }
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = bookItemViewModel.BookId });
            }
            else
                return View("Error");
        }

        public ActionResult UserCart()
        {
            string user_id = User.Identity.GetUserId();
            ShoppingCart cart = db.ShoppingCarts.Where(c => c.UserId == user_id).SingleOrDefault();
            return View(cart);
        }

        public ActionResult RemoveFromCart(int id)
        {
            string user_id = User.Identity.GetUserId();
            ShoppingCart cart = db.ShoppingCarts.Where(c => c.UserId == user_id).SingleOrDefault();
            BookItemViewModel bookItem = cart.BookItems.Where(b => b.ItemId == id).SingleOrDefault();
            if (bookItem.Quantity - 1 > 0)
            {
                bookItem.Quantity -= 1;
                db.Entry(bookItem).State = EntityState.Modified;
            }
            else
            {
                cart.BookItems.Remove(bookItem);
                db.BookItemViewModels.Remove(bookItem);
            }
            db.Entry(cart).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("UserCart", cart);
        }

        public ActionResult NumberOfItemsInCart()
        {
            string user_id = User.Identity.GetUserId();
            ShoppingCart shopping = db.ShoppingCarts.Where(user => user.UserId == user_id).SingleOrDefault();
            int items = shopping.BookItems.Count();
            foreach(var item in shopping.BookItems)
            {
                if (item.Quantity > 1)
                    items += (item.Quantity - 1);
            }
            ViewBag.items = items;
            return PartialView("_Cart");
        }
    }
}
