using DeepTransaction;
using Transaction.Test.Exceptions;

namespace Transaction.Test.DataProviders
{
  
    public class FirstTransaction : BaseTransaction
    {
        public FirstTransaction() : base("My Transaction")
        {
            this.AddStep<Step3>().AddStep<Step4>();
        }
    }

    public class DummyStep : ITransactionStep
    {
        public void Before(dynamic input)
        {
            
        }

        public TransactionContext Execute(dynamic input)
        {
            return input;
        }
    }

    public class ThrowExceptionStep : ITransactionStep
    {
        public void Before(dynamic input)
        {
            
        }

        public TransactionContext Execute(dynamic input)
        {
            throw new CustomException("Some nasty exception");
        }
    }

    public class Step3 : ITransactionStep
    {
        public void Before(dynamic input)
        {
            
        }

        public TransactionContext Execute(dynamic input)
        {
            if (input.Step3 == "Pasul 3")
            {
                input.Result = new Result() { Step3 = "Pasul 3 rezolvat" };
            }

            return input;
        }
    }

    public class Step4 : ITransactionStep
    {
        public void Before(dynamic input)
        {
            
        }

        public TransactionContext Execute(dynamic input)
        {
            if (input.Step4 == "Pasul 4")
            {
                input.Result.Step4 = "Pasul 4 rezolvat";
            }

            return input;
        }
    }

    public class Step5 : ITransactionStep
    {
        public void Before(dynamic input)
        {
            
        }

        public TransactionContext Execute(dynamic input)
        {
            if (input.Step5 == "Pasul 5")
            {
                input.Result.Step5 = "Pasul 5 rezolvat";
            }

            return input;
        }
    }

    public class Result
    {
        public string Step3 { get; set; }
        public string Step4 { get; set; }
        public string Step5 { get; set; }
    }
}