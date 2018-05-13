using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public Carrinho Carrinho { get; set; }
        [Required]
        public int CarrinhoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<PedidoItens> Itens { get; set; }
    }
}