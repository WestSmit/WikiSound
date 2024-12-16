using Nest;

namespace WikiSound.Server.DTOs
{
    public class ArtistForElasticSearch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArtistAlternate { get; set; }
        public string BiographyEN { get; set; }
        public string ArtistLogo { get; set; }
        public string ArtistThumb { get; set; }
        public CompletionField SuggestName
        {
            get
            {
                return new CompletionField() {
                    Input = new [] { Name, ArtistAlternate },
                    Weight = 100,
                };
            }
        }
    }
}
