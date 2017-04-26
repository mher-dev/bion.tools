using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Bion.Core
{
    public class BArgumentNullException : ArgumentNullException, IBException
    {
        public BArgumentNullException()
        {
        }

        public BArgumentNullException(string paramName) : base(paramName)
        {
        }

        public BArgumentNullException(string paramName, string message) : base(paramName, message)
        {
        }

        public BArgumentNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BArgumentNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
