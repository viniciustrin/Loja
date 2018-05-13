using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CarrinhoItens
    {
        public int Id { get; set; }
        public Carrinho Carrinho { get; set; }
        [Required]
        public int CarrinhoId { get; set; }
        public Produto Produto { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotalItem { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}