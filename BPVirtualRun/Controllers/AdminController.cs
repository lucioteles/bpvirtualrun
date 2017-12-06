using BPVirtualRun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BPVirtualRun.Controllers
{
    public class AdminController : Controller
    {
        private BPContext db = new BPContext();


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Entrar(Usuarios model)
        {
            string senha = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Senha, "md5");
            try
            {
                var usuario = db.Usuarios.Where(x => x.Login == model.Login && x.Senha == senha).FirstOrDefault();
                if (usuario != null)
                    return RedirectToAction("../Campanhas");
                else
                    ViewBag.Erro = "Usuario ou senha incorretos.";
            }
            catch(Exception ex)
            {
                ViewBag.Erro = "Ocorreu um erro ao tentar consultar o banco de dados..." + ex.Message;
            }

            //return View(model);
            return View("Index", model);
        }
    }
}