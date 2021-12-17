using Main.UseCase.Actor.Create;
using Main.UseCase.Actor.Edit;
using Zenject;

namespace Main.Controller
{
    public class ActorController
    {
        [Inject] private CreateActorUseCase     _createActorUseCase;
        [Inject] private ChangeDirectionUseCase _changeDirectionUseCase;
        [Inject] private MakeActorDieUseCase    _makeActorDieUseCase;

        public void CreateActor(string actorDataId) {
            var input = new CreateActorInput();
            input.ActorDataId = actorDataId;
            _createActorUseCase.Execute (input);
        }

        public void ChangeDirection(string actorId, int direction) {
            var input = new ChangeDirectionInput();
            input.ActorId   = actorId;
            input.Direction = direction;
           _changeDirectionUseCase.Execute (input); 
        }
        
        public void MakeActorDie(string actorId) {
            var input = new MakeActorDieInput();
            input.ActorId = actorId;
            _makeActorDieUseCase.Execute (input); 
        }
    }
}