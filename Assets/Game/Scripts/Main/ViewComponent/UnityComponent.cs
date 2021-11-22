using System;
using UniRx;
using UnityEngine;
using Utilities.Contract;

namespace Main.ViewComponent
{
    public interface IUnityComponent
    {
        void PlayAnimation(string animName, Action animEndCallback = null);
        void AddForce(Vector2     jumpForce);

        void    MoveCharacter(Vector3 movement);
        bool    IsGrounding();
        Vector3 GetGroundCheckPosition();
    }

    public class UnityComponent : IUnityComponent
    {
        private Transform   _transform;
        private Animator    _animator;
        private Rigidbody2D _rigidbody2D;
        private Transform   _groundSensorTransform;
        private int         _groundLayer;
        private float       _radius;

    #region Constructor

        public UnityComponent(Transform transform) {
            _transform = transform;
        }

        public UnityComponent(Animator animator) {
            _animator = animator;
        }

        public UnityComponent(Rigidbody2D rigidbody2D) {
            _rigidbody2D = rigidbody2D;
        }

        public UnityComponent(Transform   transform,
                              Animator    animator,
                              Rigidbody2D rigidbody2D,
                              float radius) {
            _radius      = radius;
            _groundLayer = (1 << LayerMask.NameToLayer ("Ground"));

            _transform   = transform;
            _animator    = animator;
            _rigidbody2D = rigidbody2D;

            var boxCollider2D = _transform.GetComponent<BoxCollider2D>();
            var height        = boxCollider2D.size.y / 2;
            var offsetY       = boxCollider2D.offset.y;
            var groundY       = -height - offsetY;
            _groundSensorTransform                  = new GameObject ("Actor Ground").transform;
            _groundSensorTransform.transform.parent = _transform;
            _groundSensorTransform.localPosition    = Vector3.up * groundY;
        }

    #endregion

        public void PlayAnimation(string animName, Action animEndCallback = null) {
            Contract.RequireNotNull (_animator, "Animator");
            var currentClip     = _animator.GetCurrentAnimatorClipInfo (0)[0].clip;
            var currentClipName = currentClip.name;
            if (currentClipName == animName)
                return;


            _animator.Play (animName);
            if (animEndCallback != null) {
                var clipLength = _animator.GetCurrentAnimatorClipInfo (0)[0].clip.length - Time.deltaTime;
                Observable.Timer (TimeSpan.FromSeconds (clipLength))
                    .Subscribe (_ => animEndCallback?.Invoke());
            }
        }

        public void AddForce(Vector2 jumpForce) {
            Contract.RequireNotNull (_rigidbody2D, "Rigidbody2D");
            _rigidbody2D.AddForce (jumpForce, ForceMode2D.Impulse);
        }

        public void MoveCharacter(Vector3 movement) {
            Contract.RequireNotNull (_transform, "Transform");
            _transform.position += movement;
        }

        public bool IsGrounding() {
            return Physics2D.OverlapCircle (_groundSensorTransform.position, _radius, _groundLayer);
        }

        public Vector3 GetGroundCheckPosition() {
           return _groundSensorTransform.position;
        }
    }
}