using API.Models;
using Site.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClienteController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Autenticado = User.Identity.IsAuthenticated;
            var vm = new ClienteViewModel
            {
                Clientes = _context.Cliente.ToList(),
                Heading = "Adicionar Cliente",
                Botao = "Adicionar"
            };
            return View(vm);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Create(ClienteViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                vm.Clientes = _context.Cliente.ToList();
                vm.Heading = "Adicionar Cliente";
                vm.Botao = "Adicionar";
                return View(vm);
            }

            var cliente = new Cliente
            {
                Nome = vm.Nome,
                Email = vm.Email,
                DataCadastro = DateTime.Now
            };
            _context.Cliente.Add(cliente);
            _context.SaveChanges();
            return RedirectToAction("Create", "Cliente");

        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewBag.Autenticado = User.Identity.IsAuthenticated;
            var cliente = _context.Cliente.Single(x => x.Id == id);

            var vm = new ClienteViewModel
            {
                Id = id,
                Clientes = _context.Cliente.ToList(),
                Nome = cliente.Nome,
                Email = cliente.Email,
                Heading = "Editar Cliente",
                Botao = "Editar"
            };
            return View("Create", vm);
        }



        [Authorize]
        [HttpPost]
        public ActionResult Update(ClienteViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Clientes = _context.Cliente.ToList();
                vm.Heading = "Editar Cliente";
                vm.Botao = "Editar";
                return View("Create", vm);
            }

            var cliente = _context.Cliente.Single(x => x.Id == vm.Id);

            cliente.Nome = vm.Nome;
            cliente.Email = vm.Email;

            _context.SaveChanges();
            return RedirectToAction("Create", "Cliente");

        }
    }
}