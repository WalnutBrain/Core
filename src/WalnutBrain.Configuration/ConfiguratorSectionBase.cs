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
        protected ISectionReadWriter ReadWriter { get; }
        public Uri Uri { get; }
        public string Name { get; }

        private T Original { get; }

        protected ConfiguratorSectionBase(string name, ISectionReadWriter readWriter, Uri uri)
        {
            ReadWriter = readWriter;
            Uri = uri;
            Name = name;
            var json = ReadWriter.Read(uri);
            Original = JsonConvert.DeserializeObject<T>(json);
            ValueBase = JsonConvert.DeserializeObject<T>(json);
        }

        protected ConfiguratorSectionBase(string name, T obj, ISectionReadWriter writer, Uri uri)
        {
            ReadWriter = writer;
            Uri = uri;
            Name = name;
            Original = JToken.FromObject(obj).ToObject<T>();
            ValueBase = JToken.FromObject(obj).ToObject<T>();
        }

        protected T ValueBase { get; set; }

        public abstract T Value { get; set; }

        public string Diff()
        {
            var patch = new JsonDiffer().Diff(JToken.FromObject(Original), JToken.FromObject(ValueBase), false);
            if (patch.Operations.Count == 0) return null;
            return patch.ToString(Formatting.Indented);
        }

        protected void ApplyPatch(string patch)
        {
            var patchDoc = PatchDocument.Parse(patch);
            var patcher = new JsonPatcher();
            var jo = JToken.FromObject(ValueBase);
            patcher.Patch(ref jo, patchDoc);
            ValueBase = jo.ToObject<T>();
        }

        public abstract void Patch(string patch);

        protected void WriteBase()
        {
            if(!ReadWriter.CanWrite(Uri))
                throw new ConfiguratorException(typeof(T), "Uri {0} cannot be writed".AsFormat(Uri.ToString()));
            ReadWriter.Write(Uri, this.ToString());
        }

        public abstract void Write();

        public override string ToString()
        {
            return JToken.FromObject(Value).ToString(Formatting.Indented);
        }
    }
}