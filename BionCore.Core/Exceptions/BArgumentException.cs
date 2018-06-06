using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BionCore.Core
{
    /// <summary>
    /// The exception that is thrown when one of the arguments provided to a method is
    ///  not valid.
    /// </summary>
    public class BArgumentException : ArgumentException, IBException
    {
        /// <summary>
        /// Initializes a new instance of the System.ArgumentException class.
        /// </summary>
        public BArgumentException()
        {
        }

        public BArgumentException(string message) : base(message)
        {
        }

        public BArgumentException(string message, string paramName) : base(message, paramName)
        {
        }

        public BArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BArgumentException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
        {
        }

        protected BArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
