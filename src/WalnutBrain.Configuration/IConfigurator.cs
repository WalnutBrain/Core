using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Dynamic;

namespace WalnutBrain.Configuration
{
    public interface IConfigurator
    {
        IConfigurator Register<T>(string name, Uri uri = null);
        ConfiguratorSectionBase<T> Get<T>(string name);
        

        //IConfigurator Patch<T>(string patch) where T : ICfgSection;
    }
}
