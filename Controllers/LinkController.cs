using Microsoft.AspNetCore.Mvc;
using QRCoder;
using Shortener_Link.DTO;
using Shortener_Link.Interface.Services;
using System.Drawing;
using System.Drawing.Imaging;

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

        [HttpGet("qrcode")]
        public IActionResult GenerateQRcode()
        {
            using(MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode("hello", QRCodeGenerator.ECCLevel.Q);
                QRCode qRCode = new QRCode(qRCodeData);
                using (Bitmap bitmap = qRCode.GetGraphic(20))
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRcode = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }

            return View();
        }
    }
}
