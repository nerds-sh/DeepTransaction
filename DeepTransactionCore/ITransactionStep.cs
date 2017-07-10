
namespace DeepTransactionCore
{
    public interface ITransactionStep
    {
        void Before(dynamic input);

        TransactionContext Execute(dynamic input);
    }
}