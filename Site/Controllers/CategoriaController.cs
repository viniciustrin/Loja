using API.Models;
using Site.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class CategoriaController : BaseController
    {
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Autenticado = User.Identity.IsAuthenticated;
            var vm = new CategoriaViewModel
            {
                Categorias = _context.Categoria.ToList(),
                Heading = "Adicionar Categoria",
                Botao = "Adicionar"
            };
            return View(vm);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Create(CategoriaViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                vm.Categorias = _context.Categoria.ToList();
                vm.Heading = "Adicionar Categoria";
                vm.Botao = "Adicionar";
                return View(vm);
            }

            var categoria = new Categoria
            {
                Nome = vm.Nome,
                Ativo = vm.Ativo,
                DataCadastro = DateTime.Now
            };
            _context.Categoria.Add(categoria);
            _context.SaveChanges();
            return RedirectToAction("Create", "Categoria");

        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewBag.Autenticado = User.Identity.IsAuthenticated;
            var categoria = _context.Categoria.Single(x => x.Id == id);

            var vm = new CategoriaViewModel
            {
                Id = id,
                Categorias = _context.Categoria.ToList(),
                Nome = categoria.Nome,
                Ativo = categoria.Ativo,
                Heading = "Editar Categoria",
                Botao = "Editar"
            };
            return View("Create", vm);
        }



        [Authorize]
        [HttpPost]
        public ActionResult Update(CategoriaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categorias = _context.Categoria.ToList();
                vm.Heading = "Editar Categoria";
                vm.Botao = "Editar";
                return View("Create", vm);
            }

            var categoria = _context.Categoria.Single(x => x.Id == vm.Id);

            categoria.Nome = vm.Nome;
            categoria.Ativo = vm.Ativo;

            _context.SaveChanges();
            return RedirectToAction("Create", "Categoria");

        }
    }
}