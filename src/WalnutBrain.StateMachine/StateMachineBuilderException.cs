using System;
using System.Runtime.Serialization;

namespace WalnutBrain.StateMachine
{
    [Serializable]
    public class StateMachineBuilderException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public StateMachineBuilderException()
        {
        }

        public StateMachineBuilderException(string message) : base(message)
        {
        }

        public StateMachineBuilderException(string message, Exception inner) : base(message, inner)
        {
        }

        protected StateMachineBuilderException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}