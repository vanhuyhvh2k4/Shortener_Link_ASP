using QRCoder;
using Shortener_Link.Interface.Utilities;
using System.Drawing.Imaging;
using System.Drawing;

namespace Shortener_Link.Utilities
{
    public class QRcodeUtilities : IQRcodeUtilities
    {
        public string GenerateQRcode(string payload)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode("hello", QRCodeGenerator.ECCLevel.Q);
                QRCode qRCode = new QRCode(qRCodeData);
                using (Bitmap bitmap = qRCode.GetGraphic(20))
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }

        }
    }
}
