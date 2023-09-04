using System;

namespace SimpleMan.StateMachine
{
    /// <summary>
    /// Allows user to read values of state machine and change it's states
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public interface IStateMachineSwitchStatesAccess<TEnum> : IReadOnlyStateMachine<TEnum> where TEnum : Enum
    {
        public void SwitchState(TEnum key);

        public void SwitchState<T0>(TEnum key, T0 arg0);

        public void SwitchState<T0, T1>(TEnum key, T0 arg0, T1 arg1);

        public void SwitchState<T0, T1, T2>(TEnum key, T0 arg0, T1 arg1, T2 arg2);
    }
}