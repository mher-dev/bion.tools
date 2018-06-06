using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools
{
    public class BException : Exception
    {
        public BException() : base() { }
        public BException(string message) : base(message) { }
        public BException(string message, Exception innerException) : base(message, innerException) { }

        public static BException<T> From<T>(T exception) where T : Exception, new()
        {
            return new BException<T>(message: exception.Message, innerException: exception);
        }

        public static BException<T> From<T>(string message, T exception) where T : Exception, new()
        {
            return new BException<T>(message: exception.Message, innerException: exception);
        }
    }


    public class BException<T> : BException
        where T : Exception, new()
    {
        public Type InnerExceptionType { get; private set; }

        public BException()
            : base(message: null, innerException: new T())
        {
            this.InnerExceptionType = typeof(T);
        }

        public BException(string message)
            : base(message)
        {
            this.InnerExceptionType = typeof(T);
        }
        public BException(string message, T innerException)
            : base(message, innerException)
        {
            this.InnerExceptionType = typeof(T);
        }
    }
}
