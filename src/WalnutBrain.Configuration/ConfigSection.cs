using System.Reflection;
using JsonDiffPatch;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WalnutBrain.Configuration
{
    public abstract class ConfigSectionBase
    {
        protected ConfigSectionBase(string name, IStringStore store)
        {
            Name = name;
            Store = store;
            Reload();
        }

        internal void Reload()
        {
            ValueBase = JToken.Parse(Store.Read());
        }

        public string Name { get; }

        internal IStringStore Store { get; }

        internal protected JToken ValueBase { get; private set; }
    }

    public class ConfigSection<T> : ConfigSectionBase
    {
        public ConfigSection(string name, IStringStore store) : base(name, store)
        {
        }

        public ConfigSection(string name, T value, IStringStore store = null) : base(name, store ?? new FakeStringStore(JsonConvert.SerializeObject(value, Formatting.Indented)))
        {
        }

        public T Value => ValueBase.ToObject<T>();
    }

    public class ConfigSectionPatcher
    {
        private JsonPatcher _patcher = new JsonPatcher();

        public void Patch(ConfigSectionBase configSection, string patch)
        {
            var patchDoc = PatchDocument.Parse(patch);
            JToken token = configSection.ValueBase;
            _patcher.Patch(ref token, patchDoc);
            configSection.Store.Write(token.ToString(Formatting.Indented));
            configSection.Reload();
        }
    }

    public interface IStringStore
    {
        string Read();
        void Write(string toWrite);
    }

    public class FakeStringStore : IStringStore
    {
        public FakeStringStore(string buffer)
        {
            _buffer = buffer;
        }

        public FakeStringStore()
        {
            _buffer = "{}";
        }

        private string _buffer;

        public string Read()
        {
            return _buffer;
        }

        public void Write(string toWrite)
        {
            _buffer = toWrite;
        }
    }

    


}