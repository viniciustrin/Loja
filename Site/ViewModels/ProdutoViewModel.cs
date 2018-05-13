using API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Site.ViewModels
{
    public class ProdutoViewModel : BaseViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        [Required]
        [StringLength(100)]

        [Display(Name = "Descrição")]
        public string Desc { get; set; }
        public bool Ativo { get; set; }
        [Required]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
        [Display(Name = "Preço Promocional")]
        public decimal PrecoPromo { get; set; }
        public List<Produto> Produtos { get; set; }
        public int CategoriaId { get; set; }
        public IEnumerable<Categoria> Categorias{ get; set; }

        public string Action
        {
            get
            {
                return (Id != 0) ? "Update" : "Create";
            }
        }
    }
}