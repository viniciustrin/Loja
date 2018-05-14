using API.Models;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class BaseController : Controller
    {
        public readonly ApplicationDbContext _context;
        public BaseController()
        {
            _context = new ApplicationDbContext();
        }
    }
}