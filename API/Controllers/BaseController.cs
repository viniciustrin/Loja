using API.Models;
using System.Web.Http;

namespace API.Controllers
{
    public class BaseController : ApiController
    {
        public ApplicationDbContext _context;

        public BaseController()
        {
            _context = new ApplicationDbContext();

        }
    }
}
