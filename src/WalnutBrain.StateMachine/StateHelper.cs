using System;

namespace WalnutBrain.StateMachine
{
    public class StateHelper<TMachine, TState, TEvent>
        where TMachine : StateMachine<TState, TEvent>
        where TState : IEquatable<TState>
        where TEvent : IEquatable<TEvent>
    {
         
    }
}