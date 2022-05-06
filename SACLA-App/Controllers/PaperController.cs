using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SACLA_App.Areas.Identity.Data;
using SACLA_App.ViewModels;
using SACLA_App.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SACLA_App.Controllers
{
    public class PaperController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public PaperController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<PaperModel> GetPapers()
        {
            List<PaperModel> papers = new List<PaperModel>();
            return papers;
        }

        public IEnumerable<TopicModel> GetTopics()
        {
            List<TopicModel> topics = new List<TopicModel>();
            return topics;
        }

        // GET: PaperController
        public async Task<IActionResult> Index()
        {
            //ApplicationViewModel appViewModel = new ApplicationViewModel();
            //appViewModel.Papers = GetPapers();
            //appViewModel.Topics = GetTopics();

            var applicationDbContext = _context.Papers.Include(p => p.Author).Include(p => p.Topic);

            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Author")]
        // GET: PaperController
        public async Task<IActionResult> Author()
        {
            var currentUserId = _userManager.GetUserId(User);
            //var authorPapers = await _context.Papers.ToListAsync();

            var applicationDbContext = _context.Papers.Include(p => p.Author).Include(p => p.Topic).Where(p => p.AuthorId == currentUserId);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PaperController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paperToView = await _context.Papers.Include(p => p.Author).Include(p => p.Topic).FirstOrDefaultAsync(p => p.Id == id);

            if (paperToView == null)
            {
                return NotFound();
            }

            return View(paperToView);
        }

        // GET: PaperController/Create
        public async Task<IActionResult> Create()
        {
            //PaperViewModel paperViewModel = new PaperViewModel();
            //paperViewModel.Topic = GetTopics();
            ViewData["TopicId"] = new SelectList(_context.Set<TopicModel>(), "Id", "Name");
            return View();
        }

        // POST: PaperController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaperViewModel paperViewModel)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = _userManager.GetUserId(User);

            try
            {
                PaperModel paper = new PaperModel()
                {
                    Title = paperViewModel.Paper.Title,
                    Abstract = paperViewModel.Paper.Abstract,
                    TopicId = paperViewModel.Topic.Id,
                    AuthorId = currentUserId,
                    DateSubmitted = DateTime.Now
                };
                _context.Papers.Add(paper);
                _context.SaveChanges();
                return RedirectToAction(nameof(Author));
                ViewData["TopicId"] = new SelectList(_context.Set<TopicModel>(), "Id", "Name", paperViewModel.Topic.Name);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PaperController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paperModel = await _context.Papers.FindAsync(id);

            PaperViewModel paperViewModel = new PaperViewModel()
            {
                Paper = paperModel,
                Topic = paperModel.Topic
            };

            if (paperModel == null)
            {
                return NotFound();
            }

            ViewData["TopicId"] = new SelectList(_context.Set<TopicModel>(), "Id", "Name", paperModel.TopicId);
            return View(paperViewModel);
        }

        // POST: PaperController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaperViewModel paperViewModel)
        {
            if (id != paperViewModel.Paper.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            var paperToUpdate = await _context.Papers.FirstOrDefaultAsync(p => p.Id == id);

            ClaimsPrincipal currentUser = this.User;
            var currentUserId = _userManager.GetUserId(User);

            try
            {
                paperToUpdate.Title = paperViewModel.Paper.Title;
                paperToUpdate.Abstract = paperViewModel.Paper.Abstract;
                paperToUpdate.TopicId = paperViewModel.Topic.Id;
                paperToUpdate.Author = paperToUpdate.Author;
                paperToUpdate.AuthorId = paperToUpdate.AuthorId;
                paperToUpdate.DateSubmitted = paperToUpdate.DateSubmitted;

                _context.Update(paperToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaperViewModelExists(paperViewModel.Paper.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Author));
            //}
            ViewData["TopicId"] = new SelectList(_context.Set<TopicModel>(), "Id", "Name", paperViewModel.Topic.Name);
            return View(paperViewModel);
        }

        // GET: PaperController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paperToView = await _context.Papers.Include(p => p.Author).Include(p => p.Topic).FirstOrDefaultAsync(p => p.Id == id);

            if (paperToView == null)
            {
                return NotFound();
            }

            return View(paperToView);
        }

        // POST: PaperController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paperModel = await _context.Papers.FindAsync(id);
            _context.Papers.Remove(paperModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Author));
        }

        private bool PaperViewModelExists(int id)
        {
            return _context.Papers.Any(e => e.Id == id);
        }
    }
}
