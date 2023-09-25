using Shortener_Link.Interface.Utilities;
using System.Net;

namespace Shortener_Link.Utilities
{
    public class UrlUtilities : IUrlUtilities
    {
        public bool CheckUrlActive(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url.Trim());
                request.Timeout = 5000; // Đặt thời gian chờ kết nối là 5 giây

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Kiểm tra mã trạng thái của phản hồi
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true; // URL hoạt động
                    }
                    else
                    {
                        return false; // URL không hoạt động
                    }
                }
            }
            catch (WebException)
            {
                return false; // URL không hoạt động
            }

        }
    }
}
