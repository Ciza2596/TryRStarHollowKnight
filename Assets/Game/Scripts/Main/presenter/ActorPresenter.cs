using System.Collections.Generic;
using DDDCore.Adapter.Presenter.Unity;
using Main.GameDataStructure;
using Main.Controller;
using Main.Input.Event;
using Main.Input.Events;
using Main.ViewComponent.Events;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Main.presenter
{
    public class ActorPresenter : UnityPresenter
    {
        [Inject] private ActorController _actorController;
        
        [SerializeField] private Button _button_CreateActor_Player;
        [SerializeField] private Button _button_CreateActor_DealDamage;
        [SerializeField] private Button _button_MakeActorDie;

        [Inject] private ActorMapper     _actorMapper;
        [Inject] private List<ActorData> _actorDatas;
        
        private string     _cacheActorId;
        private int        _direction;


        private void Start() {
            ButtonBinding (_button_CreateActor_Player,   () => _actorController.CreateActor (_actorDatas[1].ActorDataId));
            ButtonBinding (_button_CreateActor_DealDamage,   () => _actorController.DealDamage(_cacheActorId, 10));
            ButtonBinding(_button_MakeActorDie, () => _actorController.MakeActorDie(_cacheActorId));
        }

        
        public void OnActorCreated(string actorId, string actorDataId, int direction) {
            _cacheActorId = actorId;
            _actorMapper.CreateActorViewData (actorId, actorDataId, direction);
        }

        public void OnDirectionChanged(string actorId, int direction) {

            if (!string.IsNullOrEmpty (_cacheActorId)) {
                var actorComponent = _actorMapper.GetActorComponent (actorId);
                actorComponent.SetDirection (direction);
            }
        }

        public void OnHorizontalChanged(InputHorizontal inputHorizontal) {

            if (!string.IsNullOrEmpty (_cacheActorId)) {
                var horizontalValue = inputHorizontal.HorizontalValue;
                var isMove          = horizontalValue != 0;
                var actorComponent  = _actorMapper.GetActorComponent (_cacheActorId);
                actorComponent.SetIsMoving (isMove);
                                                                  
                if (isMove) {                                                       
                    int direction = horizontalValue == 1? 1:0; 
                    _actorController.ChangeDirection (_cacheActorId, direction); 
                }
            }
        }

        public void OnButtonDownJump(ButtonDownJump buttonDownJump) {
            if (!string.IsNullOrEmpty (_cacheActorId)) {
                var actorComponent = _actorMapper.GetActorComponent (_cacheActorId);
                actorComponent.Jump(); 
            }
        }

        public void OnButtonDownAttack(ButtonDownAttack buttonDownAttack) {
            if (!string.IsNullOrEmpty (_cacheActorId)) {
                var actorComponent = _actorMapper.GetActorComponent (_cacheActorId);
                actorComponent.Attack();
            }
        }

        public void OnHitBoxTriggered(HitBoxTriggered hitBoxTriggered) {

            var triggerActorComponent = hitBoxTriggered.TriggerActorComponent;
            triggerActorComponent.UnityComponent.PlayAnimation("Hit");
            
        }

        public void OnDamageDealt(string actorId, int currentHealth) {
            if (!string.IsNullOrEmpty (_cacheActorId)) {
                var actorComponent = _actorMapper.GetActorComponent (actorId);
                actorComponent.SetHealthText(currentHealth);
            }
        }

        public void OnActorDead(string actorId) {
            var actorComponent = _actorMapper.GetActorComponent(actorId);
            actorComponent.MakeDie();
        }
    }
}