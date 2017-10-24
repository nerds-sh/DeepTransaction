namespace DeepTransaction
{
    public abstract class BaseTransaction : ITransactionStep
    {
        protected TransactionEngine _tran;

        /// <summary>
        /// Defines the transaction name
        /// </summary>
        /// <param name="name">Transaction Name</param>
        protected BaseTransaction(string name)
        {
            this._tran = new TransactionEngine(name);
        }

        /// <summary>
        /// This method is for passing the context and execute the current transaction
        /// </summary>
        /// <param name="input">Input Context which has to have the type Transaction Context</param>
        /// <returns></returns>
        public TransactionContext Process(dynamic input)
        {
            return _tran.Process(input);
        }

        /// <summary>
        /// This method is using for chaning steps for the current transaction
        /// </summary>
        /// <typeparam name="TStep">The type of the step</typeparam>
        /// <returns>A base transaction used for chainging</returns>
        public BaseTransaction AddStep<TStep>() where TStep : ITransactionStep
        {
            this._tran.AddStep<TStep>();

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