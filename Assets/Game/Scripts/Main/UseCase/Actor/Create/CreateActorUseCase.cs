using DDDCore;
using DDDCore.Usecase;
using Entity.Model;
using Main.UseCase.Respository;

namespace Main.UseCase.Actor.Create
{
    public class CreateActorInput : DDDCore.Usecase.Input
    {
        public string ActorId;
        public string ActorDataId;
    }
    
    public class CreateActorUseCase : UseCase<CreateActorInput, ActorRepository>
    {
        public CreateActorUseCase(IDomainEventBus domainEventBus,
                                  ActorRepository actorRepository)
            : base (domainEventBus, actorRepository) {
        }

        public override void Execute(CreateActorInput input) {

            //var actor = new Main.Actor.Actor (input.ActorId, input.ActorDataId);
            var actor =  ActorBuilder.NewInstance()
                                     .SetActorId (input.ActorId)
                                     .SetActorDataId (input.ActorDataId)
                                     .Build();
            repository.Save (actor);
            domainEventBus.PostAll (actor);
        }
    }
}