namespace DeepTransaction
{
    public abstract class BaseTransaction : ITransactionStep
    {
        protected TransactionWorker Tran { get; set; }

        protected BaseTransaction(string name)
        {
            this.Tran = TransactionWorker.Define(name);
        }

        public TransactionContext Process(dynamic input)
        {
            return this.Tran.Process(input);
        }

        public BaseTransaction AddStep<TStep>() where TStep : ITransactionStep
        {
            this.Tran.AddStep<TStep>();

            return this;
        }

        public void Before(dynamic input)
        {
            // do nothing
        }

        public virtual TransactionContext Execute(dynamic input)
        {
            return this.Process(input);
        }
    }
}