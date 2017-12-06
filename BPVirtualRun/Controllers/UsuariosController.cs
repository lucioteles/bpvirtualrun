using BPVirtualRun.Models;
using BPVirtualRun.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BPVirtualRun.Controllers
{
    public class UsuariosController : Controller
    {
        private BPContext db = new BPContext();

        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario
        public ActionResult Novo()
        {
            //string senha = FormsAuthentication.HashPasswordForStoringInConfigFile("bpivia2017", "md5");
            return View();
        }

        [HttpPost]
        public ActionResult Novo(AtletaVM model)
        {
            var usuarioExistente = db.Usuarios.Where(x => x.Login == model.Usuario.Login).FirstOrDefault();
            if (usuarioExistente == null)
            {
                string senha = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Usuario.Senha, "md5");
                model.Usuario.Senha = senha;
                model.Usuario.DataCadastro = DateTime.Today;
                model.Atleta.DataInscricao = DateTime.Today;


                //db.Atletas.Add(model.Atleta);
                //db.Usuarios.Add(model.Usuario);
                //db.SaveChanges();
                try
                {
                    db.Usuarios.Add(model.Usuario);
                    db.SaveChanges();
                    model.Usuario = db.Usuarios.Where(x => x.Login == model.Usuario.Login && x.Senha == senha).FirstOrDefault();
                    model.Atleta.IdUsuario = model.Usuario.Id;

                    db.Atletas.Add(model.Atleta);
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    ViewBag.Erro = "Erro ao tentar salvar cadastro do atleta. " + ex.Message;
                }


                Session["usuario"] = model;
                return RedirectToAction("Index");
                //return View(model);
            }
            else
            {
                //ModelState.AddModelError("erro", "Usuário já existente. Escolha outro antes de salvar novamente.");
                ViewBag.Erro = "Usuário já existente. Escolha outro antes de salvar novamente.";
                return View(model);
            }
        }

        public ActionResult Entrar()
        {
            return View();
            //return RedirectToAction("Index");
        }

        [HttpPost]
        //public ActionResult Entrar(Usuarios model)
        public ActionResult Entrar(AtletaVM model)
        {
            string senha = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Usuario.Senha, "md5");
            var usuario = db.Usuarios.Where(x => x.Login == model.Usuario.Login && x.Senha == senha).FirstOrDefault();
            if (usuario == null)
            {
                //ViewBag.Erro = "Usuário/Senha inválidos. Tente novamente...";
                ViewData["Erro"] = "Usuário/Senha inválidos. Tente novamente...";

                return RedirectToAction("Index");
            }
            else
            {
                var atleta = db.Atletas.Where(x => x.IdUsuario == usuario.Id).FirstOrDefault();
                model.Usuario = usuario;
                model.Atleta = atleta;

                Session["usuario"] = model;

                //return View(model);
                return RedirectToAction("Index", "AreaAtleta");
            }
            
        }

        public ActionResult Desconectar()
        {
            Session["usuario"] = null;

            return RedirectToAction("Index");
        }

    }
}