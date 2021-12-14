using DDDCore;
using DDDCore.Usecase;
using Entity.Model;
using Main.UseCase.Repository;

namespace Main.UseCase.Actor.Create
{
    public class CreateActorInput : DDDCore.Usecase.Input
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
            var actorDataId     = input.ActorDataId;
            var actorDomainData = _dataRepository.GetActorDomainData(actorDataId);
            var health          = actorDomainData.Health;
            
            //var actor = new Main.Actor.Actor (input.ActorId, input.ActorDataId);
            var actor =  ActorBuilder.NewInstance()
                                     .SetActorId (input.ActorId)
                                     .SetActorDataId (input.ActorDataId)
                                     .SetHealth(health)
                                     .Build();
            repository.Save (actor);
            domainEventBus.PostAll (actor);
        }
    }
}