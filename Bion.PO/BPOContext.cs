using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO
{
    public sealed class BPOContext
    {

        private IBPOPolitics _Politics;

        public IBPOPolitics Politics
        {
            get { return _Politics ?? (this._Politics = new BPOPolitics()); }
            set { _Politics = value; }
        }

        public IBPOInstanceContext CustomInstanceContext { get; set; }

        public IBPOSerializationContext CustomSerializationContext { get; set; }

        public int? CompressionLevel { get; set; }


    }
}
