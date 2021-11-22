using DDDCore;
using Entity.Events;
using Main.Input;
using Main.presenter;
using Main.ViewComponent;

namespace Main.EventHandler.View
{
    public class ViewEventHandler: DDDCore.EventHandler
    {

        public ViewEventHandler(IDomainEventBus domainEventBus,
                                     ActorPresenter actorPresenter)
            : base(domainEventBus) {

            var signalBus = domainEventBus.SignalBus;
            //domain Event
            Register<ActorCreated>(created => {
                                       actorPresenter.OnActorCreated(created.ActorId,
                                                                     created.ActorDataId,
                                                                     created.Direction);
                                   });
            Register <DirectionChanged>(changed => {
                                            actorPresenter.OnDirectionChanged(changed.ActorId,
                                                                              changed.Direction);  
                                        });
            
            //some view event
            signalBus.Subscribe<Input_Horizontal> (actorPresenter.OnHorizontalChanged);
            signalBus.Subscribe<ButtonDownJump> (actorPresenter.OnButtonDownJump);
            signalBus.Subscribe<ButtonDownAttack> (actorPresenter.OnButtonDownAttack);
            signalBus.Subscribe<AnimEvent> (actorPresenter.OnAnimationTriggered);
        }
        
        
    }
}