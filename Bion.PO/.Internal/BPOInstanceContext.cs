using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO
{
    internal class BPOInstanceContext : IBPOInstanceContext
    {
        public virtual bool IsDisposed { get; protected set; }

        public T Instance<T>()
        {
            return Activator.CreateInstance<T>();
        }


        public object Instance(Type type)
        {
            if (type.IsArray)
            {
                
            }
            return Activator.CreateInstance(type);
        }
        public T Instance<T>(object[] args)
        {
            return Activator.CreateInstance(typeof(T), args).ForceAs<T>();
        }


        public object Instance(Type type, object[] args)
        {
            return Activator.CreateInstance(type, args);
        }
        public void Dispose()
        {
            if (this.IsDisposed) return;
            this.IsDisposed = true;
        }
    }
}
