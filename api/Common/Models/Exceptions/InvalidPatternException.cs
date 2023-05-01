using Common.Models.Exceptions.Generic;

namespace Common.Models.Exceptions
{
    public class InvalidPatternException : BadRequestException
    {
        public InvalidPatternException(string message) : base(message) { }
        public InvalidPatternException() : base("Invalid Pattern") { }
    }
}