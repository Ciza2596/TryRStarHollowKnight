using DDDCore;
using Main.Entity.Actor.Events;
using Main.Input.Event;
using Main.Input.Events;
using Main.presenter;
using Main.ViewComponent.Events;

namespace Main.EventHandler.View
{
    public class ActorViewEventHandler : DDDCore.EventHandler
    {
        public ActorViewEventHandler(IDomainEventBus domainEventBus,
                                ActorPresenter  actorPresenter)
            : base(domainEventBus) {
            var signalBus = domainEventBus.SignalBus;
            //domain Event
            Register<ActorCreated>(created => {
                                       actorPresenter.OnActorCreated(created.ActorId,
                                                                     created.ActorDataId,
                                                                     created.Direction);
                                   });
            Register<DirectionChanged>(changed => {
                                           actorPresenter.OnDirectionChanged(changed.ActorId,
                                                                             changed.Direction);
                                       });
            Register<DamageDealt>(damageDealt => {
                                      actorPresenter.OnDamageDealt(damageDealt.ActorId,
                                                                   damageDealt.CurrentHealth);
                                  });

            Register<ActorDead>(dead => { actorPresenter.OnActorDead(dead.ActorId); });

            //some view event
            signalBus.Subscribe<InputHorizontal>(actorPresenter.OnHorizontalChanged);
            signalBus.Subscribe<ButtonDownJump>(actorPresenter.OnButtonDownJump);
            signalBus.Subscribe<ButtonDownAttack>(actorPresenter.OnButtonDownAttack);
            signalBus.Subscribe<HitBoxTriggered>(actorPresenter.OnHitBoxTriggered);
        }
    }
}
