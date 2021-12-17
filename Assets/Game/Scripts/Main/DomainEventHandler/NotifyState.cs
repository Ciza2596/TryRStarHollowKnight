
using DDDCore;
using Main.Controller;
using Main.Entity.Actor.Events;
using Main.UseCase.Repository;
using Zenject;

namespace Main.DomainEventHandler
{
    public class NotifyState : DDDCore.EventHandler
    {

        [Inject] private StateController _stateController;
        [Inject] private IDataRepository _dataRepository;
        
        public NotifyState(IDomainEventBus domainEventBus)
            : base(domainEventBus) {

            Register<ActorCreated>(OnActorCreated);

        }                                          

        private void OnActorCreated(ActorCreated created) {
            var actorId     = created.ActorId;
            var actorDataId = created.ActorDataId;
            var actorData   = _dataRepository.GetActorDomainData(actorDataId);
            
            foreach(var stateData in actorData.StateDatas){
                var stateName = stateData.StateName;
                var amount    = stateData.Amount;
                _stateController.CreateState(actorId, stateName, amount);

            }
        }
    }
}