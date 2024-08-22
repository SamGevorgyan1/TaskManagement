using System.Net;

namespace TaskManagement.Application.Exceptions
{
    public abstract class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected AppException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        protected AppException(string? message, Exception? innerException, HttpStatusCode statusCode) : base(message,
            innerException)
        {
            StatusCode = statusCode;
        }
    }
}