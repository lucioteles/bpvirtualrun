using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPVirtualRun.Models.ViewModels
{
    public class DetalheCampanhaVM
    {
        public string NumeroPeito { get; set; }
        public string NomeAtleta { get; set; }
        public int KMCorridos { get; set; }
        public int QtdeMedalhas { get; set; }
        public decimal ValorPago { get; set; }
        public string Endereco { get; set; }
        public string Modalidades { get; set; }
    }
}