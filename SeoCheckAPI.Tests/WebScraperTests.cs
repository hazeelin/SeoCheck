using Moq;
using NUnit.Framework;
using SeoCheckAPI.Interfaces;
using SeoCheckAPI.Models;
using System.Collections.Generic;

namespace SeoCheckAPI.Tests
{
    public class WebScraperTests
    {
        [Test]
        public void ScrapeItTest_ResultShouldBeMoreThanZero()
        {
            var _configMock = new Mock<ISeoCheckConfig>();

            _configMock.Setup(c => c.GetBaseUrl()).Returns("https://www.google.co.uk/search?num=");
            _configMock.Setup(c => c.GetMaxResults()).Returns("100");

            var list = new Dictionary<int, string>();

            var ws = new WebScraper(_configMock.Object);

            list = ws.ScrapeIt("www.infotrack.co.uk", "land registry search");

            int result = list.Count;

            Assert.That(result, Is.GreaterThan(0));
        }

    }
}