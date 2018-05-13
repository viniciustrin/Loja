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

        public HttpResponseMessage Get([FromUri] int id)
        {
            try
            {
                var pedido = _context.Pedido.Find(id);
                if (pedido == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Pedido não encontrado!");
                }

                var carrinho = _context.Carrinho.Find(pedido.CarrinhoId);
                var cliente = _context.Cliente.Find(carrinho.ClienteId);
                var itens = _context.PedidoItens.Where(x => x.PedidoId == pedido.Id).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, pedido);
            }
            catch (DbEntityValidationException ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }



        [HttpPost]
        public HttpResponseMessage Post([FromBody] Pedido pedido)
        {
            var carrinho = _context.Carrinho.Find(pedido.CarrinhoId);

            if (carrinho == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Carrinho não existe!");
            }


            var carrinhoItens = _context.CarrinhoItens.Where(x => x.CarrinhoId == pedido.CarrinhoId).ToList();

            if (carrinhoItens.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "O carrinho está vazio!");
            }

            pedido.Valor = carrinho.Total;
            pedido.DataCadastro = DateTime.Now;


            try
            {
                _context.Pedido.Add(pedido);

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
                    _context.PedidoItens.Add(pedidoitens);
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
