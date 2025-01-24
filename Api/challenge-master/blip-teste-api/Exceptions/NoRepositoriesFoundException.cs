using System;

namespace blip_teste_api.Exceptions
{
    public class NoRepositoriesFoundException : Exception
    {
        public NoRepositoriesFoundException(string message) : base(message)
        {
        }
    }
}
