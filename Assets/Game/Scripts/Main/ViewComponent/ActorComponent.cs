using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;
using Utilities.Contract;

namespace Main.ViewComponent
{
    public class ActorComponent : MonoBehaviour
    {
        public                   IUnityComponent     UnityComponent;
        [ShowInInspector] public ICharacterCondition CharacterCondition;

        [Required]
        public                   Text      Text_IdAndDataId;
        [Required]
        public                   Transform ActorTransform;
        [SerializeField] private Animator  _animator;

        public int CurrentDirectionValue;
        [Required]
        public float JumpForce; 


        private int   _moveSpeed = 5;
        [SerializeField]
        private float _radius     = 0.1f;

        private Rigidbody2D _rigidbody2D;

    #region Public Methods

        public void SetIsMoving(bool isMoving) {
            CharacterCondition.IsMoving = isMoving;
            string animName = isMoving ? "Run" : "Idle";

            if (!CharacterCondition.IsAttacking)
                UnityComponent.PlayAnimation (animName);
        }

        public void SetText(string displayText) {
            Text_IdAndDataId.text = displayText;
        }

        public void SetDirection(int directionValue) {
            CurrentDirectionValue = directionValue;

            var x                      = 0;
            if (directionValue == 0) x = 1;
            if (directionValue == 1) x = -1;

            if (!CharacterCondition.IsAttacking)
                ActorTransform.transform.localScale =
                    new Vector3 (x, ActorTransform.transform.localScale.y, ActorTransform.transform.localScale.z);
        }

        public void Jump() {
            CharacterCondition.IsOnGround = false;
            UnityComponent.PlayAnimation ("Jump");
            UnityComponent.AddForce (Vector2.up * JumpForce);
        }

        public void Attack() {
            CharacterCondition.IsAttacking = true;
            UnityComponent.PlayAnimation ("Attack", OnAttackEnd);
        }
        
        public void OnAttackEnd() {
            CharacterCondition.IsAttacking = false;
        }

        public void MoveCharacter() {
            var movement = GetMovement();
            UnityComponent.MoveCharacter (movement);
            UnityComponent.PlayAnimation ("Run");
        }

        public Vector3 GetMovement() {
            Vector3 directionValue = (CurrentDirectionValue == 0) ? Vector3.left : Vector3.right;
            return directionValue * _moveSpeed * Time.deltaTime;
        }

    #endregion

    #region Unity Callback Functions

        public void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            UnityComponent = new UnityComponent (gameObject.transform,
                _animator,
                _rigidbody2D,
                _radius);

            CharacterCondition            = new CharacterCondition();
        }

        public void Update() {
            Contract.RequireNotNull (CharacterCondition, "characterCondition");
            
            if(CharacterCondition.IsOnGround != UnityComponent.IsGrounding()) 
                CharacterCondition.IsOnGround = UnityComponent.IsGrounding();
            
            if (CharacterCondition.CanMove())
                MoveCharacter();
        }

    #endregion

    #region Private Methods

        private void OnDrawGizmos() {

            if (UnityComponent != null) {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere (UnityComponent.GetGroundCheckPosition(), _radius);
            }

        }

    #endregion
        
    }
}