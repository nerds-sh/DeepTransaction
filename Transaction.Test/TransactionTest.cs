using DeepTransaction;
using DeepTransaction.DI;
using Xunit;

namespace Transaction.Test
{
    public class TransactionTest
    {
        public TransactionTest()
        {
            DeepBootstrapper.MapResolver(new DependecyResolver());
        }

        [Fact]
        public void it_can_chain_multiple_transaction_steps()
        {
            dynamic tranContext = new TransactionContext();
            tranContext.Name = "Alex";
            tranContext.fhdjfjhdshgjsd = "sfjksdfsdfhks";
            var response = TransactionWorker.Define("Shoud do the do")
                .AddStep<Step1>()
                .AddStep<Step2>()
                .Process(tranContext);

            Assert.Equal(response.Person.FirstName, tranContext.Name);
            Assert.Equal(response.Person.LastName, tranContext.Name);
        }
    }

    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class Step1 : ITransactionStep
    {
        public void Before(dynamic input)
        {
            
        }

        public TransactionContext Execute(dynamic input)
        {
            input.Person = new Person()
            {
                FirstName = input.Name
            };
            return input;
        }
    }

    public class Step2 : ITransactionStep
    {
        public void Before(dynamic input)
        {
            
        }

        public TransactionContext Execute(dynamic input)
        {
            input.Person.LastName = input.Name;

            return input;
        }
    }
}