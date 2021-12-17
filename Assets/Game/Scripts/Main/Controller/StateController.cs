using Main.UseCase.State;
using Main.UseCase.State.Edit;
using UnityEngine;
using Zenject;

namespace Main.Controller
{
    public class StateController
    {
        [Inject] private CreateStateUseCase  _createStateUseCase;
        [Inject] private ModifyAmountUseCase _modifyAmountUseCase;

        public void CreateState(string actorId,
                                string stateName,
                                int    amount) {
            var input = new CreateStateInput();
            input.ActorId   = actorId;
            input.StateName = stateName;
            input.Amount    = amount;
            _createStateUseCase.Execute(input);
        }

        public void ModifyStateAmount(string actorId, string stateName, int amount) {
            var input = new ModifyAmountInput();
            input.ActorId   = actorId;
            input.StateName = stateName;
            input.Amount    = amount;
            _modifyAmountUseCase.Execute(input);
        }
    }
}
