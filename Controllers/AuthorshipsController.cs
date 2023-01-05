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
    public class AuthorshipsController : Controller
    {
        private readonly DataContext _context;

        public AuthorshipsController(DataContext context)
        {
            _context = context;
        }

        // GET: Authorships
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Authorship.Include(a => a.Author).Include(a => a.Book);
            return View(await dataContext.ToListAsync());
        }

        // GET: Authorships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorship = await _context.Authorship
                .Include(a => a.Author)
                .Include(a => a.Book)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (authorship == null)
            {
                return NotFound();
            }

            return View(authorship);
        }

        // GET: Authorships/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "Name");
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Name");
            return View();
        }

        // POST: Authorships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BookId,AuthorId,Role")] Authorship authorship)
        {


            if (ModelState.IsValid)
            {
                _context.Add(authorship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "Name", authorship.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Name", authorship.BookId);
            return View(authorship);
        }

        // GET: Authorships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorship = await _context.Authorship.FindAsync(id);
            if (authorship == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "Name", authorship.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Name", authorship.BookId);
            return View(authorship);
        }

        // POST: Authorships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BookId,AuthorId,Role")] Authorship authorship)
        {
            if (id != authorship.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorshipExists(authorship.ID))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "Name", authorship.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Name", authorship.BookId);
            return View(authorship);
        }

        // GET: Authorships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorship = await _context.Authorship
                .Include(a => a.Author)
                .Include(a => a.Book)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (authorship == null)
            {
                return NotFound();
            }

            return View(authorship);
        }

        // POST: Authorships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorship = await _context.Authorship.FindAsync(id);
            _context.Authorship.Remove(authorship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorshipExists(int id)
        {
            return _context.Authorship.Any(e => e.ID == id);
        }


        /////////////////////////
        ///
        public IActionResult FindAuthorByStuff()
        {
            return View(new FindAuthorByStuff());
        }
        [HttpPost]
        public async Task<IActionResult> FindAuthorByStuff(FindAuthorByStuff model)
        {

            IEnumerable<Authorship> result = null;
            if (string.IsNullOrEmpty(model.Name))
            {
                result = await _context.Authorship.Include(a => a.Author).Include(a => a.Book).ToListAsync();
                model.Authorships = result;
            }
            else
            {
                result = await _context.Authorship.Include(a => a.Author).Include(a => a.Book).ToListAsync();
                result = result.Where(a => a.Book.Name.ToLower().Contains(model.Name.ToLower().Trim()));
                result = result.Where(a => a.Role == model.Role);
                model.Authorships = result;
            }
            return View(model);
        }
    }
}
