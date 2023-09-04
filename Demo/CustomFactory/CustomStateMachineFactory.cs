using SimpleMan.StateMachine;
using System.Collections.Generic;

namespace SimpleMan.StateMachineCustomFactoryDemo
{
    //Use your own factories if you want more control over creation.
    //process.You can also create states with parameterized constructors.
    public class CustomStateMachineFactory 
    {
        public ExampleStateMachine Create()
        {
            //Create instance of the our example state machine
            ExampleStateMachine instance = new ExampleStateMachine();

            //Add states to the state machine instance above
            foreach (var pair in CreateStates())
            {
                instance.AddState(pair.Key, pair.Value);
            }

            return instance;
        }

        private Dictionary<EStates, IBaseState> CreateStates()
        {
            //Create and bind state classes to Enum keys
            return new Dictionary<EStates, IBaseState>(8)
            {
                { EStates.Idle, new IdleState() },
                { EStates.Jumping, new JumpingState(7) },
                { EStates.Floating, new FloatingState(2) },
            };
        }
    }
}

