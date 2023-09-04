using SimpleMan.StateMachine;
using UnityEngine;

namespace SimpleMan.StateMachineCustomFactoryDemo
{
    public class JumpingState : IState, ITickable
    {
        private readonly float _jumpForce;
        private Transform _actorTransform;
        private Rigidbody _rigidbody;

        public JumpingState(float jumpForce)
        {
            _jumpForce = jumpForce;
        }

        public void Start()
        {
            _actorTransform = SceneManager.instance.actor.transform;
            _actorTransform.position = Vector3.zero;

            _rigidbody = _actorTransform.GetComponent<Rigidbody>();
            _rigidbody.isKinematic = false;
        }

        public void Stop()
        {
            _rigidbody.isKinematic = true;
        }

        public void Tick()
        {
            if(IsActorGrounded())
                PerformJump();
        }

        private bool IsActorGrounded()
        {
            return _actorTransform.position.y <= 0;
        }

        private void PerformJump()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}

