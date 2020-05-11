using EnumLibrary;

namespace WalletSystem
{
    public class InputPayment:Payment
    {
        public InputPurpose purpose { get; private set; }
        public InputPayment( double tempSum, InputPurpose pur) : base( tempSum) { purpose = pur; }
    }
}
