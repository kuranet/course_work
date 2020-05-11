using System;

namespace WalletSystem
{
    public abstract class Payment
    {
        #region Properties
        public DateTime time { get; private set; }
        public decimal sum { get; private set; }
        
        #endregion

        public Payment( decimal tempSum) 
        {
            time = DateTime.Now;
            sum = tempSum;            
        }

    }
}
