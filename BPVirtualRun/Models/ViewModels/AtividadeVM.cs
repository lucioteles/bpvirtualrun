using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPVirtualRun.Models.ViewModels
{
    public class AtividadeVM
    {
        public AtividadeVM()
        {
            Inscricao = new Inscricao();
            Atividade = new Atividade();
        }

        public Inscricao Inscricao { get; set; }
        public Atividade Atividade { get; set; }
    }
}