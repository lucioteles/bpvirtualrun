using BPVirtualRun.Models;
using BPVirtualRun.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BPVirtualRun.Controllers
{
    public class AreaAtletaController : Controller
    {
        private BPContext db = new BPContext();

        // GET: AreaAtleta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CorridasAtivas()
        {
            var corridas = db.Campanhas.Where(x => x.FimInscricao >= DateTime.Today).ToList();

            return View(corridas);
        }

        public ActionResult InscricaoModalidades(int idCampanha)
        {
            var atletaVM = (AtletaVM)Session["usuario"];
            atletaVM.Campanha = db.Campanhas.Where(x => x.Id == idCampanha).FirstOrDefault();
            atletaVM.Inscricao = new Inscricao();
            atletaVM.Inscricao.IdAtleta = atletaVM.Atleta.Id;
            atletaVM.Inscricao.IdCampanha = atletaVM.Campanha.Id;

            //return View(c);
            return View(atletaVM);
        }

        [HttpPost]
        public ActionResult Inscrever(AtletaVM model)
        {
            //var inscricao = model.Inscricoes;
            //inscricao.IdAtleta = model.Atleta.Id;
            //inscricao.IdCampanha = model.Campanha.Id;

            //db.Inscricoes.Add(inscricao);
            db.Inscricoes.Add(model.Inscricao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MinhasCorridas()
        {
            var dados = (AtletaVM)Session["usuario"];
            if (dados == null)
                return RedirectToAction("Index");
            dados.Inscricoes = new List<InscricoesVM>();
            var insc = db.Inscricoes.Where(x => x.IdAtleta == dados.Atleta.Id).ToList();
            foreach(var i in insc)
            {
                InscricoesVM inscricao = new InscricoesVM();
                inscricao.Id = i.Id;
                inscricao.IdAtleta = i.IdAtleta;
                inscricao.IdCampanha = i.IdCampanha;
                inscricao.NomeCampanha = db.Campanhas.Where(x => x.Id == i.IdCampanha).FirstOrDefault().Nome;
                inscricao.is5KM = i.is5KM;
                inscricao.is10KM = i.is10KM;
                inscricao.is15KM = i.is15KM;
                dados.Inscricoes.Add(inscricao);
            }
            //dados.Inscricoes = db.Inscricoes.Where(x => x.IdAtleta == dados.Atleta.Id).ToList();
            Session["usuario"] = dados;

            return View(dados);
        }

        public ActionResult RegistrarAtividade(int id)
        {
            AtividadeVM vm = new AtividadeVM();
            
            var inscricao = db.Inscricoes.Where(x => x.Id == id).FirstOrDefault();
            Atividade atividade = new Atividade();
            atividade.IdInscricao = id;

            vm.Atividade = atividade;
            vm.Inscricao = inscricao;

            return View(vm);
        }

        [HttpPost]
        public ActionResult RegistrarAtividade(AtividadeVM model)
        {
            try
            {
                db.Atividades.Add(model.Atividade);
                db.SaveChanges();
                ViewBag.Sucesso = "Atividade de " + model.Atividade.Distancia + "KM registrada com sucesso!";
                model.Inscricao = db.Inscricoes.Where(x => x.Id == model.Atividade.IdInscricao).FirstOrDefault();
            }
            catch(Exception ex)
            {
                ViewBag.Erro = "Não foi possível registrar atividade. " + ex.Message;
            }

            return View(model);
            //return RedirectToAction("Index");
        }

        public ActionResult ImprimirNumeroPeito(int idInscricao)
        {
            AtletaVM dados = new AtletaVM();
            var inscricao = db.Inscricoes.Where(x => x.Id == idInscricao).FirstOrDefault();
            var atleta = db.Atletas.Where(x => x.Id == inscricao.IdAtleta).FirstOrDefault();
            var campanha = db.Campanhas.Where(x => x.Id == inscricao.IdCampanha).FirstOrDefault();

            dados.Inscricao = inscricao;
            dados.Atleta = atleta;
            dados.Campanha = campanha;

            return View(dados);
        }
    }
}