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

        public Payment(int tempType, double tempSum) 
        {
            time = DateTime.Now;
            type = (TypeOfPayment)tempType;
            sum = tempSum;
        }

    }
}
