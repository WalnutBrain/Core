using System;
using System.IO;
using System.Security.Policy;
using JsonDiffPatch;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WalnutBrain.Configuration
{
    public class UserConfiguratorSection<T> : ConfiguratorSectionBase<T>
    {
        public UserConfiguratorSection(string name, ISectionReadWriter readWriter, Uri uri) : base(name, readWriter, uri)
        {
            var userJson = readWriter.Read(new UriBuilder("user", name).Uri);
            UserPatch = userJson != null ? PatchDocument.Parse(userJson) : null;
            DoUserPatch();
        }

        private void DoUserPatch()
        {
            if (UserPatch != null)
            {
                var token = JToken.FromObject(ValueBase);
                new JsonPatcher().Patch(ref token, UserPatch);
                Value = token.ToObject<T>();
            }
            else
                Value = JToken.FromObject(ValueBase).ToObject<T>();
        }

        private PatchDocument UserPatch { get; set; }

        public UserConfiguratorSection(string name, T obj, ISectionReadWriter writer, Uri uri) : base(name, obj, writer, uri)
        {
            Value = JToken.FromObject(obj).ToObject<T>();
        }

        public override T Value { get; set; }

        public override void Patch(string patch)
        {
            ApplyPatch(patch);
            DoUserPatch();
        }

        public override void Write()
        {
            WriteBase();
            var differ = new JsonDiffer();
            var patch = differ.Diff(JToken.FromObject(ValueBase), JToken.FromObject(Value), false);
            ReadWriter.Write(new UriBuilder("user", Name).Uri, patch.ToString(Formatting.Indented));
        }
    }
}