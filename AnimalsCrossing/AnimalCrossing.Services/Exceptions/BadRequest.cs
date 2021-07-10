using System;

namespace AnimalCrossing.Services.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string error)
        {
            this.Data.Add("error", error);
        }
    }
}
