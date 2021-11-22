using System.Collections.Generic;
using DDDCore.Adapter.Presenter.Unity;
//using Entity.Events;
using Main.ScriptableObject;
using Main.Controller;
using Main.Input;
using Main.ViewComponent;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Main.presenter
{
    public class ActorPresenter : UnityPresenter
    {
        [Inject] private ActorController _actorController;

        [SerializeField] private Button _button_CreateActor_A;
        [SerializeField] private Button _button_CreateActor_B;
        [SerializeField] private Button _button_CreateActor_C;
        [SerializeField] private Button _button_ChangeDirection;

        [Inject] private ActorMapper     _actorMapper;
        [Inject] private List<ActorData> _actorDatas;
        
        private string     _cacheActorId;
        private int        _direction;


        private void Start() {
            ButtonBinding (_button_CreateActor_A,   () => _actorController.CreateActor (_actorDatas[0].ActorDataId));
            ButtonBinding (_button_CreateActor_B,   () => _actorController.CreateActor (_actorDatas[1].ActorDataId));
            ButtonBinding (_button_CreateActor_C,   () => _actorController.CreateActor (_actorDatas[2].ActorDataId));
            ButtonBinding (_button_ChangeDirection, () => {

                _direction = _direction == 0 ? 1 : 0;
                _actorController.ChangeDirection (_cacheActorId, _direction);
            });
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

        public void OnHorizontalChanged(Input_Horizontal inputHorizontal) {

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

        public void OnAnimationTriggered(AnimEvent animEvent) {
            var actorComponent =  _actorMapper.GetActorComponent(_cacheActorId);
            var transform      = actorComponent.transform;
            var origin         = transform.position;
            var direction      = actorComponent.CurrentDirectionValue == 0 ? Vector2.left : Vector2.right;
            var raycastHit2Ds =  Physics2D.BoxCastAll (origin, new Vector2 (2, 1), 0, direction);
            if (raycastHit2Ds.Length > 0) {

                foreach (var raycastHit2D in raycastHit2Ds) {
                    if (raycastHit2D.transform.gameObject != actorComponent.gameObject) {
                        var hitActorComponent = raycastHit2D.transform.GetComponent<ActorComponent>();
                        hitActorComponent.UnityComponent.PlayAnimation ("Hit");
                    }

                }
            }
        }
    }
}