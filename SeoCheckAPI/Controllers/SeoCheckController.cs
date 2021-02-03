using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeoCheckAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeoCheckAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeoCheckController : ControllerBase
    {
        private readonly ILogger<SeoCheckController> _logger;
        private readonly IRankedUrlDto _rankedUrlDto;
        private readonly IWebScraper _webScraper;
        private readonly ISeoCheckConfig _seoCheckConfig;

        public SeoCheckController(ILogger<SeoCheckController> logger
                                                                , IRankedUrlDto rankedUrlDto
                                                                    , IWebScraper webScraper,
                                                                        ISeoCheckConfig seoCheckConfig)
        {
            _logger = logger;
            _rankedUrlDto = rankedUrlDto;
            _webScraper = webScraper;
            _seoCheckConfig = seoCheckConfig;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("This API call needs two parameters: target url and keywords. Example: https://localhost:44314/seocheck/{target url}/{keywords}");
        }

        [HttpGet("{url}/{keywords}")]
        public IActionResult Get(string url, string keywords)
        {
            try
            {
                var list = new Dictionary<int, string>();

                list = _webScraper.ScrapeIt(url, keywords);

                var maxResults = Convert.ToInt32(_seoCheckConfig.GetMaxResults());
                int[] occurrencesOfTargetUrl = list.Where(l => l.Value.Contains(url))
                                                   .Where(l => l.Key <= maxResults)
                                                   .Select(l => l.Key).ToArray();

                if (occurrencesOfTargetUrl.Length == 0)
                    return NotFound();

                _rankedUrlDto.Url = url;
                _rankedUrlDto.ListOfRanks = String.Join(", ", occurrencesOfTargetUrl);
                _rankedUrlDto.TotalOccurrences = occurrencesOfTargetUrl.Length.ToString();
                _rankedUrlDto.KeywordsUsed = keywords;

                return Ok(_rankedUrlDto);
            }
            catch
            {
                return StatusCode(500);
            }
        }


    }

}

