using API.Models;
using Site.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProdutoController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Autenticado = User.Identity.IsAuthenticated;
            var vm = new ProdutoViewModel
            {
                Produtos = _context.Produto.ToList(),
                Categorias = _context.Categoria.Where(x => x.Ativo == true).ToList(),
                Heading = "Adicionar Produto",
                Botao = "Adicionar"
            };
            return View(vm);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Create(ProdutoViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                vm.Produtos = _context.Produto.ToList();
                vm.Categorias = _context.Categoria.Where(x => x.Ativo == true).ToList();
                vm.Heading = "Adicionar Produto";
                vm.Botao = "Adicionar";
                return View(vm);
            }

            var produto = new Produto
            {
                Nome = vm.Nome,
                Ativo = vm.Ativo,
                Desc = vm.Desc,
                Preco = vm.Preco,
                PrecoPromo = vm.PrecoPromo
            };

            var produtoCategoria = new ProdutoCategoria
            {
                CategoriaId = vm.CategoriaId,
                ProdutoId = produto.Id
            };
            _context.Produto.Add(produto);
            _context.ProdutoCategoria.Add(produtoCategoria);
            _context.SaveChanges();
            return RedirectToAction("Create", "Produto");

        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewBag.Autenticado = User.Identity.IsAuthenticated;
            var produto = _context.Produto.Single(x => x.Id == id);
            var produtoCategoria = _context.ProdutoCategoria.Where(x => x.ProdutoId == id).FirstOrDefault();

            var vm = new ProdutoViewModel
            {
                Id = id,
                Produtos = _context.Produto.ToList(),
                Nome = produto.Nome,
                Ativo = produto.Ativo,
                Desc = produto.Desc,
                Preco = produto.Preco,
                PrecoPromo = produto.PrecoPromo,
                CategoriaId = produtoCategoria == null ? 0 : produtoCategoria.CategoriaId,
                Categorias = _context.Categoria.Where(x => x.Ativo == true).ToList(),
                Heading = "Editar Produto",
                Botao = "Editar"
            };
            return View("Create", vm);
        }



        [Authorize]
        [HttpPost]
        public ActionResult Update(ProdutoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Produtos = _context.Produto.ToList();
                vm.Categorias = _context.Categoria.Where(x => x.Ativo == true).ToList();
                vm.Heading = "Editar Produto";
                vm.Botao = "Editar";
                return View("Create", vm);
            }
            var produto = _context.Produto.Single(x => x.Id == vm.Id);

            produto.Nome = vm.Nome;
            produto.Ativo = vm.Ativo;
            produto.Desc = vm.Desc;
            produto.Preco = vm.Preco;
            produto.PrecoPromo = vm.PrecoPromo;

            var produtoCategoria = _context.ProdutoCategoria.Where(x => x.ProdutoId == vm.Id).FirstOrDefault();

            if (produtoCategoria == null)
            {
                produtoCategoria = new ProdutoCategoria
                {
                    CategoriaId = vm.CategoriaId,
                    ProdutoId = produto.Id
                };
                _context.ProdutoCategoria.Add(produtoCategoria);
            }            

            _context.SaveChanges();
            return RedirectToAction("Create", "Produto");

        }
    }
}