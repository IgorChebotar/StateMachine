using SimpleMan.StateMachine;
using UnityEngine;

namespace SimpleMan.StateMachineDemo
{
    /// <summary>
    /// This state allows object to jump when player clicks on it
    /// </summary>
    public class InteractableState : IState, ITickable
    {
        private const float IMPULSE_FORCE = 5;
        private const float COOLDOWN = 2;

        private readonly IStateMachineSwitchStatesAccess _stateMachine;
        private readonly Rigidbody _rigidbody;

        public InteractableState(
            IStateMachineSwitchStatesAccess stateMachine, 
            Rigidbody rigidbody)
        {
            _stateMachine = stateMachine;
            _rigidbody = rigidbody;
        }

        /// <summary>
        /// Called when this state start working
        /// </summary>
        public void Start()
        {
            
        }

        /// <summary>
        /// Called when this state stop working
        /// </summary>
        public void Stop()
        {
            
        }

        /// <summary>
        /// Called every frame (like simple Update method in MonoBehaviors)
        /// </summary>
        public void Tick()
        {
            if(IsClickDetected())
            {
                MakeJump();
                ToCooldowState();
            }
        }

        /// <summary>
        /// Switch state machine to the cooldow state after each 'jump'
        /// </summary>
        private void ToCooldowState()
        {
            _stateMachine.SwitchState<CooldownState, float>(COOLDOWN);
        }

        private bool IsClickDetected()
        {
            if (!Input.GetMouseButtonDown(0))
                return false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit))
                return false;

            if (hit.rigidbody != _rigidbody)
                return false;

            return true;
        }

        private void MakeJump()
        {
            _rigidbody.AddForce(Vector3.up * IMPULSE_FORCE, ForceMode.Impulse);
        }
    }
}

