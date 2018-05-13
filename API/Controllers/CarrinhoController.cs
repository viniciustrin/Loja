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
                var lista = _context.Carrinho.ToList();
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

            var cliente = _context.Cliente.Find(carrinho.ClienteId);

            if (cliente == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Cliente não existe!");
            }

            carrinho.DataCadastro = DateTime.Now;
            carrinho.Total = 0;

            try
            {
                _context.Carrinho.Add(carrinho);
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
