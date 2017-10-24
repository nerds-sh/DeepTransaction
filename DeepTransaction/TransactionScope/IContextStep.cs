using System;

namespace DeepTransaction.TransactionScope
{
    public interface IContextStep<T> where T : IDisposable
    {
        T Context { get; set; }
    }
}