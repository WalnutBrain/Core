using System;

namespace WalnutBrain.StateMachine
{
    public interface ITransitionContext<TMachine, TState, TEvent, TEventDetail, TStateHelper>
        where TMachine : StateMachine<TState, TEvent>
        where TState : IEquatable<TState>
        where TEvent : IEquatable<TEvent>
        where TStateHelper : StateHelper<TMachine, TState, TEvent>
    {
    }

    public interface ITransitionContext<TMachine, TState, TEvent, TEventDetail>
        where TMachine : StateMachine<TState, TEvent>
        where TState : IEquatable<TState>
        where TEvent : IEquatable<TEvent>
        
    {
    }

    public interface ITransitionContextEx<TMachine, TState, TEvent, TStateHelper>
        where TMachine : StateMachine<TState, TEvent>
        where TState : IEquatable<TState>
        where TEvent : IEquatable<TEvent>
        where TStateHelper : StateHelper<TMachine, TState, TEvent>
    {
    }

    public interface ITransitionContext<TMachine, TState, TEvent>
        where TMachine : StateMachine<TState, TEvent>
        where TState : IEquatable<TState>
        where TEvent : IEquatable<TEvent>
    {
    }
}