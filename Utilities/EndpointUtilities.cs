
using Shortener_Link.Interface.Utilities;

namespace Shortener_Link.Utilities
{
    public class EndpointUtilities : IEndpointUtilities
    {
        public string GenerateRandomEndpoint(int length)
        {
            string[] array = new string[] { "A", "B", "C", "D", "E", "F", "a", "b", "c", "e", "f", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string randomEndpoint = "";

            for (int dem = 0; dem < length; dem++)
            {
                var random = new Random();
                int randomNumber = random.Next(0, array.Length);
                randomEndpoint += array[randomNumber];
            }

            return randomEndpoint;
        }
    }   
}
