using API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Site.ViewModels
{
    public class ClienteViewModel : BaseViewModel
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
        public List<Cliente> Clientes { get; set; }
        public string Action
        {
            get
            {
                return (Id != 0) ? "Update" : "Create";
            }
        }
    }
}