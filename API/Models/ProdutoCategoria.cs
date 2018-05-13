using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class ProdutoCategoria
    {
        public Produto Produto { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ProdutoId { get; set; }
        public Categoria Categoria { get; set; }
        [Key]
        [Column(Order = 2)]
        public int CategoriaId { get; set; }
    }
}