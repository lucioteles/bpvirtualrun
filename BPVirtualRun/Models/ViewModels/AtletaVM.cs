using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BPVirtualRun.Models;
using BPVirtualRun.Models.ViewModels;

namespace BPVirtualRun.Models.ViewModels
{
    public class AtletaVM
    {
        public Atleta Atleta { get; set; }
        public Usuarios Usuario { get; set; }
        public Inscricao Inscricao { get; set; }
        public Campanha Campanha { get; set; }
        public List<InscricoesVM> Inscricoes { get; set; }
    }
}