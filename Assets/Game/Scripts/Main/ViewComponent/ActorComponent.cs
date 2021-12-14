using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using UnityEngine;
using Utilities.Contract;
using Zenject;
using Main.ViewComponent.Events;

namespace Main.ViewComponent
{
    public class ActorComponent : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;

        public                   IUnityComponent     UnityComponent;
        [ShowInInspector] public ICharacterCondition CharacterCondition;

        [Required]       public  Text      Text_IdAndDataId;
        [Required]       public  Text      Text_Health;
        [Required]       public  Transform RendererTransform;
        [SerializeField] private Animator  _animator;

        public            int   CurrentDirectionValue;
        [Required] public float JumpForce;


        private                  int   _moveSpeed = 5;
        [SerializeField] private float _radius    = 0.1f;

        [SerializeField] [Required] private BoxCollider2D boxCollider_HitBox;

        private Rigidbody2D _rigidbody2D;

    #region Public Methods

        public void SetIsMoving(bool isMoving) {
            if(CharacterCondition.IsDead)
                return;

            CharacterCondition.IsMoving = isMoving;
            string animName = isMoving ? "Run" : "Idle";

            if(!CharacterCondition.IsAttacking)
                UnityComponent.PlayAnimation(animName);
        }

        public void SetIdAndDataId(string idAndDataId) {
            Text_IdAndDataId.text = idAndDataId;
        }

        public void SetHealthText(int health) {
            string displayText = $"Health: {health}";
            Text_Health.text = displayText;
        }

        public void SetDirection(int directionValue) {
            CurrentDirectionValue = directionValue;

            var x                     = 0;
            if(directionValue == 0) x = 1;
            if(directionValue == 1) x = -1;

            if(!CharacterCondition.IsAttacking)
                RendererTransform.transform.localScale =
                    new Vector3(x, RendererTransform.transform.localScale.y, RendererTransform.transform.localScale.z);
        }

        public void Jump() {
            CharacterCondition.IsOnGround = false;
            UnityComponent.PlayAnimation("Jump");
            UnityComponent.AddForce(Vector2.up * JumpForce);
        }

        public void MakeDie() {
            CharacterCondition.IsDead = true;
            UnityComponent.PlayAnimation("Die");
            if(Text_IdAndDataId != null)
                Text_IdAndDataId.enabled = false;
            if(Text_Health != null)
                Text_Health.enabled = false;
        }

        public void Attack() {
            if(CharacterCondition.IsDead)
                return;
            CharacterCondition.IsAttacking = true;
            UnityComponent.PlayAnimation("Attack", OnAttackEnd);
        }

        public void OnAttackEnd() {
            CharacterCondition.IsAttacking = false;
        }

        public void MoveCharacter() {
            var movement = GetMovement();
            UnityComponent.MoveCharacter(movement);
            UnityComponent.PlayAnimation("Run");
        }

        public Vector3 GetMovement() {
            Vector3 directionValue = (CurrentDirectionValue == 0) ? Vector3.left : Vector3.right;
            return directionValue * _moveSpeed * Time.deltaTime;
        }

    #endregion

    #region Unity Callback Functions

        public void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            UnityComponent = new UnityComponent(gameObject.transform,
                                                _animator,
                                                _rigidbody2D,
                                                _radius);

            CharacterCondition = new CharacterCondition();

            //listen hit box trigger event
            boxCollider_HitBox.OnTriggerEnter2DAsObservable()
                              .Subscribe(collider2D => OnHitBoxTriggered(collider2D))
                              .AddTo(gameObject);
        }

        public void Update() {
            Contract.RequireNotNull(CharacterCondition, "characterCondition");

            if(CharacterCondition.IsOnGround != UnityComponent.IsGrounding())
                CharacterCondition.IsOnGround = UnityComponent.IsGrounding();

            if(CharacterCondition.CanMove())
                MoveCharacter();
        }

    #endregion

    #region Private Methods

        private void OnHitBoxTriggered(Collider2D collider) {
            var colliderGameObject = collider.gameObject;
            if(colliderGameObject != gameObject){
                var triggerActorComponent = colliderGameObject.GetComponent<ActorComponent>();

                _signalBus.Fire(new HitBoxTriggered(triggerActorComponent));
                //Debug.Log($"Collider: {collider.name}");
            }
        }

        private void OnDrawGizmos() {
            if(UnityComponent != null){
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(UnityComponent.GetGroundCheckPosition(), _radius);
            }
        }

    #endregion
    }
}
