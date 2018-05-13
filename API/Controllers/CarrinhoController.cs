using API.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [Authorize]
    public class CarrinhoController : BaseController
    {
        public HttpResponseMessage Get()
        {
            try
            {
                var lista = _context.Carrinhos.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (DbEntityValidationException ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }

        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Carrinho carrinho)
        {

            //var cliente = _context.Clientes.Single(x => x.Id == carrinho.ClienteId);
            var cliente = _context.Clientes.Find(carrinho.ClienteId);

            if (cliente == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Cliente não existe!");
            }

            carrinho.DataCadastro = DateTime.Now;
            carrinho.Total = 0;

            try
            {
                _context.Carrinhos.Add(carrinho);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
            return Request.CreateResponse(HttpStatusCode.OK, carrinho);
        }
    }
}
