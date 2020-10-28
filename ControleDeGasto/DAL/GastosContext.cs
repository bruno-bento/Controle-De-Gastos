using ControleDeGasto.BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeGasto.DAL
{
    class GastosContext : DbContext
    {
        public GastosContext(): base("localdb")
        {

        }

        public DbSet<Gasto> Gastos { get; set; }
    }
}
