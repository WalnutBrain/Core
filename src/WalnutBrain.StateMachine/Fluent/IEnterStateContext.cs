using System;

namespace WalnutBrain.StateMachine
{
    public interface IEnterStateContext<TMachine, TState, TEvent, TStateHelper>
        where TMachine : StateMachine<TState, TEvent>
        where TState : IEquatable<TState>
        where TEvent : IEquatable<TEvent>
        where TStateHelper : StateHelper<TMachine, TState, TEvent>
    {
    }

    public interface IEnterStateContext<TMachine, TState, TEvent>
        where TMachine : StateMachine<TState, TEvent>
        where TState : IEquatable<TState>
        where TEvent : IEquatable<TEvent>
    {
    }
}