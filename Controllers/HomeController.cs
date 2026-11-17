using Microsoft.AspNetCore.Mvc;
using RoomIQ.Models;
using System;
using System.Linq;

namespace RoomIQ.Controllers
{
    public class HomeController : Controller
    {
        // 1. index.html -> Dynamic Landing Page
        public IActionResult Index()
        {
            var featuredHostels = HostelData.Hostels.Take(3).ToList();
            return View(featuredHostels);
        }

        // 2. finder-home.html -> Dynamic Student Search Page
        public IActionResult FinderHome(string area, string gender, int? maxPrice)
        {
            var list = HostelData.Hostels.AsQueryable();

            if (!string.IsNullOrEmpty(area) && area != "All")
                list = list.Where(h => h.Area.Equals(area, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(gender) && gender != "All")
                list = list.Where(h => h.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase));

            if (maxPrice.HasValue && maxPrice.Value > 0)
                list = list.Where(h => h.Price <= maxPrice.Value);

            ViewBag.SelectedArea = area ?? "All";
            ViewBag.SelectedGender = gender ?? "All";
            ViewBag.MaxPrice = maxPrice;

            return View(list.ToList());
        }

        // 3. lister-dashboard.html -> Dynamic Owner Dashboard
        [HttpGet]
        public IActionResult ListerDashboard()
        {
            return View(HostelData.Hostels);
        }

        // Owner Dashboard se naya hostel dynamically add karne ke liye
        [HttpPost]
        public IActionResult AddHostel(Hostel hostel)
        {
            HostelData.Add(hostel);
            return RedirectToAction("ListerDashboard");
        }

        // Owner Dashboard se hostel delete karne ke liye
        [HttpPost]
        public IActionResult DeleteHostel(int id)
        {
            HostelData.Delete(id);
            return RedirectToAction("ListerDashboard");
        }
    }
}