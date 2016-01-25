using System;

namespace WalnutBrain.StateMachine
{
    public interface IStateBuilder<TMachine, TState, TEvent>
        where TMachine : StateMachine<TState, TEvent>
        where TState : IEquatable<TState>
        where TEvent : IEquatable<TEvent>
    {
        IStateBuilder<TMachine, TState, TEvent> AfterEnter(Action<IEnterStateContext<TMachine, TState, TEvent>>  onEnter);
        IStateBuilder<TMachine, TState, TEvent> BeforeExit(Action<IExitStateContext<TMachine, TState, TEvent>> onEnter);

        IStateBuilder<TMachine, TState, TEvent> Local<TLocal>(string name, TLocal @default = default(TLocal),
            bool permanent = false, bool readOnly = false);

        IStateBuilder<TMachine, TState, TEvent> Local<TLocal>(string name,
            Func<IEnterStateContext<TMachine, TState, TEvent>, TLocal> initializer = null, bool permanent = false,
            bool readOnly = false);

        IStateBuilder<TMachine, TState, TEvent> Transition(TEvent @event, TState toState,
            Func<ITransitionContext<TMachine, TState, TEvent>, bool> predicate = null, Action<ITransitionContext<TMachine, TState, TEvent>> onTransition = null,
            bool reeterability = false);

        IStateBuilder<TMachine, TState, TEvent> Transition(TEvent @event, Func<ITransitionContext<TMachine, TState, TEvent>,TState> stateSelector,
            Func<ITransitionContext<TMachine, TState, TEvent>, bool> predicate = null, Action<ITransitionContext<TMachine, TState, TEvent>> onTransition = null,
            bool reeterability = false);

        IStateBuilder<TMachine, TState, TEvent> Transition<TEventDetail>(TEvent @event, TState toState,
            Func<ITransitionContext<TMachine, TState, TEvent, TEventDetail>, bool> predicate = null, Action<ITransitionContext<TMachine, TState, TEvent, TEventDetail>> onTransition = null,
            bool reeterability = false);

        IStateBuilder<TMachine, TState, TEvent> Transition<TEventDetail>(TEvent @event, Func<ITransitionContext<TMachine, TState, TEvent, TEventDetail>, TState> stateSelector,
            Func<ITransitionContext<TMachine, TState, TEvent, TEventDetail>, bool> predicate = null, Action<ITransitionContext<TMachine, TState, TEvent, TEventDetail>> onTransition = null,
            bool reeterability = false);
    }

    public interface IStateBuilder<TMachine, TState, TEvent, TStateHelper>
        where TMachine : StateMachine<TState, TEvent>
        where TState : IEquatable<TState>
        where TEvent : IEquatable<TEvent>
        where TStateHelper : StateHelper<TMachine, TState, TEvent>
    {
        IStateBuilder<TMachine, TState, TEvent, TStateHelper> AfterEnter(Action<IEnterStateContext<TMachine, TState, TEvent, TStateHelper>> onEnter);
        IStateBuilder<TMachine, TState, TEvent, TStateHelper> BeforeExit(Action<IExitStateContext<TMachine, TState, TEvent, TStateHelper>> onEnter);

        IStateBuilder<TMachine, TState, TEvent, TStateHelper> Local<TLocal>(string name, TLocal @default = default(TLocal),
            bool permanent = false, bool readOnly = false);

        IStateBuilder<TMachine, TState, TEvent, TStateHelper> Local<TLocal>(string name,
            Func<IEnterStateContext<TMachine, TState, TEvent, TStateHelper>, TLocal> initializer = null, bool permanent = false,
            bool readOnly = false);

        IStateBuilder<TMachine, TState, TEvent, TStateHelper> Transition(TEvent @event, TState toState,
            Func<ITransitionContext<TMachine, TState, TEvent, TStateHelper>, bool> predicate = null, Action<ITransitionContext<TMachine, TState, TEvent, TStateHelper>> onTransition = null,
            bool reeterability = false);

        IStateBuilder<TMachine, TState, TEvent, TStateHelper> Transition(TEvent @event, Func<ITransitionContextEx<TMachine, TState, TEvent, TStateHelper>, TState> stateSelector,
            Func<ITransitionContextEx<TMachine, TState, TEvent, TStateHelper>, bool> predicate = null, Action<ITransitionContextEx<TMachine, TState, TEvent, TStateHelper>> onTransition = null,
            bool reeterability = false);

        IStateBuilder<TMachine, TState, TEvent, TStateHelper> Transition<TEventDetail>(TEvent @event, TState toState,
            Func<ITransitionContext<TMachine, TState, TEvent, TEventDetail, TStateHelper>, bool> predicate = null, Action<ITransitionContext<TMachine, TState, TEvent, TEventDetail, TStateHelper>> onTransition = null,
            bool reeterability = false);

        IStateBuilder<TMachine, TState, TEvent, TStateHelper> Transition<TEventDetail>(TEvent @event, Func<ITransitionContext<TMachine, TState, TEvent, TEventDetail, TStateHelper>, TState> stateSelector,
            Func<ITransitionContext<TMachine, TState, TEvent, TEventDetail, TStateHelper>, bool> predicate = null, Action<ITransitionContext<TMachine, TState, TEvent, TEventDetail, TStateHelper>> onTransition = null,
            bool reeterability = false);
    }
}