using DDDCore;
using DDDCore.Usecase;
using Main.UseCase.Repository;
using UnityEngine;
using Input = DDDCore.Usecase.Input;

namespace Main.UseCase.Actor.Edit
{
    public class MakeActorDieInput : Input
    {
        public string ActorId;
    }


    public class MakeActorDieUseCase : UseCase<MakeActorDieInput,ActorRepository>
    {
        public MakeActorDieUseCase(IDomainEventBus domainEventBus,
                                   ActorRepository actorRepository)
            : base (domainEventBus, actorRepository) {
        }

        public override void Execute(MakeActorDieInput input) {
            var actorId = input.ActorId;
            var actor   = repository.FindById(actorId);
            actor.MakeDie(); 
            domainEventBus.PostAll(actor);
        }
    }
}