using System;

namespace WalletSystem
{
    public class Payment
    {
        #region Properties
        public DateTime time { get; private set; }
        public double sum { get; private set; }
        
        #endregion

        public Payment( double tempSum) 
        {
            time = DateTime.Now;
            sum = tempSum;            
        }

    }
}
