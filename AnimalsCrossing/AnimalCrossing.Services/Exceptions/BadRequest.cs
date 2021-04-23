using System;
using System.Collections.Generic;
using System.Text;

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
