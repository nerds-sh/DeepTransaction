namespace DeepTransaction.Listeners
{
    public class ListenerModel
    {
        public string TransactionName { get; set; }

        public string StepName { get; set; }

        public dynamic Context { get; set; }
    }
}