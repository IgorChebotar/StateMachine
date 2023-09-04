using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleMan.StateMachine
{
    /// <summary>
    /// Inherit your state machine from this class.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public abstract class BaseStateMachine<TEnum> : IReadOnlyStateMachine<TEnum>, IStateMachineSwitchStatesAccess<TEnum> where TEnum : Enum
    {
        private const string DEFAULT_NAME = "Unnamed state machine";
        private string _name;
        private ILogger _logger;
        private ITickable _currentTickableState;
        private Dictionary<TEnum, IBaseState> _states = new Dictionary<TEnum, IBaseState>(8);


        public BaseStateMachine()
        {
            _name = DEFAULT_NAME;
            _logger = new DefaultLogger();
            StartTickAsync();
        }

        public BaseStateMachine(string name)
        {
            _name = name;
            _logger = new DefaultLogger();
            StartTickAsync();
        }

        public BaseStateMachine(
            ILogger customLogger,
            string name)
        {
            Logger = customLogger;
            Name = name;
            StartTickAsync();
        }


        public IReadOnlyDictionary<TEnum, IBaseState> States => _states;
        public ILogger Logger
        {
            get => _logger;
            set 
            {
                if (value.NotExist())
                {
                    PrintWarningIfPossible(
                        $"You tried to set {nameof(Logger)} as null. This operation" +
                        $"will be ingored. Set the '{nameof(PrintLogs)}' as false, if you don't want to receive messages");

                    return;
                }

                _logger = value;
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _name = DEFAULT_NAME;
                    return;
                }

                _name = value;
            }
        }
        public bool PrintLogs { get; set; } = true;
        public TEnum CurrentStateKey { get; private set; }
        public TEnum PreviousStateKey { get; private set; }


        public void AddState(TEnum key, IBaseState state)
        {
            if (_states.ContainsKey(key))
            {
                PrintWarningIfPossible(
                    $"State with key '{key}' can not be " +
                    $"added, because another state with the same id " +
                    $"already exist. This operation will be ignored");

                return;
            }

            _states.Add(key, state);

            PrintInfoIfPossible(
                $"State with key '{key}' was added");
        }

        public void RemoveState(TEnum key)
        {
            if(!_states.ContainsKey(key))
            {
                PrintWarningIfPossible(
                    $"State with key '{key}' can not be " +
                    $"removed, because it isn't exist. This operation will be ignored");

                return;
            }

            _states.Remove(key);

            PrintInfoIfPossible(
                $"State with key '{key}' was removed");
        }

        public void SwitchState(TEnum key)
        {
            ValidateStateExist(key);
            ValidateStateType<IState>(key, _states[key]);
            IState state = (IState)_states[key];

            PreviousStateKey = CurrentStateKey;
            StopPreviousState();

            CurrentStateKey = key;
            PrintStateChangedLog(key);
            TryCacheCurrentStateAsTickable();
            state.Start();
        }

        public void SwitchState<T0>(TEnum key, T0 arg0)
        {
            ValidateStateExist(key);
            ValidateStateType<IStateOneParam<T0>>(key, _states[key]);
            IStateOneParam<T0> state = (IStateOneParam<T0>)_states[key];

            PreviousStateKey = CurrentStateKey;
            StopPreviousState();

            CurrentStateKey = key;
            TryCacheCurrentStateAsTickable();
            state.Start(arg0);
        }

        public void SwitchState<T0, T1>(TEnum key, T0 arg0, T1 arg1)
        {
            ValidateStateExist(key);
            ValidateStateType<IStateTwoParams<T0, T1>>(key, _states[key]);
            IStateTwoParams<T0, T1> state = (IStateTwoParams<T0, T1>)_states[key];

            PreviousStateKey = CurrentStateKey;
            StopPreviousState();

            CurrentStateKey = key;
            TryCacheCurrentStateAsTickable();
            state.Start(arg0, arg1);
        }

        public void SwitchState<T0, T1, T2>(TEnum key, T0 arg0, T1 arg1, T2 arg2)
        {
            ValidateStateExist(key);
            ValidateStateType<IStateThreeParams<T0, T1, T2>>(key, _states[key]);
            IStateThreeParams<T0, T1, T2> state = (IStateThreeParams<T0, T1, T2>)_states[key];

            PreviousStateKey = CurrentStateKey;
            StopPreviousState();

            CurrentStateKey = key;
            TryCacheCurrentStateAsTickable();
            state.Start(arg0, arg1, arg2);
        }

        private void PrintInfoIfPossible(string message)
        {
            if (!PrintLogs)
                return;

            if (Logger.NotExist())
                return;

            Logger.PrintInfo($"{Name}: {message}");
        }

        private void PrintWarningIfPossible(string message)
        {
            if (!PrintLogs)
                return;

            if (Logger.NotExist())
                return;

            Logger.PrintWarning($"{Name}: {message}");
        }

        private void PrintStateChangedLog(TEnum newState)
        {
            PrintInfoIfPossible(
                $"State changed from '{PreviousStateKey}' " +
                $"to '{CurrentStateKey}'");
        }

        private void ValidateStateExist(TEnum key)
        {
            if (!_states.ContainsKey(key) || _states[key].NotExist())
            {
                throw new NullReferenceException(
                    $"State with key '{key}' is not exist");
            }
        }

        private void ValidateStateType<T>(TEnum key, IBaseState target) where T : IBaseState
        {
            if(target is not T)
            {
                throw new InvalidCastException(
                    $"State with key '{key}' has different type than '{typeof(T).Name}'. " +
                    $"Check number of parameters you gave to '{nameof(SwitchState)}' function");
            }
        }

        private void TryCacheCurrentStateAsTickable()
        {
            if (CurrentStateKey.NotExist() || States[CurrentStateKey] is not ITickable)
            {
                _currentTickableState = null;
                return;
            }

            _currentTickableState = (ITickable)States[CurrentStateKey];
        }

        private void StopPreviousState()
        {
            if (!_states.ContainsKey(CurrentStateKey))
                return;

            IBaseState previousState = _states[PreviousStateKey];
            previousState.Stop();
        }

        private async void StartTickAsync()
        {
            while (Application.isPlaying)
            {
                await Task.Yield();
                _currentTickableState?.Tick();
            }
        }
    }
}