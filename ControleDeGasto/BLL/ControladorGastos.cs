using ControleDeGasto.DAL;
using System.Collections;
using System.Windows.Forms;

namespace ControleDeGasto.BLL
{
    class ControladorGastos
    {
        private IGastosDAO dao;

        public ControladorGastos()
        {
            dao = DAOFactory.CriarGastosDAO();
        }

        public bool Salvar(Gasto gasto)
        {
            return dao.Inserir(gasto);
        }


        public bool Atualizar(Gasto gasto)
        {
            return dao.Atualizar(gasto);
        }

        public bool Excluir(int id)
        {
            return dao.Excluir(id);
        }

        public IEnumerable ListarTodos()
        {
            return dao.ListarTodos();
        }

        public IEnumerable ListarPorDescricao(TextBox txt)
        {
            return dao.ListarPorDescricao(txt);
        }
    }
}
