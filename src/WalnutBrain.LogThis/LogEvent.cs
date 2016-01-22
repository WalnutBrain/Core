using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace WalnutBrain.LogThis
{
    public class LogEvent
    {
        internal LogEvent(LogLevel level)
        {
            Level = level;
        }

        public LogLevel Level { get; set; }
        public string Message { get; internal set; }

        public Exception Exception { get; internal set; }

        public IDictionary<string, object> Parameters { get; } = new Dictionary<string, object>();
    }
}