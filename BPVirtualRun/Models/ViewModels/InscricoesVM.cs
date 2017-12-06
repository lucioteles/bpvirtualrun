using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPVirtualRun.Models.ViewModels
{
    public class InscricoesVM
    {
        public int Id { get; set; }
        public int IdAtleta { get; set; }
        public int IdCampanha { get; set; }
        public string NomeCampanha { get; set; }
        public bool is5KM { get; set; }
        public bool is10KM { get; set; }
        public bool is15KM { get; set; }

    }
}