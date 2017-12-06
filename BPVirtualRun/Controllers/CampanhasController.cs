using BPVirtualRun.Models;
using BPVirtualRun.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BPVirtualRun.Controllers
{
    public class CampanhasController : Controller
    {
        private BPContext db = new BPContext();

        // GET: Campanhas
        public ActionResult Index()
        {
            var campanhas = new List<Campanha>();
            campanhas = db.Campanhas.OrderBy(x => x.Id).ToList();

            return View(campanhas);
        }

        public ActionResult Nova()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Salvar(Campanha model)
        {
            if (model.Id != 0)
                db.Entry<Campanha>(model).State = System.Data.Entity.EntityState.Modified;
            else
            {
                model.Criacao = DateTime.Today;
                db.Campanhas.Add(model);
            }

            db.SaveChanges();
            return RedirectToAction("../Campanhas");
        }

        public JsonResult Excluir(int idCampanha)
        {
            try
            {
                var campanha = db.Campanhas.Where(x => x.Id == idCampanha).FirstOrDefault();
                db.Entry<Campanha>(campanha).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json("Erro", JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detalhar(int idCampanha)
        {
            List<DetalheCampanhaVM> detalhes = new List<DetalheCampanhaVM>();
            var campanha = db.Campanhas.Where(x => x.Id == idCampanha).FirstOrDefault();
            var inscricoes = db.Inscricoes.Where(x => x.IdCampanha == idCampanha).ToList();

            foreach (var i in inscricoes)
            {
                DetalheCampanhaVM detalhe = new DetalheCampanhaVM();
                var atleta = db.Atletas.Where(x => x.Id == i.IdAtleta).FirstOrDefault();
                var atividades = db.Atividades.Where(x => x.IdInscricao == i.Id).ToList();
                detalhe.NumeroPeito = atleta.Id.ToString("0000");
                //detalhe.NumeroPeito = i.Id.ToString("0000");
                detalhe.Endereco = atleta.Logradouro + ", " + atleta.Numero.ToString() + ", " + atleta.Bairro;
                detalhe.NomeAtleta = atleta.Nome;
                detalhe.KMCorridos = atividades.Sum(x => x.Distancia);

                detalhe.ValorPago = CalcularValorPago(i, campanha.ValorInscricao);
                detalhe.QtdeMedalhas = atividades.Count();
                string modalidades = "";
                if (i.is5KM)
                    modalidades += "5KM; ";
                if (i.is10KM)
                    modalidades += "10KM; ";
                if (i.is15KM)
                    modalidades += "15KM; ";
                detalhe.Modalidades = modalidades;

                detalhes.Add(detalhe);
            }

            return View(detalhes);
        }

        public ActionResult Download(List<DetalheCampanhaVM> model)
        {
            StringWriter sw = new StringWriter();

            //First line for column names
            sw.WriteLine("\"Id\",\"Atleta\",\"Endereço\",\"KMs\",\"Medalhas\",\"Pagou\",\"Modalidades\"");

            foreach(var det in model)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\""),
                det.NumeroPeito,
                det.NomeAtleta,
                det.Endereco,
                det.KMCorridos,
                det.QtdeMedalhas,
                det.ValorPago,
                det.Modalidades);
            }

            Response.AddHeader("Content-Disposition", "attachment; filename=test.csv");
            Response.ContentType = "text/csv";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Response.Write(sw);
            Response.End();

            //return Json("", JsonRequestBehavior.AllowGet);
            return View();
        }

        private decimal CalcularValorPago(Inscricao i, decimal valorInscricao)
        {
            int marcados = 0;
            if (i.is10KM)
                marcados++;
            if (i.is15KM)
                marcados++;
            if (i.is5KM)
                marcados++;

            return marcados * valorInscricao;
        }
    }
}