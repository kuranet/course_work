using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class InputPayment:Payment
    {
        public InputPurpose purpose { get; private set; }
        public InputPayment( double tempSum, InputPurpose pur) : base( tempSum) { purpose = pur; }
    }
}
