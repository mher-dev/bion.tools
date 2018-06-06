using BionCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools.Console.Exceptions
{
    public class BCException : Exception, IBException
    {
        public BCException() : base() { }
        public BCException(string message) : base(message) { }
        public BCException(string message, params Exception[] innerExceptions) : base(message)
        {
            this.InnerExceptions = innerExceptions;
        }

        internal void AddExceptions(params Exception[] exceptions)
        {
            this.InnerExceptions = this.InnerExceptions.Concat(exceptions).ToArray();
        }


        private Exception[] _InnerExceptions;
        public virtual Exception[] InnerExceptions
        {
            get { return this._InnerExceptions ?? (this._InnerExceptions = new Exception[] { }); }
            private set { this._InnerExceptions = value; }
        }



    }
}
