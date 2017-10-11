using DeepTransaction.DI;

namespace Transaction.Test.Tests
{
    public class BaseTest
    {
        public BaseTest()
        {
            DeepBootstrapper.MapResolver(new DependecyResolver());
        }
    }
}