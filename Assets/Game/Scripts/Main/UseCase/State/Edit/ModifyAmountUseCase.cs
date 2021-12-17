using DDDCore;
using DDDCore.Usecase;
using Main.UseCase.Repository;
using UnityEngine;

namespace Main.UseCase.State.Edit
{
    public class ModifyAmountInput
    {
        public string ActorId;
        public int    Amount;
        public string StateName;
    }

    public class ModifyAmountUseCase : UseCase<ModifyAmountInput, IStateRepository>
    {
        public ModifyAmountUseCase(IDomainEventBus domainEventBus, IStateRepository repository)
            : base(domainEventBus, repository) { }

        public override void Execute(ModifyAmountInput input) {
            var actorId   = input.ActorId;
            var stateName = input.StateName;
            var amount    = input.Amount;

            var state = repository.FindState(actorId, stateName);

            if(state == null){
                return;
            }

            var stateAmount      = state.Amount;
            var calculatedAmount = stateAmount + amount;
            if(calculatedAmount < 0) 
                calculatedAmount = 0;
            
            state.SetAmount(calculatedAmount);
            
            domainEventBus.PostAll(state);
        }
    }
}
