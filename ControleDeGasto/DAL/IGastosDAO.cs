using ControleDeGasto.BLL;
using System.Collections;
using System.Windows.Forms;

namespace ControleDeGasto.DAL
{
    interface IGastosDAO
    {
        bool Inserir(Gasto gasto);
        bool Atualizar(Gasto gasto);
        bool Excluir(int id);
        IEnumerable ListarTodos();
        IEnumerable ListarPorDescricao(TextBox txt);
    }
}
