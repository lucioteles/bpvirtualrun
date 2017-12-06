using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BPVirtualRun.Models
{
    [Table("Campanhas")]
    public class Campanha
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Column("dtCriacao")]
        public DateTime? Criacao { get; set; }
        [Column("dtInicioInscricao")]
        public DateTime InicioInscricao { get; set; }
        [Column("dtFimInscricao")]
        public DateTime FimInscricao { get; set; }
        [Column("qtMaxInscritos")]
        public int MaximoInscritos { get; set; }
        [Column("qtMinInscritos")]
        public int MinimoInscritos { get; set; }
        [Column("valorInscricao")]
        public decimal ValorInscricao { get; set; }
        public bool is5KM { get; set; }
        public bool is10KM { get; set; }
        public bool is15KM { get; set; }
    }
}