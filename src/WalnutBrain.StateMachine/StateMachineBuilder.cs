using System;
using System.Collections.Generic;
using WalnutBrain.StateMachine.Internal;

namespace WalnutBrain.StateMachine
{
    public abstract class StateMachineBuilder<TMachine, TState, TEvent> 
        : IMachineBuilder<TMachine, TState, TEvent> 
        where TEvent : IEquatable<TEvent> 
        where TState : IEquatable<TState> 
        where TMachine : StateMachine<TState, TEvent>
    {
        public IStateBuilder<TMachine, TState, TEvent> State(TState state, string displayName = null)
        {
            return new StateBuilder<TMachine, TState, TEvent>(this, RegisterState(state, displayName, null));
        }

        private StateDescription<TState> RegisterState(TState state, string displayName, Type stateHelperType)
        {
            StateDescription<TState> description;
            if (!States.TryGetValue(state, out description))
            {
                description = new StateDescription<TState>(state, displayName, stateHelperType);
                States.Add(state, description);
            }
            else
            {
                description.Declared = true;
            }
            return description;
        }

        public IStateBuilder<TMachine, TState, TEvent, TStateHelper> State<TStateHelper>(TState state, string displayName = null) where TStateHelper : StateHelper<TMachine, TState, TEvent>
        {
            
            return new StateBuilder<TMachine, TState, TEvent, TStateHelper>(this, RegisterState(state, displayName, typeof(TStateHelper)));
        }

        public IMachineBuilder<TMachine, TState, TEvent> Global<TGlobal>(string name, TGlobal @default = default(TGlobal))
        {
            Tuple<Type, object> glb;
            if(Globals.TryGetValue(name, out glb))
                throw new StateMachineBuilderException("Global with name {0} already registered".AsFormat(name));
            Globals.Add(name, Tuple.Create(typeof(TGlobal), (object) @default));
            return this;
        }

        public IMachineBuilder<TMachine, TState, TEvent> Global<TGlobal>(string name, Func<IInitGlobalContext<TMachine, TState, TEvent>, TGlobal> initializer)
        {
            Tuple<Type, object> glb;
            if (Globals.TryGetValue(name, out glb))
                throw new StateMachineBuilderException("Global with name {0} already registered".AsFormat(name));
            Globals.Add(name, Tuple.Create(typeof(TGlobal), (object)initializer));
            return this;
        }

        protected abstract void Configure();

        public void Build(TMachine machine)
        {
            throw new NotImplementedException();
        }

        internal Dictionary<TState, StateDescription<TState>> States { get; } =
            new Dictionary<TState, StateDescription<TState>>();
        internal Dictionary<string, Tuple<Type, object>> Globals { get; } =
            new Dictionary<string, Tuple<Type, object>>(StringComparer.InvariantCultureIgnoreCase);
    }

    internal class StateBuilderBase<TState>
        where TState : IEquatable<TState>
    {
        public StateBuilderBase(StateDescription<TState> stateDescription)
        {
            StateDescription = stateDescription;
        }

        protected void AddLocal(string name, object @default, object initializer, bool permanent, bool readOnly, bool withStateHelper)
        {
            LocalDescription desc;
            if (Locals.TryGetValue(name, out desc))
                throw new StateMachineBuilderException("Local with name {0} in state {1} already registered".AsFormat(name,
                    StateDescription.State));
            Locals.Add(name, new LocalDescription
            {
                Permanent = permanent,
                ReadOnly = readOnly,
                DefaultValue = @default,
                Initializator = initializer,
                WithStateHelper = withStateHelper
            });
        }

        internal Dictionary<string, LocalDescription> Locals { get; } =
            new Dictionary<string, LocalDescription>(StringComparer.InvariantCultureIgnoreCase);

        public StateDescription<TState> StateDescription { get;  }
    }

    internal class StateBuilder<TMachine, TState, TEvent> : StateBuilderBase<TState>, IStateBuilder<TMachine, TState, TEvent>
        where TEvent : IEquatable<TEvent>
        where TState : IEquatable<TState>
        where TMachine : StateMachine<TState, TEvent>

    {
        private readonly StateMachineBuilder<TMachine, TState, TEvent> _machineBuilder;

        public StateBuilder(StateMachineBuilder<TMachine, TState, TEvent> machineBuilder, StateDescription<TState> stateDescription) : base(stateDescription)
        {
            _machineBuilder = machineBuilder;
        }

        public IStateBuilder<TMachine, TState, TEvent> Local<TLocal>(string name, TLocal @default = default(TLocal), bool permanent = false,
            bool readOnly = false)
        {
            AddLocal(name, @default, null, permanent, readOnly, false);
            return this;
        }

        public IStateBuilder<TMachine, TState, TEvent> Local<TLocal>(string name, Func<IEnterStateContext<TMachine, TState, TEvent>, TLocal> initializer = null, bool permanent = false, bool readOnly = false)
        {
            AddLocal(name, null, initializer, permanent, readOnly, false);
            return this;
        }
    }

    internal class StateBuilder<TMachine, TState, TEvent, TStateHelper> : StateBuilderBase<TState>, IStateBuilder<TMachine, TState, TEvent, TStateHelper>
        where TEvent : IEquatable<TEvent>
        where TState : IEquatable<TState>
        where TMachine : StateMachine<TState, TEvent>
        where TStateHelper : StateHelper<TMachine, TState, TEvent>
    {
        private readonly StateMachineBuilder<TMachine, TState, TEvent> _machineBuilder;

        public StateBuilder(StateMachineBuilder<TMachine, TState, TEvent> machineBuilder, StateDescription<TState> stateDescription) : base(stateDescription)
        {
            _machineBuilder = machineBuilder;
        }

        public IStateBuilder<TMachine, TState, TEvent, TStateHelper> Local<TLocal>(string name, TLocal @default = default(TLocal), bool permanent = false,
            bool readOnly = false)
        {
            AddLocal(name, @default, null, permanent, readOnly, true);
            return this;
        }

        public IStateBuilder<TMachine, TState, TEvent, TStateHelper> Local<TLocal>(string name, Func<IEnterStateContext<TMachine, TState, TEvent, TStateHelper>, TLocal> initializer = null, bool permanent = false, bool readOnly = false)
        {
            AddLocal(name, null, initializer, permanent, readOnly, true);
            return this;
        }

        internal Dictionary<string, LocalDescription> Locals { get; } =
            new Dictionary<string, LocalDescription>(StringComparer.InvariantCultureIgnoreCase);
    }



}