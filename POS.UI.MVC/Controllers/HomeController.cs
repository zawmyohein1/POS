using POS.UI.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace POS.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        //private MyDbContext db = new MyDbContext();
        public IActionResult Dashboard()
        {
            DashboardViewModel dashboard = new DashboardViewModel();

            dashboard.Users_count = 10;
            dashboard.nurses_count = 20;
            dashboard.patients_count = 30;

            return View(dashboard);
        }
      
        public IActionResult Index(string menu, string actionURL)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return Redirect("/Users/Login");
            }
            
            DateTime t1 = DateTime.Now;
            ViewBag.menu = menu;
            ViewBag.actionURL = actionURL;
            TimeSpan ts = DateTime.Now.Subtract(t1);     

            return View();
        }
    }
}
