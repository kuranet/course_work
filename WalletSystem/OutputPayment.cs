using EnumLibrary;

namespace WalletSystem
{
    public class OutputPayment:Payment
    {
        public OutputPurpose purpose { get; private set; }
        public OutputPayment( decimal tempSum, OutputPurpose pur) : base( tempSum) { purpose = pur; }
    }
}
