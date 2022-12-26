using POS.UI.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace POS.UI.MVC.Controllers
{
    public class HomeController : BaseController
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
            _logger.TraceLog(String.Format("[{0:D2}:{1:D2}:{2:D3}]>>LoadTime. ", ts.Minutes, ts.Seconds, ts.Milliseconds));

            return View();
        }
    }
}
