using SeoCheckAPI.Interfaces;

namespace SeoCheckAPI.Models.Dtos
{
    public class RankedUrlDto : IRankedUrlDto
    {
        public string Url { get; set; }

        public string ListOfRanks { get; set; }

        public string TotalOccurrences { get; set; }

        public string KeywordsUsed { get; set; }
    }
}
