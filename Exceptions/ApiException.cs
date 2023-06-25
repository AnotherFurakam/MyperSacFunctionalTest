using System.Net;

namespace MyperSacFunctionalTest.Exceptions
{
    public class ApiException: Exception
    {

        public HttpStatusCode StatusCode;


        public ApiException(HttpStatusCode StatusCode,string message):base(message)
        {
            this.StatusCode = StatusCode;
        }
    }
}
