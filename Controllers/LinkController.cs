using Microsoft.AspNetCore.Mvc;

namespace Shortener_Link.Controllers
{
    public class LinkController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
