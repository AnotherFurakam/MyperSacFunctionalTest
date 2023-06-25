using System.Net;

namespace MyperSacFunctionalTest.Models
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public Boolean Success { get; set; } = true;
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; } = String.Empty;
    }
}
