# Simple man: State machine  [Download](https://github.com/IgorChebotar/StateMachine/releases)
A state machine is a design pattern that helps to model the behavior of an object by separating it into multiple states and transitions between these states. This framework provides methods to add, remove, and switch between states.
<br>
<br>
**Author:** Igor-Valerii Chebotar 
<br>
**Email:**  igor.valerii.chebotar@gmail.com

## Dependencies
* [Simple Man - Utilities](https://github.com/IgorChebotar/Utilities)
* [Simple Man - AsyncOperations](https://github.com/IgorChebotar/AsyncOperations)

## How to install plugin?
Open installer by the click on Tools -> Simple Man -> Main Installer -> [Plugins' name] -> Click 'Install' button. If you don't have one or more of the plugins this plugin depends on, you must install them first.

## Introduction:
The PureStateMachine class is a C# class that implements the IStateMachineSwitchStatesAccess interface and provides an implementation for state machine operations like adding, removing and switching between states.

## Properties
| Property name | Description                    |
| ------------- | ------------------------------ |
| IsRunning | A boolean property that indicates if the state machine is currently running |
| PrintLogs | A boolean property that indicates if logs should be printed |
| IsTickEnabled | A boolean property that indicates if the tick is enabled for the state machine |
| TickDilationInFrames | Calls target method after delay |
| Name | The name of the state machine |
| CurrentState | The current state of the state machine |
| States | Read-only dictionary of the states in the state machine |

## Methods
| Method name | Description | 
| ------------- | ------------------------------ |
| AddState | Adds a state to the state machine | 
| RemoveState | Removes a state from the state machine | 
| RemoveAllStates | Removes all states from the state machine | 
| SwitchState<TState> | Switches the current state to a new state | 
| SwitchState<TState, T0> | Switches the current state to a new state with one parameter 
| SwitchState<TState, T0, T1> | Switches the current state to a new state with two parameters |
| SwitchState<TState, T0, T1, T2> | Switches the current state to a new state with three parameters |
| SwitchState<TState, T0, T1, T2, T3> | Switches the current state to a new state with four parameters |


### C# Examples
```C# 
// Creating the state machine
PureStateMachine stateMachine = new PureStateMachine("MyStateMachine");

// Adding states to the state machine
stateMachine.AddState(new MyState());

// Switching between states
stateMachine.SwitchState<MyState>();
  
// Switching to state with float parameter
stateMachine.SwitchState<MyState, float>(3.0f);
```
