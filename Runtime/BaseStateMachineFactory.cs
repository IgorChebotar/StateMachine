using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleMan.StateMachine
{
    /// <summary>
    /// Base factory class for creating state machines and their associated states. Inherit 
    /// your custom state machine factories from it.
    /// </summary>
    public abstract class BaseStateMachineFactory<TStateMachine, TEnum>
        where TStateMachine : BaseStateMachine<TEnum>, new()
        where TEnum : Enum
    {
        /// <summary>
        /// Creates a new instance of the state machine and its associated states.
        /// </summary>
        /// <returns>The created state machine.</returns>
        public TStateMachine Create()
        {
            ValidateEnumHasNoneValue();
            BeforeStateMachineCreated();
            TStateMachine stateMachine = CreateStateMachine();
            CreateStates(stateMachine);
            AfterStateMachineCreated(stateMachine);
            return stateMachine;
        }

        /// <summary>
        /// Creates a new instance of the state machine by default. You can override (without calling base) this 
        /// method to create your state machine in a custom way.
        /// </summary>
        /// <returns>The created state machine.</returns>
        protected virtual TStateMachine CreateStateMachine()
        {
            return new TStateMachine();
        }

        /// <summary>
        /// Creates instances of states by default. You can override (without calling base) this 
        /// method to create your states in a custom way.
        /// </summary>
        /// <param name="stateType"></param>
        /// <returns></returns>
        protected virtual IBaseState CreateState(Type stateType)
        {
            ValidateStateHasNotParams(stateType);
            return (IBaseState)Activator.CreateInstance(stateType);
        }

        /// <summary>
        /// Method called before the state machine is created.
        /// </summary>
        protected virtual void BeforeStateMachineCreated()
        {

        }

        /// <summary>
        /// Method called after the state machine is created.
        /// </summary>
        /// <param name="stateMachine">The created state machine.</param>
        protected virtual void AfterStateMachineCreated(TStateMachine stateMachine)
        {

        }

        private Dictionary<TEnum, IBaseState> CreateStates()
        {
            Dictionary<TEnum, Type> bindedStates = GetBindedStates();
            Dictionary<TEnum, IBaseState> result = new Dictionary<TEnum, IBaseState>(bindedStates.Count);

            foreach (var pair in bindedStates)
            {
                IBaseState state = CreateState(pair.Value);
                result.Add(pair.Key, state);
            }

            return result;
        }

        private Dictionary<TEnum, Type> GetBindedStates()
        {
            Array keys = Enum.GetValues(typeof(TEnum));
            Dictionary<TEnum, Type> result = new Dictionary<TEnum, Type>(keys.Length);

            foreach (var key in keys)
            {
                if (TryGetBindedType(key, out Type bindedType))
                    result.Add((TEnum)key, bindedType);
            }

            return result;
        }

        private bool TryGetBindedType(object key, out Type result)
        {
            result = null;
            MemberInfo memberInfo = typeof(TEnum).GetMember(key.ToString()).FirstOrDefault();

            if (memberInfo.NotExist())
                return false;

            BindAttribute binderAttribute = (BindAttribute)memberInfo.GetCustomAttribute(typeof(BindAttribute), false);
            if (binderAttribute.NotExist())
                return false;

            result = binderAttribute.bindedType;
            return true;
        }

        private TStateMachine CreateStates(TStateMachine stateMachine)
        {
            foreach (var pair in CreateStates())
            {
                stateMachine.AddState(pair.Key, pair.Value);
            }

            return stateMachine;
        }

        private void ValidateStateHasNotParams(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                if(constructor.GetParameters().Length > 0)
                {
                    throw new InvalidProgramException(
                        $"Problem with type '{type.Name}'. Creation states with parameters not supported. " +
                        $"Check constructors inside your state class or create this state manually");
                }
            }
        }

        private void ValidateEnumHasNoneValue()
        {
            Array values = Enum.GetValues(typeof(TEnum));
            if(values.Length == 0 || values.GetValue(0).ToString() != "None")
            {
                throw new InvalidProgramException(
                    $"State declaration file (Enum) must have a 'None' value as first");
            }
        }
    }
}


