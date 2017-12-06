using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BPVirtualRun.Models
{
    [Table("Atividades")]
    public class Atividade
    {
        public int Id { get; set; }
        public int IdInscricao { get; set; }
        public int Distancia { get; set; }
        [Column("dtRegistro")]
        public DateTime DataRegistro { get; set; }
        public string Tempo { get; set; }
        public string LocalEvento { get; set; }
    }
}