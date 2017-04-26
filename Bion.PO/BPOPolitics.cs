using Bion.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO
{
    [Serializable]
    public class BPOPolitics : IBPOPolitics
    {
        #region -------- Property: ReadDepth --------
        private BPOPoliticsReadDepth _ReadDepth;
        public virtual BPOPoliticsReadDepth ReadDepth
        {
            get { return this._ReadDepth; }
            set { this._ReadDepth = value; }
        }
        #endregion [-------- ReadDepth --------]

        #region -------- Property: MemberType --------
        private BPOPoliticsMemberTypes _MemberType;
        public virtual BPOPoliticsMemberTypes MemberType
        {
            get { return this._MemberType; }
            set { this._MemberType = value; }
        }
        #endregion [-------- MemberType --------]


    }

    public enum BPOPoliticsReadDepth
    {
        Public = BLORValues.V01,
        Protected = BLORValues.V02,
        Private = BLORValues.V03,
        Inaccessibile = BLORValues.V04,

    }

    public enum BPOPoliticsMemberTypes
    {
        Property = BLORValues.V01,
        Field = BLORValues.V02,
    }
}
