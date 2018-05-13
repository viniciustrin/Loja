using System;

namespace API.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public Carrinho Carrinho { get; set; }
        public int CarrinhoId { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}