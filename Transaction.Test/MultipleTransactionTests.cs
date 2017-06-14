using DeepTransaction;
using DeepTransaction.DI;
using Xunit;

namespace Transaction.Test
{
    public class MultipleTransactionTests
    {
        public MultipleTransactionTests()
        {
            SetupResolver.Setup(new DependecyResolver());
        }

        [Fact]
        public void it_can_run_multiple_transactions()
        {
            dynamic tranContext = new TransactionContext();
            tranContext.Name = "Alex";
            tranContext.Step3 = "Pasul 3";
            tranContext.Step4 = "Pasul 4";
            tranContext.Step5 = "Pasul 5";

            var response = TransactionWorker.Define("Hala").AddStep<FirstTransaction>().AddStep<SecondTransaction>().AddStep<Step5>()
                .Process(tranContext);

            Assert.Equal(response.Result.Step3, "Pasul 3 rezolvat");
            Assert.Equal(response.Result.Step4, "Pasul 4 rezolvat");
            Assert.Equal(response.Result.Step5, "Pasul 5 rezolvat");
            Assert.Equal(response.Person.FirstName, "Alex");
            Assert.Equal(response.Person.LastName, "Alex");
        }
    }
}