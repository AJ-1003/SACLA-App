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
        public async Task<ActionResult> Index()
        {
            //var papers = await _context.Papers.ToListAsync();

            ApplicationViewModel testModel = new ApplicationViewModel();
            testModel.Papers = GetPapers();
            testModel.Topics = GetTopics();

            return View(testModel);
        }

        [Authorize(Roles = "Author")]
        // GET: PaperController
        public async Task<ActionResult> Author()
        {
            var currentUserId = _userManager.GetUserId(User);
            var authorPapers = await _context.Papers.ToListAsync();
            
            return View(authorPapers.Where(p => p.AuthorId == currentUserId));
        }

        // GET: PaperController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaperController/Create
        public async Task<ActionResult> Create()
        {
            PaperModel paper = new PaperModel();
            PaperViewModel paperViewModel = new PaperViewModel();
            paperViewModel.Title = paper.Title;
            paperViewModel.Author = paper.Author;
            paperViewModel.Topics = GetTopics();
            return View(paperViewModel);
        }

        // POST: PaperController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaperViewModel paperViewModel)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = _userManager.GetUserId(User);

            try
            {
                PaperModel paper = new PaperModel()
                {
                    Title = paperViewModel.Title,
                    Abstract = paperViewModel.Abstract,
                    //Topics = (IEnumerable<TopicModel>)paperViewModel.Topics,
                    AuthorId = currentUserId,
                    DateSubmitted = DateTime.Today
                };
                _context.Papers.Add(paper);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaperController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaperController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaperController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaperController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
