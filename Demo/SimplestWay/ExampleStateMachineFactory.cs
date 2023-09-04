using SimpleMan.StateMachine;

namespace SimpleMan.StateMachineSimpleDemo
{
    //We need a place where the state machine will be created, and this place is factory.
    //The simplest way is make an empty factory class and inherit it from base state machine factory
    //like in this example. You can also make your custom factory or override some creation parameters. 
    //The example of the custom factory can be fould in another demo folder.
    public class ExampleStateMachineFactory : BaseStateMachineFactory<ExampleStateMachine, EStates>
    {

    }
}

