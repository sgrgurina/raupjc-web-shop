using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Shop
{
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException()
        {
        }

        public DuplicateItemException(string message) : base(message)
        {
        }

        public DuplicateItemException(string message, Exception innerException) : base(message, innerException)
        { 
        }
    }
}