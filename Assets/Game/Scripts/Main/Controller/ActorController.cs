using Main.UseCase.Actor.Create;
using Main.UseCase.Actors.Edit;
using Zenject;

namespace Main.Controller
{
    public class ActorController
    {
        [Inject] private CreateActorUseCase _createActorUseCase;
        [Inject] private ChangeDirectionUseCase _changeDirectionUseCase;

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
    }
}