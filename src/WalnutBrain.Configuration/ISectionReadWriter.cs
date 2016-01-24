using System;

namespace WalnutBrain.Configuration
{
    public interface ISectionReadWriter
    {
        string Read(Uri source);
        bool CanWrite(Uri target);
        void Write(Uri target, string json);
    }

    public interface ISectionReadWriterDriver
    {
        string Schema { get; }
        bool CanProcess(Uri target);
    }


}