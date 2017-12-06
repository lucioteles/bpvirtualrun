using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BPVirtualRun.Models
{
    [Table("Inscricoes")]
    public class Inscricao
    {
        public int Id { get; set; }
        public int IdAtleta { get; set; }
        public int IdCampanha { get; set; }
        public bool is5KM { get; set; }
        public bool is10KM { get; set; }
        public bool is15KM { get; set; }
    }
}