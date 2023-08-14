using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Learn_UnitTesting
{
    public interface IConfigurationProvider
    {
        string GetConnectingString();
    }

    public class ConfigurationProvider : IConfigurationProvider
    {
        public string GetConnectingString()
        {
            var ConfigPath = Path.Combine(Environment.CurrentDirectory, "appsetting.json");
            var ConfigAllText = File.ReadAllText(ConfigPath);
            var ConfigModel = JsonSerializer.Deserialize<ConfigModel>(ConfigAllText);
            var ConnectingString = string.Format(ConfigModel.ConnectingString, Path.Combine(Environment.CurrentDirectory, "Database.mdf"));
            return ConnectingString;
        }
    }
}
