using System;
using System.Configuration;
using System.IO;
using JsonDiffPatch;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WalnutBrain.Configuration
{
    public abstract class ConfiguratorSectionBase<T>
    {
        public string Name { get; }

        private T Original { get; }

        protected ConfiguratorSectionBase(string name, string json)
        {
            Name = name;
            Original = JsonConvert.DeserializeObject<T>(json);
            InternalValue = JsonConvert.DeserializeObject<T>(json);
        }

        public T InternalValue { get; set; }

        public string Diff()
        {
            var patch = new JsonDiffer().Diff(JToken.FromObject(Original), JToken.FromObject(InternalValue), false);
            if (patch.Operations.Count == 0) return null;
            return patch.ToString(Formatting.Indented);
        }

        protected void ApplyPatch(string patch)
        {
            var patchDoc = PatchDocument.Parse(patch);
            var patcher = new JsonPatcher();
            var jo = JToken.FromObject(InternalValue);
            patcher.Patch(ref jo, patchDoc);
            InternalValue = jo.ToObject<T>();
        }

        public override string ToString()
        {
            return JToken.FromObject(InternalValue).ToString(Formatting.Indented);
        }
    }

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