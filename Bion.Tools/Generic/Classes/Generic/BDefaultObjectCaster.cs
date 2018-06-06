using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools
{
    public sealed class BDefaultObjectCaster : IBObjectCaster
    {
        public BDefaultObjectCaster() { }
        public BDefaultObjectCaster(bool isStrict)
        {
            this.IsStrict = isStrict;
        }
        public bool IsStrict { get; private set; }
        public bool Cast(Type originType, Type destinationType, object originObject, out object destinationObject)
        {
            //Es un nulo, nos tapamos las espaldas
            if (originObject == null)
            {
                var defValue = destinationType.GetDefaultValue();

                // El valor del origen no puede ser nulo. Tenemos ante nosotros una inconsistencia
                // Si estamos en modo estricto, será necesario devolver false                    
                destinationObject = defValue;
                return !this.IsStrict;
            }
            else
            {
                //Miramos si podemos hacer un cast polimórfico
                if (destinationType.IsAssignableFrom(originType))
                {
                    destinationObject = originObject;
                    return true;
                }
                else
                {
                    //Hay que hacer una conversión fea y no lo vamos a hacer
                    destinationObject = null;
                    return false;
                }
            }

        }
    }
}
