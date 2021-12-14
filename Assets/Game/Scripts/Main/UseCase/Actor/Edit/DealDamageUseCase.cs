using DDDCore;
using DDDCore.Usecase;
using Main.UseCase.Repository;

namespace Main.UseCase.Actor.Edit
{
    public class DealDamageInput : Input
    {
        public string ActorId;
        public int    Damage { get; set; }
    }
    
    public class DealDamageUseCase : UseCase<DealDamageInput, ActorRepository>
    {
        public DealDamageUseCase(IDomainEventBus domainEventBus,
                                 ActorRepository actorRepository)
            : base (domainEventBus, actorRepository) {
        }

        public override void Execute(DealDamageInput input) {
            var actor = repository.FindById(input.ActorId);
            actor.DealDamage(input.Damage);
            domainEventBus.PostAll(actor);

        }
    }
}
