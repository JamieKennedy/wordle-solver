using Microsoft.AspNetCore.Http;

namespace Common.Models.Exceptions.Generic
{
    public class BadRequestException : Exception
    {
        public const int STATUS_CODE = StatusCodes.Status400BadRequest;

        protected BadRequestException(string message) : base(message) { }
    }
}