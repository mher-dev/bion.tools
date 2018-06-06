using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BionCore.Core
{
    public class BException : Exception, IBException
    {
        public BException()
        {
        }

        public BException(string message) : base(message)
        {
        }

        public BException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
