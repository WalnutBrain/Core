using System.CodeDom;
using System.Collections.Generic;
using System.Dynamic;

namespace WalnutBrain.Configuration
{
    public interface IConfigurator
    {
        IConfigurator Add<T>(bool isUser = false) where T : ICfgSection;
        IConfigurator Add<T>(string filePath) where T : ICfgSection;
        T Get<T>() where T : ICfgSection;
        //IConfigurator Patch<T>(string patch) where T : ICfgSection;
    }
}
