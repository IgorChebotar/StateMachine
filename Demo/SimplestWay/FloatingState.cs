using SimpleMan.StateMachine;
using UnityEngine;

namespace SimpleMan.StateMachineSimpleDemo
{
    public class FloatingState : IState, ITickable
    {
        private const float MOVEMENT_SPEED = 2;
        private float _sinValue;
        private Transform _actorTransform;

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
            _actorTransform.position = Vector3.up * Mathf.Sin(MOVEMENT_SPEED * _sinValue);
        }
    }
}

