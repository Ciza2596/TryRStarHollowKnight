using DDDCore;
using DDDCore.Usecase;
using Main.Entity.Actor;
using Main.UseCase.Repository;

namespace Main.UseCase.Actor.Create
{
    public class CreateActorInput : Input
    {
        public string ActorId;
        public string ActorDataId;
    }
    
    public class CreateActorUseCase : UseCase<CreateActorInput, ActorRepository>
    {

        private readonly IDataRepository _dataRepository;
        
        public CreateActorUseCase(IDomainEventBus domainEventBus,
                                  ActorRepository actorRepository, 
                                  IDataRepository dataRepository)
            : base (domainEventBus, actorRepository) {
            _dataRepository = dataRepository;
        }

        public override void Execute(CreateActorInput input) {
            var actorId     = input.ActorId;
            var actorDataId = input.ActorDataId;

            var actor =  ActorBuilder.NewInstance()
                                     .SetActorId (actorId)
                                     .SetActorDataId (actorDataId)
                                     .Build();
            
            repository.Save (actor);
            domainEventBus.PostAll (actor);
        }
    }
}