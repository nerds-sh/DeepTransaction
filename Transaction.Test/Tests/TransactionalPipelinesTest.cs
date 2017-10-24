using DeepTransaction;
using DeepTransaction.DI;
using Transaction.Test.DataProviders;
using Transaction.Test.Implementations;
using Xunit;

namespace Transaction.Test.Tests
{
    public class TransactionalPipelinesTest : BaseTest
    {
        public TransactionalPipelinesTest()
        {
            DeepBootstrapper.MapContext(new TransactionScopeImpl());
        }

        [Fact]
        public void it_will_instantiate_ContextualTransaction()
        {
             TransactionWorker.Define("My transactional transaction", new MyContext())
                .AddStep<TransactionStep>()
                .Process(new TransactionContext());
        }
    }
}