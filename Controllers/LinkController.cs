using Microsoft.AspNetCore.Mvc;
using Shortener_Link.DTO;
using Shortener_Link.Interface.Services;

namespace Shortener_Link.Controllers
{
    public class LinkController : Controller
    {
        private readonly ILinkService _linkService;

        public LinkController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/")]
        public IActionResult CreateShortLink(CreateLinkDTO createLink)
        {
            var response = _linkService.CreateShortLink(createLink);

            ViewBag.Response = response;

            return View("Index");
        }

        [HttpGet("{endpoint}")]
        public IActionResult RedirectLink([FromRoute] string endPoint)
        {
            var response = _linkService.RedirectLink(endPoint);

            if (response.Status ==404)
            {
                return View();
            }

            return Redirect(response.Data);
        }
    }
}
