using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Shop
{
    public class DuplicateCategoryException : Exception
    {
        public DuplicateCategoryException()
        {
        }

        public DuplicateCategoryException(string message) : base(message)
        {
        }

        public DuplicateCategoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
