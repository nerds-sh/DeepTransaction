using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;
using DeepTransaction.DI;
using DeepTransaction.Listeners;

namespace DeepTransaction
{
    public class TransactionWorker
    {
        private readonly string _name;
        private readonly Queue<ITransactionStep> _steps;
        private readonly IDependencyResolver _dependencyResolver;
        private static Queue<int> _transactionDeepness;
        private TransactionScope _tranScope;
        private IListener _listener;

        public static TransactionWorker Define(string name)
        {
            _transactionDeepness = new Queue<int>();
            return new TransactionWorker(name);
        }

        private TransactionWorker(string name)
        {
            _steps = new Queue<ITransactionStep>();
            _dependencyResolver = DeepBootstrapper.Get();
            this._name = name;
        }

        /// <summary>
        /// This method is using for chaning steps for the current transaction
        /// </summary>
        /// <typeparam name="TStep">The type of the step</typeparam>
        /// <returns>A base transaction used for chainging</returns>
        public TransactionWorker AddStep<TStep>() where TStep : ITransactionStep
        {
            var step = _dependencyResolver.Resolve<TStep>();
            this._steps.Enqueue(step);

            return this;
        }

        /// <summary>
        /// This method is for passing the context and execute the current transaction
        /// </summary>
        /// <param name="input">Input Context which has to have the type Transaction Context</param>
        /// <returns></returns>
        public TransactionContext Process(TransactionContext input)
        {
            _transactionDeepness.Enqueue(1);
            BeginTransaction();
            var stepName = string.Empty;
            dynamic previousOutput = null;
            while (_steps.Count > 0)
            {
                try
                {
                    var step = _steps.Dequeue();
                    stepName = step.GetType().FullName;
                    Trace.WriteLine(stepName);

                    _listener?.Before(new ListenerModel() { Context = input, StepName = stepName, TransactionName = _name });

                    step.Before(input);
                    previousOutput = step.Execute(input);

                    _listener?.After(new ListenerModel() { Context = input, StepName = stepName, TransactionName = _name });
                }
                catch (System.Exception e)
                {
                    _listener?.OnError(new ListenerModel() { Context = input, StepName = stepName, TransactionName = _name }, e);
                    throw;
                }
            }

            CommitTransaction();
            _transactionDeepness.Dequeue();

            return previousOutput;
        }

        private void CommitTransaction()
        {
            if (_transactionDeepness.Count > 1) return;
            try
            {
                _tranScope.Complete();
                _tranScope.Dispose();
                _tranScope = null;
            }
            catch (System.Exception e)
            {

                throw;
            }
        }

        private void BeginTransaction()
        {
            if (_transactionDeepness.Count <= 1)
            {
                _tranScope = new TransactionScope();
            }
        }

        public void WithListener(IListener listener)
        {
            this._listener = listener;
        }

    }
}
