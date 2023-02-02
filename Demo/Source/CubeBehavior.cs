using SimpleMan.StateMachine;
using UnityEngine;

namespace SimpleMan.StateMachineDemo
{
    public class CubeBehavior : MonoBehaviour
    {
        //This is instance of state machine. Do NOT use pre-initialization for this field like this:
        //private PureStateMachine _stateMachine = new PureStateMachine("CubeStateMachine");
        private PureStateMachine _stateMachine;




        private void Awake()
        {
            //Init your state machine inside the unity message receivers like 'Awake' or 'Start'.
            _stateMachine = new PureStateMachine("CubeStateMachine");

            Rigidbody rigidbody = GetComponent<Rigidbody>();

            //States initialization looks like any other pure C# class 
            InteractableState interactableState = new InteractableState(_stateMachine, rigidbody);
            CooldownState cooldownState = new CooldownState(_stateMachine, this);

            //Now you need to add created states into the state machine
            _stateMachine.AddState(interactableState);
            _stateMachine.AddState(cooldownState);
        }

        private void Start()
        {
            //Thats how state machine can be started
            _stateMachine.SwitchState<InteractableState>();
        }
    }
}

