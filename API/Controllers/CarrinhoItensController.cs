using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [Authorize]
    public class CarrinhoItensController : BaseController
    {

        public HttpResponseMessage Get([FromUri] int id)
        {
            try
            {
                var carrinhItens = _context.CarrinhoItens.Where(x => x.CarrinhoId == id).ToList();
                if (carrinhItens == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Carrinho não encontrado!");
                }

                return Request.CreateResponse(HttpStatusCode.OK, carrinhItens);
            }
            catch (DbEntityValidationException ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] CarrinhoItens item)
        {
            var carrinho = _context.Carrinho.Find(item.CarrinhoId);

            if (carrinho == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Carrinho não existe!");
            }

            var produto = _context.Produto.Find(item.ProdutoId);

            if (produto == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Produto não existe!");
            }

            if (!produto.Ativo)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Produto inativo!");
            }

            if (item.Quantidade == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Quantidade deve ser maior que zero!");
            }

            decimal valor = produto.PrecoPromo != 0 ? produto.PrecoPromo : produto.Preco;

            var carrinhoItens = new CarrinhoItens
            {
                CarrinhoId = item.CarrinhoId,
                ProdutoId = item.ProdutoId,
                DataCadastro = DateTime.Now,
                Quantidade = item.Quantidade,
                ValorUnitario = valor,
                ValorTotalItem = valor * item.Quantidade                
            };

            try
            {
                _context.CarrinhoItens.Add(carrinhoItens);
                carrinho.AtualizaTotal(valor * item.Quantidade);
                _context.SaveChanges();                

            }
            catch (DbEntityValidationException ex)
            {
                List<string> validationErrors = new List<string>();

                foreach (var failure in ex.EntityValidationErrors)
                {

                    foreach (var error in failure.ValidationErrors)
                    {
                        validationErrors.Add(error.ErrorMessage);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, validationErrors);
            }
            return Request.CreateResponse(HttpStatusCode.OK, carrinhoItens);
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody] CarrinhoItens item)
        {
            var carrinho = _context.Carrinho.Find(item.CarrinhoId);

            if (carrinho == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Carrinho não existe!");
            }

            var produto = _context.Produto.Find(item.ProdutoId);

            if (produto == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Produto não existe!");
            }

            if (!produto.Ativo)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Produto inativo!");
            }

            if (item.Quantidade == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Quantidade deve ser maior que zero!");
            }

            var carrinhoItens = _context.CarrinhoItens.Where(x => x.ProdutoId == item.ProdutoId && x.CarrinhoId == item.CarrinhoId).FirstOrDefault();

            carrinho.AtualizaTotal(-carrinhoItens.ValorTotalItem);

            carrinhoItens.Quantidade = item.Quantidade;
            carrinhoItens.ValorTotalItem = carrinhoItens.ValorUnitario * item.Quantidade;

            try
            {
                carrinho.AtualizaTotal(carrinhoItens.ValorUnitario * item.Quantidade);
                _context.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                List<string> validationErrors = new List<string>();

                foreach (var failure in ex.EntityValidationErrors)
                {

                    foreach (var error in failure.ValidationErrors)
                    {
                        validationErrors.Add(error.ErrorMessage);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, validationErrors);
            }
            return Request.CreateResponse(HttpStatusCode.OK, carrinhoItens);
        }
    }
}
