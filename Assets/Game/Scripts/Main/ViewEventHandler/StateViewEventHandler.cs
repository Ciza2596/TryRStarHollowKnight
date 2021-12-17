using DDDCore;
using Main.Entity.State.Events;
using Main.presenter;

namespace Main.EventHandler.View
{
    public class StateViewEventHandler : DDDCore.EventHandler
    {
        
        public StateViewEventHandler(IDomainEventBus domainEventBus,
                                     StatePresenter  statePresenter)
            : base(domainEventBus) {

            handlerType = HandlerType.View;
            Register<StateCreated>(created => statePresenter.OnStateCreated(created.ActorId,
                                                                            created.StateName,
                                                                            created.Amount));
            
             Register<AmountModified>(created => statePresenter.OnAmountModified(created.ActorId,
                                                                            created.StateName,
                                                                            created.Amount));


        }
    }
}
