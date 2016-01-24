using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WalnutBrain.Configuration
{
    public class ConfigaratorSection<T> : ConfiguratorSectionBase<T>
    {
        public ConfigaratorSection(string name, T obj, ISectionReadWriter writer, Uri uri) : base(name, obj, writer, uri)
        {
        }

        public ConfigaratorSection(string name, ISectionReadWriter readWriter, Uri uri) : base(name, readWriter, uri)
        {
        }

        public override T Value
        {
            get { return ValueBase; }
            set { ValueBase = value; }
        }

        public override void Patch(string patch)
        {
            ApplyPatch(patch);
        }

        public override void Write()
        {
            WriteBase();
        }

    }
}