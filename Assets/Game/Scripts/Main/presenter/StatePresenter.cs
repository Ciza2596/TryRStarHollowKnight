using DDDCore.Adapter.Presenter.Unity;
using Main.Controller;
using UnityEngine.UI;
using UnityEngine;
using Zenject;


namespace Main.presenter
{
    public class StatePresenter : UnityPresenter
    {
        
        [SerializeField] private Button _button_CreateActor_DealDamage;

        [Inject] private StateController _stateController;
        [Inject] private ActorMapper     _actorMapper;

        private string cachActorId;
        
        // Start is called before the first frame update
        void Start() {
            ButtonBinding(_button_CreateActor_DealDamage, () => {
                                                              var stateName = "HP";
                                                              _stateController.ModifyStateAmount(cachActorId, stateName,-10);
                                                              
                                                          });
        }

        public void OnStateCreated(string actorId, string stateName, int amount ) {
            cachActorId = actorId;
            var actorComponent = _actorMapper.GetActorComponent(actorId);
            actorComponent.CreateState(stateName, amount);
        }

        public void OnAmountModified(string actorId, string stateName, int amount) {
            cachActorId = actorId;
            var actorComponent = _actorMapper.GetActorComponent(actorId);
            actorComponent.SetStateText(stateName, amount);
        }
    }
}
