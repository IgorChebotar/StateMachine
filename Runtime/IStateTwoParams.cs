namespace SimpleMan.StateMachine
{
    public interface IStateTwoParams<T0, T1> : IBaseState
    {
        void Start(T0 arg0, T1 arg1);
    }
}