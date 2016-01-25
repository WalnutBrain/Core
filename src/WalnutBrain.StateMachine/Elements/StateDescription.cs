using System;

namespace WalnutBrain.StateMachine.Internal
{
    public class StateDescription<TState> where TState : IEquatable<TState>
    {
        public StateDescription(TState state, string displayName = null, Type stateHelperType = null)
        {
            _displayName = displayName;
            State = state;
            StateHelperType = stateHelperType;
        }


        public TState State { get; }

        public string DisplayName
        {
            get { return _displayName ?? State.ToString(); }
            
        }

        public Type StateHelperType { get; }

        internal bool Declared { get; set; } = true;

        private readonly string _displayName;


    }

    
}