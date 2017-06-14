# DeepTransaction
Write better code with the help of steps which are wrapped in a transaction.

### Simple Example of usage:

   ```
    dynamic context = new TransactionContext();
               context.Name = "Jhonny Cash";
               context.Age = 25;
   
   var response = TransactionWorker.Define("Insert and return a person")
                   .AddStep<AddPersonStep>()
                   .AddStep<GetPersonStep>()
                   .Process(context);
   ```
A transaction has a collection of steps. Each step can resolve a piece of your code.
   
The AddPersonStep step can look like this:
  ```
    public class AddPersonStep : ITransactionStep
        {
            private string _name;
            private int _age;
    
    
            public void Before(dynamic input)
            {
                _name = input.Name;
                _age = input.Age;
            }
    
            public TransactionContext Execute(dynamic input)
            {
                using (var repo = new ExtraContext())
                {
                    var person = new Person() {Age = _age, Name = _name};
                    repo.Persons.Add(person);
    
                    repo.SaveChanges();
                }
    
                return input;
            }
        }
  ```

  A step must implement `ITransactionStep` and will override two methods: (`Before` and `Execute`).
  As you can see the input and output is dynamic because we must take advantage of this feature and not write boiler plate code.
  
  The workflow for a step is: 
     
     
   1. Call `Before` method - here you can extract the params for the context and validate the input parameters
   2. Call `Execute` method - which will execute the business logic
  
   ### Installation:
   1. On your solution navigate to package manager console and execute:  `Install-Package DeepTransaction`
   2. We need to hook with your dependency injection module:
       - Create a new class which will implement `IDependencyResolver`: 
                
                    public class RegisterIoc : IDependencyResolver
                    {
                        public TOut Get<TOut>()
                        {
                            return Container.Instance.Resolve<TOut>();
                        }
                    }
       - We need to implement Get method with your dependency injection module. In this case the DI module is Unity
       - As a next step we need to register the newly created class to the framework (This would be a good idea to put it into the bootstrap class from your application)
                    
                   SetupResolver.Setup(new RegisterIoc());
                   
   ### Advanced scenarios:
   
   In case you want reuse your transactions in other transactions it can be done very easy: 
      
      public class FirstTransaction : BaseTransaction
          {
              public FirstTransaction() : base("My Transaction")
              {
                  this.AddStep<Step3>().AddStep<Step4>();
              }
          }
          
      
   We must inherit BaseTransaction and in the constructor we can add the steps needed for the transaction.
   
   The usage would look like this (like a normal step):
   
     var response = TransactionWorker.Define("Do a lot of work")
                     .AddStep<FirstTransaction>()
                     .AddStep<SecondTransaction>()
                     .AddStep<Step5>()
                     .Process(tranContext);
                        
   As you can see we can mix transactions with steps.
   
   ### Using your existing code
   
   Because we are using your Dependency Injection module we can use inject on steps different modules to work with:
       
       public class ArchiveStep : ITransactionStep
           {
               private IZipService _zipService;
               private Stream _stream;
       
               public ArchiveStep(IZipService zipService)
               {
                   this._zipService = zipService;
               }
       
               public void Before(dynamic input)
               {
                   _stream = input.StreamFile;
               }
       
               public TransactionContext Execute(dynamic input)
               {
                   input.ArchivedStream =  _zipService.Archive(_stream);
                   
                   return input;
               }
           }
   
   ### Error recovery and Transactional Behaviour:
   If an error occurs then all the database modifications will rollback.  