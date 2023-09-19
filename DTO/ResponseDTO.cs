namespace Shortener_Link.DTO
{
    public class ResponseDTO<T>
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public T ?Data { get; set; }
    }
}
