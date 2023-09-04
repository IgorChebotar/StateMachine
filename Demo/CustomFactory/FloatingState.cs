using SimpleMan.StateMachine;
using UnityEngine;

namespace SimpleMan.StateMachineCustomFactoryDemo
{
    public class FloatingState : IState, ITickable
    {
        private readonly float _movementSpeed = 2;
        private float _sinValue;
        private Transform _actorTransform;

        public FloatingState(float movementSpeed)
        {
            _movementSpeed = movementSpeed;
        }

        public void Start()
        {
            _actorTransform = SceneManager.instance.actor.transform;
            _actorTransform.position = Vector3.zero;
            _sinValue = 0;
        }

        public void Stop()
        {

        }

        public void Tick()
        {
            _sinValue += Time.deltaTime;
            _actorTransform.position = Vector3.up * Mathf.Sin(_movementSpeed * _sinValue);
        }
    }
}

