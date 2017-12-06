using BPVirtualRun.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BPVirtualRun.Controllers
{
    public class ContatoController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            ContatoVM contato = new ContatoVM();
            return View(contato);
        }

        [HttpPost]
        public ActionResult Index(ContatoVM model)
        {
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(model.Email, model.Nome);
                message.To.Add("lucio.teles@ivia.com.br");
                message.Subject = model.Assunto;
                message.Body = model.Mensagem;
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("lucio.teles@ivia.com.br", "Btv305?!");

                try
                {
                    smtp.Send(message);
                    ViewBag.Sucesso = "Mensagem enviada com sucesso! Em breve entramos em contato com você!";
                }
                catch(Exception ex)
                {
                    ViewBag.Erro = "Não foi possível enviar o e-mail de contato. Por favor tente novamente mais tarde.";
                }
            }

            return View();
        }
    }
}