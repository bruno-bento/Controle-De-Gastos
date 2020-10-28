using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeGasto.DAL
{
    class DAOFactory
    {
        public static IGastosDAO CriarGastosDAO()
        {
            return new GastosDAOBD();
        }
    }
}
