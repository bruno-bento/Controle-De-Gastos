using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeGasto.BLL
{
    class Gasto
    {
        private int id;
        private DateTime data;
        private double valor;
        private string descricao;

        public Gasto(DateTime data, double valor, string descricao)
        {
            this.Id = id;
            this.Data = data;
            this.Valor = valor;
            this.Descricao = descricao;

        }

        public Gasto()
        {

        }
        [Required]
        public int Id { get => id; set => id = value; }

        [Required]
        public DateTime Data { get => data; set => data = value; }

        [Required]
        public double Valor { get => valor; set => valor = value; }

        [Required]
        public string Descricao { get => descricao; set => descricao = value; }
    }
}
