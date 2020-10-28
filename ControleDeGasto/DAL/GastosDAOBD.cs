using ControleDeGasto.BLL;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ControleDeGasto.DAL
{
    class GastosDAOBD : IGastosDAO
    {
      

        public bool Inserir(Gasto gasto)
        {
            using (var db = new GastosContext())
            {
                if (db.Database.Connection != null)
                {
                    db.Gastos.Add(gasto);
                    db.SaveChanges(); 
                    return true;
                }
                return false;

            }   
        }
        public bool Atualizar(Gasto gasto)
        {
            using (GastosContext db  = new GastosContext())
            {
                if (db.Database.Connection != null)
                {
                    db.Gastos.Attach(gasto);
                    db.Entry(gasto).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool Excluir(int id)
        {
            using (GastosContext db = new GastosContext())
            {
                if (db.Database.Connection != null )
                {
                    Gasto gasto = db.Gastos.FirstOrDefault(a => a.Id == id);
                    db.Gastos.Remove(gasto);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public IEnumerable ListarTodos()
        {
            using (GastosContext db = new GastosContext())
            {
                if (db.Database.Connection != null)
                {

                    var consulta = from p in db.Gastos
                                   select new
                                   {
                                       Id = p.Id,
                                       Descrição = p.Descricao,
                                       Valor = p.Valor,
                                       Data = p.Data,
                                   };

                    return consulta.ToList();
                }
                return null;
            }
        }

        public IEnumerable ListarPorDescricao(TextBox txt)
        {
            using (GastosContext db = new GastosContext())
            {
                if (db.Database.Connection != null)
                {
                    var consulta = from p in db.Gastos.Where(a => a.Descricao.Contains(txt.Text))
                                   select new
                                   {
                                       Id = p.Id,
                                       Descrição = p.Descricao,
                                       Valor = p.Valor,
                                       Data = p.Data,
                                   };
                    return consulta.ToList();
                }
                return null;
            }
        }


    }
}
