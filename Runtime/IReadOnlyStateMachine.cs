using System;
using System.Collections.Generic;

namespace SimpleMan.StateMachine
{
    /// <summary>
    /// Provides functionality to gettin information about state machine. 
    /// The states can be given parameters and will have their Start and Stop methods called upon switching to and from them, respectively. 
    /// If the state implements the ITickable interface, the state machine will call the state's Tick method in regular intervals.
    /// </summary>
    public interface IReadOnlyStateMachine
    {
        /// <summary>
        /// Indicates if the state machine is running.
        /// </summary>
        public bool IsRunning { get; }

        /// <summary>
        /// Indicates if logs should be printed. 
        /// </summary>
        public bool PrintLogs { get; }

        /// <summary>
        /// Indicates if the tick is enabled for the state machine. 
        /// </summary>
        public bool IsTickEnabled { get; }

        /// <summary>
        /// The name of the state machine. 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Specifies the number of frames to dilate the tick. 
        /// </summary>
        public byte TickDilationInFrames { get; }

        /// <summary>
        /// The current state of the state machine. 
        /// </summary>
        public IBaseState CurrentState { get; }

        /// <summary>
        /// Read-only dictionary of the states in the state machine. 
        /// </summary>
        public IReadOnlyDictionary<Type, IBaseState> States { get; }
    }
}