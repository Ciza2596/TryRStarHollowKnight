using DDDCore;
using DDDCore.Usecase;
using Main.UseCase.Respository;

namespace Main.UseCase.Actors.Edit
{
    public class ChangeDirectionInput : DDDCore.Usecase.Input
    {
    #region Public Variables

        public string ActorId;

        public int Direction;

    #endregion
    }

    public class ChangeDirectionUseCase: UseCase<ChangeDirectionInput, ActorRepository>
    {
        public ChangeDirectionUseCase(IDomainEventBus domainEventBus,
                                      ActorRepository actorRepository)
            : base (domainEventBus, actorRepository) {
        }

        public override void Execute(ChangeDirectionInput input) {

            var actor = repository.FindById (input.ActorId);
            actor.ChangeDirection (input.Direction);
            domainEventBus.PostAll (actor);

        }
    }
}