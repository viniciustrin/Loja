using API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Site.ViewModels
{
    public class CategoriaViewModel : BaseViewModel
    {

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public List<Categoria> Categorias { get; set; }
        public string Action
        {
            get
            {
                return (Id != 0) ? "Update" : "Create";
            }
        }

    }
}