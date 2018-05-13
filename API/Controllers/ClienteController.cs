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
    public class ClienteController : BaseController
    {

        public HttpResponseMessage Get()
        {
            try
            {
                var lista = _context.Cliente.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (DbEntityValidationException ex)
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
                _context.Cliente.Add(cli);
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
            return Request.CreateResponse(HttpStatusCode.OK, cli);
        }
    }
}
