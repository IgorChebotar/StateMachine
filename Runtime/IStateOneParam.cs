namespace SimpleMan.StateMachine
{
    public interface IStateOneParam<T0> : IBaseState
    {
        void Start(T0 arg0);
    }
}