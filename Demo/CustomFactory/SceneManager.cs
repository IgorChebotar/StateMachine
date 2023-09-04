using SimpleMan.Utilities;
using UnityEngine;

namespace SimpleMan.StateMachineCustomFactoryDemo
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager instance;
        public GameObject actor;
        public ExampleStateMachine stateMachine;

        private void Awake()
        {
            if(instance.NotExist())
                instance = this;
        }

        private void Start()
        {
            CustomStateMachineFactory stateMachineFactory = new CustomStateMachineFactory();
            stateMachine = stateMachineFactory.Create();
            stateMachine.SwitchState(EStates.Idle);
        }
    }
}

