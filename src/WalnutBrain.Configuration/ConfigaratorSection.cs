using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WalnutBrain.Configuration
{
    public class ConfigaratorSection<T> : ConfiguratorSectionBase<T>
    {
        public ConfigaratorSection(string name, T obj) : base(name, JToken.FromObject(obj).ToString(Formatting.Indented))
        {
        }

        public ConfigaratorSection(string name, string json) : base(name, json)
        {
        }

        public T Value
        {
            get { return InternalValue; }
            set { InternalValue = value; }
        }

        public void Patch(string patch)
        {
            ApplyPatch(patch);
        }

    }
}