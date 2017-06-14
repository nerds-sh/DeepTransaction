namespace Trsanction.Core
{
    public interface ITransactionStep
    {
        TransactionContext Execute(dynamic input);
    }
}