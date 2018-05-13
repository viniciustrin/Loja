using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [Authorize]
    public class CarrinhoItensController : BaseController
    {
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
    }
}
