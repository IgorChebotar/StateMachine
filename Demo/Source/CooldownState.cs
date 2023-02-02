using SimpleMan.AsyncOperations;
using SimpleMan.StateMachine;
using SimpleMan.Utilities;
using UnityEngine;

namespace SimpleMan.StateMachineDemo
{
    /// <summary>
    /// This state waits before cooldown time is up and returns to 
    /// interactable state. This is example how state can work with parameter
    /// </summary>
    public class CooldownState : IStateOneParam<float>
    {
        private readonly IStateMachineSwitchStatesAccess _stateMachine;
        private readonly MonoBehaviour _coroutineRunner;
        private Coroutine _delayRoutine;

        public CooldownState(
            IStateMachineSwitchStatesAccess stateMachine,
            MonoBehaviour coroutineRunner)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
        }

        /// <summary>
        /// Called when this state start working
        /// </summary>
        public void Start(float waitTime)
        {
            _delayRoutine = _coroutineRunner.
                Delay(waitTime, ToInteractableState);
        }

        /// <summary>
        /// Called when this state stop working
        /// </summary>
        public void Stop()
        {
            if (_delayRoutine.Exist())
                _coroutineRunner.StopCoroutine(_delayRoutine);
        }

        /// <summary>
        /// Switch state machine back to interactable state
        /// </summary>
        private void ToInteractableState()
        {
            _stateMachine.SwitchState<InteractableState>();
        }
    }
}

