using DeepTransaction;
using DeepTransaction.DI;
using DeepTransaction.Listeners;
using Moq;
using Transaction.Test.DataProviders;
using Transaction.Test.Exceptions;
using Xunit;

namespace Transaction.Test.Tests
{
    public class GlobalListenerTest : BaseTest
    {
        private readonly Mock<IListener> _listener;
        
        public GlobalListenerTest()
        {
            _listener = new Mock<IListener>();
            DeepBootstrapper.MapListener(_listener.Object);
        }

        [Fact]
        public void it_will_call_before_global_listener_with_parameters()
        {
            TransactionWorker.Define("Some transaction").AddStep<DummyStep>().Process(new TransactionContext());

            _listener.Verify(z => z.Before(It.Is<ListenerModel>(t => t.TransactionName == "Some transaction")));
            _listener.Verify(z => z.Before(It.Is<ListenerModel>(t => t.StepName == typeof(DummyStep).ToString())));
        }

        [Fact]
        public void it_will_call_after_global_listener_with_parameters()
        {
            TransactionWorker.Define("Some transaction").AddStep<DummyStep>().Process(new TransactionContext());

            _listener.Verify(z => z.After(It.Is<ListenerModel>(t => t.TransactionName == "Some transaction")));
            _listener.Verify(z => z.After(It.Is<ListenerModel>(t => t.StepName == typeof(DummyStep).ToString())));
        }

        [Fact]
        public void in_case_of_exception_will_call_global_listener_with_params()
        {
            var transaction = TransactionWorker.Define("Some transaction").AddStep<ThrowExceptionStep>();

            var ex = Assert.Throws<CustomException>(() => transaction.Process(new TransactionContext()));

            _listener.Verify(z => z.OnError(It.Is<ListenerModel>(t => t.TransactionName == "Some transaction"), It.IsAny<CustomException>()));
            _listener.Verify(z => z.OnError(It.Is<ListenerModel>(t => t.StepName == typeof(ThrowExceptionStep).ToString()), It.IsAny<CustomException>()));

            Assert.Equal("Some nasty exception", ex.Message);
        }
    }
}