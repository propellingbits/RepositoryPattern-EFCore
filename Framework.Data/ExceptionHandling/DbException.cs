using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Data.ExceptionHandling
{
    public class DbException : Exception
    {
        public DbException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
