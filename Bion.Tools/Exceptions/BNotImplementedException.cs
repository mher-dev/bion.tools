using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools.Exceptions
{
    internal class BENotImplementedException : NotImplementedException, IBException
    {
        public BENotImplementedException()
        {
        }

        public BENotImplementedException(string message) : base(message)
        {
        }

        public BENotImplementedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BENotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
