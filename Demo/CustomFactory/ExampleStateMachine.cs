using SimpleMan.StateMachine;

namespace SimpleMan.StateMachineCustomFactoryDemo
{
    public class ExampleStateMachine : BaseStateMachine<EStates>
    {
        public ExampleStateMachine() : base("My state machine")
        {

        }   
    }
}

