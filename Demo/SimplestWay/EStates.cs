using SimpleMan.StateMachine;

namespace SimpleMan.StateMachineSimpleDemo
{
    //Use 'Bind' attribute to bind Enum key and state class type
    public enum EStates
    {
        None = 0,

        [Bind(typeof(IdleState))]
        Idle = 1,

        [Bind(typeof(FloatingState))]
        Floating = 2,

        [Bind(typeof(JumpingState))]
        Jumping = 3
    }
}

