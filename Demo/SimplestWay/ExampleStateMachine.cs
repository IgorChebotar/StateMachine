using SimpleMan.StateMachine;

namespace SimpleMan.StateMachineSimpleDemo
{
    //Inherit your state machine from the base state machine class.
    public class ExampleStateMachine : BaseStateMachine<EStates>
    {
        //You can give a special name for your state machine if you need (optional).
        //You can also add a custom debug logger as a second parameter (optional).
        public ExampleStateMachine() : base("My state machine")
        {

        }   
    }
}

