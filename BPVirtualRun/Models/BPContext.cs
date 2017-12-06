using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BPVirtualRun.Models
{
    public class BPContext : DbContext
    {
        public BPContext() : base("bpConnection")
        {

        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Campanha> Campanhas { get; set; }
        public DbSet<Atleta> Atletas { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }
        public DbSet<Atividade> Atividades { get; set; }
    }
}