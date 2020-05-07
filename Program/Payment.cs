using System;
using System.Collections.Generic;
using System.Text;

namespace Program
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
