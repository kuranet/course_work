using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    public class Payment
    {
        #region Properties
        public DateTime time { get; private set; }
        public TypeOfPayment type { get; private set; }
        public double sum { get; private set; }
        public double purpose { get; private set; }
        #endregion

        public Payment(TypeOfPayment tempType, double tempSum) 
        {
            time = DateTime.Now;
            type = tempType;
            sum = tempSum;
        }

    }
}
