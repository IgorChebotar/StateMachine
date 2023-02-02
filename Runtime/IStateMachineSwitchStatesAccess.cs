namespace SimpleMan.StateMachine
{
    /// <summary>
    /// Provides functionality to switch between different states. 
    /// The states can be given parameters and will have their Start and Stop methods called upon switching to and from them, respectively. 
    /// If the state implements the ITickable interface, the state machine will call the state's Tick method in regular intervals.
    /// </summary>
    public interface IStateMachineSwitchStatesAccess : IReadOnlyStateMachine
    {
        /// <summary>
        /// Switches the current state to a new state
        /// </summary>
        /// <typeparam name="TState">The type of the new state</typeparam>
        public void SwitchState<TState>() where TState : class, IState;

        /// <summary>
        /// Switches the current state to a new state with one parameter
        /// </summary>
        /// <typeparam name="TState">The type of the new state</typeparam>
        /// <typeparam name="T0">The type of the first parameter</typeparam>
        /// <param name="arg0">The first parameter</param>
        public void SwitchState<TState, T0>(T0 arg0) where TState : class, IStateOneParam<T0>;

        /// <summary>
        /// Switches the current state to a new state with two parameters
        /// </summary>
        /// <typeparam name="TState">The type of the new state</typeparam>
        /// <typeparam name="T0">The type of the first parameter</typeparam>
        /// <typeparam name="T1">The type of the second parameter</typeparam>
        /// <param name="arg0">The first parameter</param>
        /// <param name="arg1">The second parameter</param>
        public void SwitchState<TState, T0, T1>(T0 arg0, T1 arg1) where TState : class, IStateTwoParams<T0, T1>;

        /// <summary>
        /// Switches the current state to a new state with three parameters
        /// </summary>
        /// <typeparam name="TState">The type of the new state</typeparam>
        /// <typeparam name="T0">The type of the first parameter</typeparam>
        /// <typeparam name="T1">The type of the second parameter</typeparam>
        /// <typeparam name="T3">The type of the third parameter</typeparam>
        /// <param name="arg0">The first parameter</param>
        /// <param name="arg1">The second parameter</param>
        /// <param name="arg3">The third parameter</param>
        public void SwitchState<TState, T0, T1, T3>(T0 arg0, T1 arg1, T3 arg3) where TState : class, IStateThreeParams<T0, T1, T3>;
    }
}