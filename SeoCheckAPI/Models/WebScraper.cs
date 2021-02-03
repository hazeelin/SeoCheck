using SeoCheckAPI.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SeoCheckAPI.Models
{
    public class WebScraper : IWebScraper
    {
        private readonly ISeoCheckConfig _seoCheckConfig;

        StringBuilder _sb;

        public WebScraper(ISeoCheckConfig seoCheckConfig)
        {
            _seoCheckConfig = seoCheckConfig;
        }


        private void GetHtml(string keywords)
        {
            var newEngineUrl = _seoCheckConfig.GetBaseUrl() + _seoCheckConfig.GetMaxResults() + "&q=" + keywords.Replace(" ", "+");
            WebRequest req = WebRequest.Create(newEngineUrl);
            _sb = new System.Text.StringBuilder();

            using (var stream = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                string line;

                while ((line = stream.ReadLine()) != null)
                {
                    if (line.Length > 0)
                        _sb.Append(line);
                }

                stream.Close();
            };

        }

        public Dictionary<int, string> ScrapeIt(string url, string keywords)
        {
            if (!(url == null && keywords == null))
            {
                GetHtml(keywords);

                // Note that this regex search is not ideal 
                // as I couldn't find the perfect search yet for Google yet!
                Regex regex = new Regex(@"https://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?");

                MatchCollection matches = regex.Matches(_sb.ToString());

                var counter = 0;

                Dictionary<int, string> resultList = new Dictionary<int, string>();

                foreach (Match match in matches)
                {
                    if (!match.Value.Contains("google.co"))
                    {
                        counter += 1;

                        resultList.Add(counter, match.Value);
                    }
                }

                return resultList;
            }
            return null;
        }
    }
}
