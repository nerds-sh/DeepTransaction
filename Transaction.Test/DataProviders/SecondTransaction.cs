using DeepTransaction;
using Transaction.Test.Tests;

namespace Transaction.Test.DataProviders
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