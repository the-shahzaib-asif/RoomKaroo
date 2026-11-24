using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomIQ.Models;
using RoomIQ.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace RoomIQ.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostelRepository _hostelRepo;
        private readonly IRoommateRepository _roommateRepo;
        private readonly ISavedHostelRepository _savedRepo;
        private readonly IInquiryRepository _inquiryRepo;

        public HomeController(
            IHostelRepository hostelRepo,
            IRoommateRepository roommateRepo,
            ISavedHostelRepository savedRepo,
            IInquiryRepository inquiryRepo)
        {
            _hostelRepo = hostelRepo;
            _roommateRepo = roommateRepo;
            _savedRepo = savedRepo;
            _inquiryRepo = inquiryRepo;
        }

        // 1. Index Page
        public async Task<IActionResult> Index()
        {
            var hostels = (await _hostelRepo.GetAllAsync()).Take(3).ToList();
            return View(hostels);
        }

        // 2. Finder Home Page
        public async Task<IActionResult> FinderHome()
        {
            var hostels = await _hostelRepo.GetAllAsync();
            var userEmail = User.Identity?.IsAuthenticated == true ? User.Identity.Name! : "guest@roomiq.pk";
            ViewBag.SavedIds = (await _savedRepo.GetSavedIdsByUserAsync(userEmail)).ToList();
            return View(hostels.ToList());
        }

        // AJAX ($.get): Live Filter Hostels without page reload
        [HttpGet]
        public async Task<IActionResult> FilterHostelsAjax(string? area, string? gender, int? maxPrice)
        {
            var hostels = await _hostelRepo.FilterAsync(area, gender, maxPrice);
            return Json(hostels);
        }

        // AJAX ($.post): Save / Unsave Hostel Bookmark
        [HttpPost]
        public async Task<IActionResult> ToggleSaveAjax(int hostelId)
        {
            var userEmail = User.Identity?.IsAuthenticated == true ? User.Identity.Name! : "guest@roomiq.pk";
            var isSaved = await _savedRepo.ToggleSaveAsync(userEmail, hostelId);
            return Json(new { saved = isSaved });
        }

        // AJAX ($.ajax): Send Quick Inquiry to Hostel Owner
        [HttpPost]
        public async Task<IActionResult> SendInquiryAjax([FromBody] Inquiry inquiry)
        {
            if (string.IsNullOrWhiteSpace(inquiry.StudentName) || string.IsNullOrWhiteSpace(inquiry.Phone))
                return BadRequest(new { message = "Name and phone are required." });

            await _inquiryRepo.AddAsync(inquiry);
            return Json(new { success = true, message = "Inquiry sent to hostel owner." });
        }

        // 3. Lister Dashboard (Protected by ASP.NET Identity)
        [Authorize]
        public async Task<IActionResult> ListerDashboard()
        {
            ViewBag.Inquiries = (await _inquiryRepo.GetAllAsync()).ToList();
            var hostels = await _hostelRepo.GetAllAsync();
            return View(hostels.ToList());
        }

        // AJAX ($.ajax): Add New Hostel dynamically
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddHostelAjax([FromBody] Hostel hostel)
        {
            var newId = await _hostelRepo.AddAsync(hostel);
            hostel.Id = newId;
            return Json(new { success = true, data = hostel });
        }

        // AJAX ($.ajax): Delete Hostel dynamically
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteHostelAjax(int id)
        {
            var deleted = await _hostelRepo.DeleteAsync(id);
            return Json(new { success = deleted });
        }

        // 4. Roommates Page
        public async Task<IActionResult> Roommates()
        {
            var posts = await _roommateRepo.GetAllAsync();
            return View(posts.ToList());
        }

        // AJAX ($.post): Add Roommate Ad without reload
        [HttpPost]
        public async Task<IActionResult> AddRoommateAjax(RoommatePost post)
        {
            var newId = await _roommateRepo.AddAsync(post);
            post.Id = newId;
            return Json(new { success = true, data = post });
        }
    }
}