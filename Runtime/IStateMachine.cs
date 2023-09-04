using System;
using System.Collections.Generic;

namespace SimpleMan.StateMachine
{
    /// <summary>
    /// Allows user to read values of state machine
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public interface IReadOnlyStateMachine<TEnum> where TEnum : Enum
    {
        string Name { get; }
        IReadOnlyDictionary<TEnum, IBaseState> States { get; }
        TEnum PreviousStateKey { get; }
        TEnum CurrentStateKey { get; }
    }
}