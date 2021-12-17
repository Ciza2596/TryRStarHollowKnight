
using DDDCore;
using DDDCore.Usecase;
using Main.Entity.State;
using Main.UseCase.Repository;

namespace Main.UseCase.State
{
    public class CreateStateInput
    {
        public string StateId;
        public string StateName;
        public int    Amount;
        
        public string ActorId;
    }
    
    public class CreateStateUseCase : UseCase<CreateStateInput, IRepository<IState>>
    {
        private IStateRepository _stateRepository;

        public CreateStateUseCase(IDomainEventBus domainEventBus, IStateRepository stateRepository)
            : base(domainEventBus, stateRepository) {

            _stateRepository = stateRepository;
        }

        public override void Execute(CreateStateInput input) {
            
            var stateId   = input.StateId;
            var stateName = input.StateName;
            var amount    = input.Amount;
            var actorId   = input.ActorId;

            var state =  StateBuilder.NewInstance()
                                     .SetStateId (stateId)
                                     .SetStateName (stateName)
                                     .SetAmount(amount)
                                     .SetActorId(actorId)
                                     .Build();
            
            repository.Save (state );
            domainEventBus.PostAll (state );
        }
    }
}
