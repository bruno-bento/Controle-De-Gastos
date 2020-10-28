using ControleDeGasto.BLL;
using ControleDeGasto.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ControleDeGasto
{
    public partial class frmControleGastos : Form
    {
        public frmControleGastos()
        {
            InitializeComponent();
            controlador = new ControladorGastos();
        }
        private  ControladorGastos controlador;

        /*Evento Click*/
        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            double valor;
            string descricao;
            if (btnEditar.Text == "Cancelar")
            {
                int selectedRowIndex = dgvDados.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvDados.Rows[selectedRowIndex];
                int id = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                GastosContext db = new GastosContext();
                Gasto gasto = db.Gastos.Where(a => a.Id == id).FirstOrDefault();
                gasto.Valor = Convert.ToDouble(txtValor.Text);
                gasto.Descricao = txtDescricao.Text;
                gasto.Data = dtpDataGasto.Value;
                controlador.Atualizar(gasto);
                LimparCampos();
                exibirInfo();
                btnNovo.Enabled = true;
                btnExcluir.Enabled = true;
                btnEditar.Text = "Editar";
            }
            else
            {
                if (txtValor.Text != "")
                {
                    valor = Convert.ToDouble(txtValor.Text);
                }
                else
                {
                    MessageBox.Show("Informe o Valor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtValor.Focus();
                    return;
                }
                if (txtDescricao.Text != "")
                {
                    descricao = txtDescricao.Text;
                }
                else
                {
                    MessageBox.Show("Informe a Descrição.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescricao.Focus();
                    return;
                }
                DateTime data = dtpDataGasto.Value;
                Gasto gasto = new Gasto(data, valor, descricao);
                controlador.Salvar(gasto);
                exibirInfo();
            }
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDados.SelectedRows.Count == 1)
                {
                    int id = Convert.ToInt32(dgvDados.CurrentRow.Cells[0].Value);
                    using (GastosContext db = new GastosContext())
                    {
                        if (MessageBox.Show("Deseja excluir este cadastro?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            controlador.Excluir(id);
                        }
                        LimparCampos();
                        exibirInfo();
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um cadastro para Excluir!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível excluir o cadastro" + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void BtnTodos_Click(object sender, EventArgs e)
        {
            exibirInfo();
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void dgvDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            using (GastosContext db = new GastosContext())
            {

                if (dgvDados.SelectedRows.Count == 1)
                {
                    int selectedRowIndex = dgvDados.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgvDados.Rows[selectedRowIndex];
                    int id = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                    var consulta = (from p in db.Gastos.Where(a => a.Id == id) select p).First();

                    txtValor.Text = consulta.Valor.ToString();
                    dtpDataGasto.Text = consulta.Data.ToString();
                    txtDescricao.Text = consulta.Descricao;
                    btnExcluir.Enabled = true;
                }
            }

        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDados.SelectedRows.Count == 1)
            {
                if (btnEditar.Text == "Editar")
                {
                    btnNovo.Enabled = false;
                    btnExcluir.Enabled = false;
                    btnEditar.Text = "Cancelar";
                }
                else
                {
                    LimparCampos();
                    btnExcluir.Enabled = true;
                    btnNovo.Enabled = true;
                    btnEditar.Text = "Editar";
                }
            }
            else
            {
                MessageBox.Show("Selecione uma linha para editar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /*Outros Eventos*/
        private void frmControleGastos_Load(object sender, EventArgs e)
        {
            dgvDados.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 232, 166); 
            exibirInfo();
        }
        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
            else if ((int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',' || e.KeyChar == '.')
            {
                e.KeyChar = ',';
                if (txtValor.Text.IndexOf(",") == -1)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if ((int)e.KeyChar == 13)
            {
                txtDescricao.Focus();
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void dgvDados_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView gridView;
            gridView = (DataGridView)sender;
            gridView.ClearSelection();
        }
        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvDados.DataSource = controlador.ListarPorDescricao(txtPesquisa);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao relizar pesquisa!" + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*Métodos*/
        public void exibirInfo()
        {
            try
            {
                dgvDados.DataSource = controlador.ListarTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados!" + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimparCampos()
        {
            dtpDataGasto.Value = DateTime.Now;
            txtValor.Text = "";
            txtDescricao.Text = "";
            btnExcluir.Enabled = false;
        }
    }
}
