using System.Collections.Generic;

namespace SeoCheckAPI.Interfaces
{
    public interface IWebScraper
    {
        public Dictionary<int, string> ScrapeIt(string url, string keywords);
    }
}