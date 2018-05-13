using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Produto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        [Required]
        [StringLength(100)]
        public string Desc { get; set; }
        public bool Ativo { get; set; }
        [Required]
        public decimal Preco { get; set; }
        public decimal PrecoPromo { get; set; }
    }
}