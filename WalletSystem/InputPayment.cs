using EnumLibrary;

namespace WalletSystem
{
    public class InputPayment:Payment
    {
        public InputPurpose purpose { get; private set; }
        public InputPayment( decimal tempSum, InputPurpose pur) : base( tempSum) { purpose = pur; }
    }
}
