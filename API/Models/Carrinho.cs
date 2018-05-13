using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        [Required]
        public int ClienteId { get; set; }
        public DateTime DataCadastro { get; set; }
        public decimal Total { get; set; }

        public void AtualizaTotal(decimal valor)
        {
            this.Total += valor;
        }

    }
}