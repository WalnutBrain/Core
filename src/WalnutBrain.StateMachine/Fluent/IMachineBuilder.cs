using System;

namespace WalnutBrain.StateMachine
{
    public interface IMachineBuilder<TMachine, TState, TEvent>
        where TMachine : StateMachine<TState, TEvent>
        where TState : IEquatable<TState>
        where TEvent : IEquatable<TEvent>
    {
        IStateBuilder<TMachine, TState, TEvent> State(TState state, string displayName = null);
        IStateBuilder<TMachine, TState, TEvent, TStateHelper> State<TStateHelper>(TState state, string displayName = null) where TStateHelper : StateHelper<TMachine, TState, TEvent>;

        IMachineBuilder<TMachine, TState, TEvent> Global<TGlobal>(string name, TGlobal @default = default(TGlobal));

        IMachineBuilder<TMachine, TState, TEvent> Global<TGlobal>(string name, Func<IInitGlobalContext<TMachine, TState, TEvent>, TGlobal> initializer);
    }

    public interface IInitGlobalContext<TMachine, TState, TEvent>
        where TMachine : StateMachine<TState, TEvent>
        where TState : IEquatable<TState>
        where TEvent : IEquatable<TEvent>
    {
    }
}