using Bion.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Bion.PO
{
    public class BUnpackException : BException
    {
        public BUnpackException()
        {
        }
        

        public BUnpackException(string message) : base(message)
        {
        }

        public BUnpackException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BUnpackException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
