using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class OutputPayment:Payment
    {
        public OutputPurpose purpose { get; private set; }
        public OutputPayment( double tempSum, OutputPurpose pur) : base( tempSum) { purpose = pur; }
    }
}
