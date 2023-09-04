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
        
    }
}