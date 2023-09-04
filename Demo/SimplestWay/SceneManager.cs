using SimpleMan.Utilities;
using UnityEngine;

namespace SimpleMan.StateMachineSimpleDemo
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
            ExampleStateMachineFactory stateMachineFactory = new ExampleStateMachineFactory();
            stateMachine = stateMachineFactory.Create();
            stateMachine.SwitchState(EStates.Idle);
        }
    }
}

