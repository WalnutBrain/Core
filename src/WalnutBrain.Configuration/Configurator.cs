using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WalnutBrain.Configuration
{
    public class Configurator : IConfigurator
    {
        private Configurator()
        {
            
        }

        public static string AppDataRoot =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Assembly.GetEntryAssembly().GetName().Name);

        public static string AppRootDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().CodeBase);

        public static string UserAppData => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData",
            Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().GetName().Name));

        public static Func<Type, string>[] UserProbes = 
            {
                t => Path.Combine(UserAppData, t.FullName + ".wbcfg"),
                t => Path.Combine(UserAppData, t.Assembly.GetName().Name + "." + t.FullName + ".wbcfg"),
            };

        public Func<Type, string>[] SystemProbes  = 
            {
                t => Path.Combine(AppRootDir, ConfigFolder, t.FullName + ".wbcfg"),
                t => Path.Combine(AppRootDir, ConfigFolder, t.Assembly.GetName().Name + "." + t.FullName + ".wbcfg"),
                t => Path.Combine(AppRootDir, t.FullName + ".wbcfg"),
                t => Path.Combine(AppRootDir, t.Assembly.GetName().Name + "." + t.FullName + ".wbcfg"),
                t => Path.Combine(AppDataRoot, ConfigFolder, t.FullName + ".wbcfg"),
                t => Path.Combine(AppDataRoot, ConfigFolder, t.Assembly.GetName().Name + "." + t.FullName + ".wbcfg"),
                t => Path.Combine(AppDataRoot, t.FullName + ".wbcfg"),
                t => Path.Combine(AppDataRoot, t.Assembly.GetName().Name + "." + t.FullName + ".wbcfg")
            };

        public static string ConfigFolder { get; private set; } = "Configs";


        IConfigurator IConfigurator.Add<T>(bool isUser)
        {
            var type = typeof (T);
            var useProbes = isUser ? UserProbes : SystemProbes;
            var fileName = useProbes.Select(p => p(type)).FirstOrDefault(File.Exists);
            if(fileName == null)
                throw new ConfiguratorException(type, "No file configuration found");
            return ((IConfigurator) this).Add<T>(fileName);
        }

        IConfigurator IConfigurator.Add<T>(string filePath) 
        {
            var entryData = ReadEntryData<T>(filePath);
            Sections.TryAdd(typeof(T), entryData);
            return this;
        }


        T IConfigurator.Get<T>()
        {
            EntryData data;
            if(!Sections.TryGetValue(typeof(T), out data))
                throw new ConfiguratorException(typeof(T), "Cannot find configuration section {0}".AsFormat(typeof(T)));
            return (T) data.Deserialized;
        }


        public IConfigurator Add<T>(bool isUser = false) where T : ICfgSection
        {
            return ((IConfigurator) Conf).Add<T>(isUser);
        }

        public static IConfigurator Add<T>(string filePath) where T : ICfgSection
        {
            return ((IConfigurator) Conf).Add<T>(filePath);
        }




        private static EntryData ReadEntryData<T>(string filePath) where T : ICfgSection
        {
            string json;
            try
            {
                json = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                throw new ConfiguratorException(typeof (T), "Cannot read file {0}".AsFormat(filePath), ex);
            }

            T obj;
            try
            {
                obj = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw new ConfiguratorException(typeof (T), "Error reading json in file {}".AsFormat(filePath), ex);
            }
            var entryData = new EntryData(json, obj, filePath);
            return entryData;
        }

        private static readonly Configurator Conf = new Configurator();
        private readonly ConcurrentDictionary<Type, EntryData> Sections = new ConcurrentDictionary<Type, EntryData>();

        

        private class EntryData
        {
            public EntryData(string originalJson, ICfgSection deserialized, string filePath)
            {
                OriginalJson = originalJson;
                Deserialized = deserialized;
                FilePath = filePath;
            }

            public string OriginalJson { get; private set; }
            public ICfgSection Deserialized { get; private set; }
            public string FilePath { get; private set; }
        }
    }
}