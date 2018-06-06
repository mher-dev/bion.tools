using BionCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{


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
