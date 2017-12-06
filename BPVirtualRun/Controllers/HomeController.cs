using BPVirtualRun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BPVirtualRun.Controllers
{
    public class HomeController : Controller
    {
        private BPContext db = new BPContext();

        public ActionResult Index()
        {
            var campanhas = db.Campanhas.Where(x => x.FimInscricao >= DateTime.Today).OrderByDescending(x => x.FimInscricao).ToList();

            return View(campanhas);
        }

        public ActionResult Premios()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}