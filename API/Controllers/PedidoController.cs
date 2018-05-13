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
    public class PedidoController : BaseController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Pedido pedido)
        {
            var carrinho = _context.Carrinhos.Find(pedido.CarrinhoId);

            if (carrinho == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Carrinho não existe!");
            }


            var carrinhoItens = _context.CarrinhosItens.Where(x => x.CarrinhoId == pedido.CarrinhoId).ToList();

            if (carrinhoItens.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "O carrinho está vazio!");
            }

            pedido.Valor = carrinho.Total;
            pedido.DataCadastro = DateTime.Now;


            try
            {
                _context.Pedidos.Add(pedido);

                foreach (var item in carrinhoItens)
                {
                    var pedidoitens = new PedidoItens
                    {
                        DataCadastro = DateTime.Now,
                        ProdutoId = item.ProdutoId,
                        Quantidade = item.Quantidade,
                        ValorTotal  = item.ValorTotalItem,
                        ValorUnidade = item.ValorUnitario
                    };
                    _context.PedidosItens.Add(pedidoitens);
                }

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
            return Request.CreateResponse(HttpStatusCode.OK, pedido);
        }
    }
}
