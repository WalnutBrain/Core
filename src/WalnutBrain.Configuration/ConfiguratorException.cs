using System;
using System.Runtime.Serialization;

namespace WalnutBrain.Configuration
{
    [Serializable]
    public class ConfiguratorException : Exception
    {
        public Type SectionType { get; private set; }
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ConfiguratorException(Type sectionType)
        {
            SectionType = sectionType;
        }

        public ConfiguratorException(Type sectionType, string message) : base(message)
        {
        }

        public ConfiguratorException(Type sectionType, string message, Exception inner) : base(message, inner)
        {
        }

        protected ConfiguratorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {

        }
    }
}