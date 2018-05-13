using System;
using System.ComponentModel.DataAnnotations;



namespace API.Models
{
    public class PedidoItens
        {
            public int Id { get; set; }
            
            [Required]
            public int PedidoId { get; set; }
            public Produto Produto { get; set; }
            [Required]
            public int ProdutoId { get; set; }
            public decimal ValorUnidade { get; set; }
            public decimal ValorTotal { get; set; }
            public int Quantidade { get; set; }
            public DateTime DataCadastro { get; set; }
        }
}
