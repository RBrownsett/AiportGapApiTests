using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiportGapApiTests.Utilities
{
    public interface IConfigHelper
    {
        public string GetBaseUrl();

        public int GetTimeOut();
    }

    public class ConfigHelper : IConfigHelper
    {
        public ExternalConnections ExternalConnections { get;}

        public ConfigHelper(IConfiguration configuration)
        {
            ExternalConnections = configuration.GetSection(nameof(ExternalConnections)).Get<ExternalConnections>();

        }

        string IConfigHelper.GetBaseUrl()
        {
            return ExternalConnections.BaseUrl;
        }

        int IConfigHelper.GetTimeOut()
        {
            return ExternalConnections.Timeout;
        }
    }
}
