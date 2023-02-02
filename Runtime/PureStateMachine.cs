using SimpleMan.AsyncOperations;
using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMan.StateMachine
{
    /// <summary>
    /// PureStateMachine is a class that implements a state machine pattern. 
    /// It provides functionality to add, remove, and switch between different states. 
    /// The states can be given parameters and will have their Start and Stop methods called upon switching to and from them, respectively. 
    /// If the state implements the ITickable interface, the state machine will call the state's Tick method in regular intervals.
    /// </summary>
    public class PureStateMachine : IStateMachineSwitchStatesAccess
    {
        private string _name = "Unnamed state machine";
        private Dictionary<Type, IBaseState> _states = new Dictionary<Type, IBaseState>(8);
        private ITickable _currentTickableState;




        /// <summary>
        /// Indicates if the state machine is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Indicates if logs should be printed. 
        /// </summary>
        public bool PrintLogs { get; set; } = true;

        /// <summary>
        /// Indicates if the tick is enabled for the state machine. 
        /// </summary>
        public bool IsTickEnabled { get; set; } = true;

        /// <summary>
        /// Specifies the number of frames to dilate the tick. 
        /// </summary>
        public byte TickDilationInFrames { get; private set; } = 0;

        /// <summary>
        /// The name of the state machine. 
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                _name = value;
            }
        }

        /// <summary>
        /// The current state of the state machine. 
        /// </summary>
        public IBaseState CurrentState { get; private set; }

        /// <summary>
        /// Read-only dictionary of the states in the state machine. 
        /// </summary>
        public IReadOnlyDictionary<Type, IBaseState> States
        {
            get => _states;
        }




        public PureStateMachine(string name)
        {
            Name = name;
        }

        public PureStateMachine(string name, byte tickDilationInFrames)
        {
            Name = name;


            IsTickEnabled = true;
            TickDilationInFrames = tickDilationInFrames;
        }

        /// <summary>
        /// Adds a state to the state machine. 
        /// </summary>
        /// <param name="state">The state to add to the state machine.</param>
        public void AddState(IBaseState state)
        {
            if(state == null)
            {
                PrintToConsole.Error(
                    this,
                    $"Argument '{state}' doesn't exist");

                return;
            }

            _states.Add(state.GetType(), state);
        }

        /// <summary>
        /// Removes a state from the state machine
        /// </summary>
        /// <param name="state">The state to be removed</param>
        public void RemoveState(IBaseState state)
        {
            _states.Remove(state.GetType());
        }

        /// <summary>
        /// Removes all states from the state machine
        /// </summary>
        public void RemoveAllStates()
        {
            _states.Clear();
        }

        /// <summary>
        /// Switches the current state to a new state
        /// </summary>
        /// <typeparam name="TState">The type of the new state</typeparam>
        public void SwitchState<TState>() where TState : class, IState
        {
            StartTickIfNeed();


            CurrentState?.Stop();
            IBaseState previousState = CurrentState;


            TState newState = GetState<TState>();
            CurrentState = newState;


            TryToCacheAsTickableState(newState);


            PrintStateChangedLog(previousState, newState);
            newState.Start();
        }

        /// <summary>
        /// Switches the current state to a new state with one parameter
        /// </summary>
        /// <typeparam name="TState">The type of the new state</typeparam>
        /// <typeparam name="T0">The type of the first parameter</typeparam>
        /// <param name="arg0">The first parameter</param>
        public void SwitchState<TState, T0>(T0 arg0) where TState : class, IStateOneParam<T0>
        {
            StartTickIfNeed();


            CurrentState?.Stop();
            IBaseState previousState = CurrentState;


            TState newState = GetState<TState>();
            CurrentState = newState;


            TryToCacheAsTickableState(newState);


            PrintStateChangedLog(previousState, newState);
            newState.Start(arg0);
        }

        /// <summary>
        /// Switches the current state to a new state with two parameters
        /// </summary>
        /// <typeparam name="TState">The type of the new state</typeparam>
        /// <typeparam name="T0">The type of the first parameter</typeparam>
        /// <typeparam name="T1">The type of the second parameter</typeparam>
        /// <param name="arg0">The first parameter</param>
        /// <param name="arg1">The second parameter</param>
        public void SwitchState<TState, T0, T1>(T0 arg0, T1 arg1) where TState : class, IStateTwoParams<T0, T1>
        {
            StartTickIfNeed();


            CurrentState?.Stop();
            IBaseState previousState = CurrentState;


            TState newState = GetState<TState>();
            CurrentState = newState;


            TryToCacheAsTickableState(newState);


            PrintStateChangedLog(previousState, newState);
            newState.Start(arg0, arg1);
        }

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
        public void SwitchState<TState, T0, T1, T3>(T0 arg0, T1 arg1, T3 arg3) where TState : class, IStateThreeParams<T0, T1, T3>
        {
            StartTickIfNeed();


            CurrentState?.Stop();
            IBaseState previousState = CurrentState;


            TState newState = GetState<TState>();
            CurrentState = newState;


            TryToCacheAsTickableState(newState);


            PrintStateChangedLog(previousState, newState);
            newState.Start(arg0, arg1, arg3);
        }

        private void Tick()
        {
            _currentTickableState?.Tick();
        }

        private void StartTickIfNeed()
        {
            if (IsRunning) return;

            IsRunning = true;
            SafeAsync.RepeatWhile(() => IsTickEnabled, Tick, TickDilationInFrames);
        }

        private TState GetState<TState>() where TState : class, IBaseState
        {
            if(!_states.ContainsKey(typeof(TState)))
            {
                PrintToConsole.Error(
                    this,
                    $"This state machine doesn't contains the '{typeof(TState).Name}' state");

                return null;
            }
            return _states[typeof(TState)] as TState;
        }

        private void PrintStateChangedLog(IBaseState previousState, IBaseState currentState)
        {
            if (!PrintLogs)
                return;

            string GetStateName(IBaseState state)
            {
                return state == null ? "None" : state.GetType().Name;
            }
            Debug.Log($"<b>{_name}</b>: State changed from '<b>{GetStateName(previousState)}</b>' to '<b>{GetStateName(currentState)}</b>'");
        }

        private void TryToCacheAsTickableState(IBaseState state)
        {
            _currentTickableState = null;

            if(state is ITickable)
                _currentTickableState = (ITickable)state;
        }
    }
}