using System;

namespace DeepTransactionCore.Listeners
{
    public interface IListener
    {
        void Before(ListenerModel listenerModel);

        void After(ListenerModel listenerModel);

        void OnError(ListenerModel listenerModel, Exception exception);
    }
}