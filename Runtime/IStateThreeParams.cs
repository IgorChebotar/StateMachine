namespace SimpleMan.StateMachine
{
    public interface IStateThreeParams<T0, T1, T2> : IBaseState
    {
        //------METHODS
        void Start(T0 arg0, T1 arg1, T2 arg2);
    }
}