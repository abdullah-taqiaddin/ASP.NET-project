using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Project.Data;
using Final_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Final_Project.Models.ViewModels;

namespace Final_Project.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BooksController(DataContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Books
        public async Task<IActionResult> Index(string SortBy)
        {
            ICollection<Book> result = null;
            

            if (SortBy is null) SortBy = string.Empty;
            switch (SortBy.ToLower())
            {
                case "title_asc":
                    return View(await _context.Book.OrderBy(b => b.Name).ToListAsync());
                case "title_desc":
                    return View(await _context.Book.OrderByDescending(b => b.Name).ToListAsync());
                case "price_asc":
                    return View(await _context.Book.OrderBy(b => b.Price).ToListAsync());
                case "price_desc":
                    return View(await _context.Book.OrderByDescending(b => b.Price).ToListAsync());
                default:
                    return View(await _context.Book.ToListAsync());
            }

            return View(await _context.Book.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Name,Price,Catagory,Cover,ReleseDate,Publsiher")] Book book, IFormFile Cover)
        {
            if (ModelState.IsValid)
            {
                if (Cover is not null)
                {
                    var GI = Guid.NewGuid().ToString();
                    var FullImageName = $"{_webHostEnvironment.WebRootPath}\\images\\{GI}_{Path.GetFileName(Cover.FileName)}";
                    using (var stream = new FileStream(FullImageName, FileMode.Create))
                    {
                        Cover.CopyTo(stream);
                        string relativeImagePath = $"/images/{GI}_{Path.GetFileName(Cover.FileName)}";
                        book.Cover = relativeImagePath;
                    }
                }
                else
                {
                    string FullFileName = $"/images/default.png";
                    book.Cover = FullFileName;
                }
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

            // GET: Books/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Name,Price,Catagory,Cover,ReleseDate,Publsiher")] Book book , IFormFile Cover)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (Cover is not null)
                {
                    var GI = Guid.NewGuid().ToString();
                    var FullImageName = $"{_webHostEnvironment.WebRootPath}\\images\\{GI}_{Path.GetFileName(Cover.FileName)}";
                    using (var stream = new FileStream(FullImageName, FileMode.Create))
                    {
                        Cover.CopyTo(stream);
                        string relativeImagePath = $"/images/{GI}_{Path.GetFileName(Cover.FileName)}";
                        book.Cover = relativeImagePath;
                    }
                }
                else
                {
                    string FullFileName = $"/images/default.png";
                    book.Cover = FullFileName;
                }
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);


        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookId == id);
        }


        ////////////////////////

        public IActionResult FilterByCatagory(FilterByCatagory model)
        {
            ICollection<Book> result = null;
            result = _context.Book.Where(b => b.Catagory == model.Catagory).ToList();
            return View("Index", result);
        }

        ////////////////////////

        public IActionResult BookReport()
        {
            return View(new BookReport());
        }
        [HttpPost]
        public async Task<IActionResult> BookReport(BookReport model)
        {
            if(model.Name is null)
            {
                model.Books = await _context.Book.Include(b => b.Authorships).ThenInclude(a => a.Author).ToListAsync();

            }
            else
            {
                model.Books = await _context.Book
                    .Where(b => b.Name.Trim().ToLower().Contains(model.Name.Trim().ToLower()))
                    .Include(b => b.Authorships)
                    .ThenInclude(a => a.Author).ToListAsync();
            }
            return View(model);
        }
    }
}
