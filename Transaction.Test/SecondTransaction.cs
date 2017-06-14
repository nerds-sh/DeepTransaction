using Trsanction.Core;

namespace Transaction.Test
{
    public class SecondTransaction : BaseTransaction
    {
        public SecondTransaction() : base("cealalata trazactie")
        {
            AddStep<Step1>();
            AddStep<Step2>();
        }
    }
}