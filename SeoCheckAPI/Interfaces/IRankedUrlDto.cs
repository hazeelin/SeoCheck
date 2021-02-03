namespace SeoCheckAPI.Interfaces
{
    public interface IRankedUrlDto
    {
        string KeywordsUsed { get; set; }
        string ListOfRanks { get; set; }
        string TotalOccurrences { get; set; }
        string Url { get; set; }
    }
}