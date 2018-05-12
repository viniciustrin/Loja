using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        [Required]
        public DateTime DataCadastro { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
    }
}