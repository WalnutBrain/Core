namespace WalnutBrain.StateMachine.Internal
{
    public class LocalDescription
    {
        public bool Permanent { get; set; }
        public bool ReadOnly { get; set; }
        public object DefaultValue { get; set; }
        public object Initializator { get; set; }
        public bool WithStateHelper { get; set; }
    }

    
}