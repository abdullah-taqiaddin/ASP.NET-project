using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Project.Data;
using Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Final_Project.Models.ViewModels;

namespace Final_Project.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        private readonly DataContext _context;

        public AuthorsController(DataContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index(string sortOrder = "Asc")
        {
            try
            {
                ViewData["sortorder"] = sortOrder;

                var result2 = _context.Book.Where(x => x.Name.Contains("Spiderman")).ToList();
                var result = _context.Author.Include(a => a.Authorships).ThenInclude(a => a.Book).Where(x => x.Authorships.Any(y => result2.Any(z => z.BookId == y.BookId))).ToList();



                //result = await _context.Author.Include(a => a.Authorships).ThenInclude(au => au.Book);      

                return View(result);
            }
            catch(Exception ex)
            {
                return View();
            }
            //switch (sortOrder)
            //{
            //    case "Asc":
            //        return View(await _context.Author.OrderBy(a => a.Name).ToListAsync());
            //    case "dsc":
            //        return View(await _context.Book.OrderByDescending(a => a.Name).ToListAsync());
            //    default:
            //        return View(await _context.Author.ToListAsync());
            //}
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,Name,DOB,Phone,Email")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,Name,DOB,Phone,Email")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.AuthorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Author.FindAsync(id);
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.AuthorId == id);
        }



        public IActionResult FindAuthor()
        {
            return View(new FindAuthor());
        }
        [HttpPost]
        public IActionResult FindAuthor(FindAuthor model)
        {
            ICollection<Author> result = null;

            if(!ModelState.IsValid)
            {
                return View(model);
            }
            
            result = _context.Author.Where(a => a.Name == model.Name).ToList();
            model.Authors = result;
            return View(model);
        }
    }
}
