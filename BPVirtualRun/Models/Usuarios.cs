using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BPVirtualRun.Models
{
    [Table("Usuarios")]
    public class Usuarios
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        [Column("dtCadastro")]
        public DateTime DataCadastro { get; set; }
        [Column("admin")]
        public bool isAdmininstrador { get; set; }
    }
}