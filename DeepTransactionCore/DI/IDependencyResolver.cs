namespace DeepTransactionCore.DI 
{
    public interface IDependencyResolver
    {
        TOut Resolve<TOut>();
    }
}