using API.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{

    [Authorize]
    public class ClienteController : ApiController
    {
        private ApplicationDbContext _context;

        public ClienteController()
        {
            _context = new ApplicationDbContext();

        }


        public HttpResponseMessage Get()
        {
            try
            {
                var lista = _context.Clientes.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
            
        }


        [HttpPost]
        public HttpResponseMessage Post([FromBody] Cliente cli)
        {
            cli.DataCadastro = DateTime.Now;
            try
            {
                _context.Clientes.Add(cli);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest,err);                
            }
            return Request.CreateResponse(HttpStatusCode.OK, cli);
        }

    }
}
