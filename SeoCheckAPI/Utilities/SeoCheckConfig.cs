using Microsoft.Extensions.Configuration;
using SeoCheckAPI.Interfaces;

namespace SeoCheckAPI.Utilities
{
    public class SeoCheckConfig : ISeoCheckConfig
    {
        private readonly IConfiguration _configuration;

        public SeoCheckConfig(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public string GetBaseUrl()
        {
            return _configuration.GetValue<string>("SeoCheckAppSettings:BaseUrl")
                    ?? "https://www.google.co.uk/search?num=";
        }

        public string GetMaxResults()
        {
            return _configuration.GetValue<string>("SeoCheckAppSettings:MaxResults")
                    ?? "100";
        }
    }
}
