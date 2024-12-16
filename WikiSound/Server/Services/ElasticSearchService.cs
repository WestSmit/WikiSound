using Nest;
using WikiSound.Server.DTOs;

namespace WikiSound.Server.Services
{
    internal class ElasticSearchService : IElasticSearchService
    {
        private readonly IElasticClient _client;

        public ElasticSearchService(IElasticClient client)
        {
            _client = client;
        }

        public async Task<CreateIndexResponse> CreateArtistsIndex()
        {
            return await _client.Indices.CreateAsync("artists", i => i
                .Map<ArtistForElasticSearch>(mm => mm
                    .AutoMap()
                    .Properties(p => p
                        .Completion(t => t
                            .Name(n => n.SuggestName)
                        )
                        //phrase suggestion 
                        .Text(t => t
                            .Name(n => n.Name)
                        )
                )));
        }

        public async Task<BulkResponse> IndexAllArtists(IEnumerable<ArtistForElasticSearch> artists)
        {
            return await _client.IndexManyAsync(artists, "artists");
        }

        public async Task<bool> IndexExists(string indexName)
        {
            return (await _client.Indices.ExistsAsync(indexName)).Exists;
        }

        public async Task<IEnumerable<ArtistForElasticSearch>> GetArtistsSuggestions(string searchString)
        {
            var response = await _client.SearchAsync<ArtistForElasticSearch>(s => s
                .Index("artists")
                .Suggest(su => su
                    .Completion("artist-suggest", cs => cs
                        .Field(f => f.SuggestName)
                        .Prefix(searchString)
                        .Fuzzy(f => f
                            .Fuzziness(Fuzziness.Auto)
                           // min length of not changed input
                           .PrefixLength(4)
                        )
                        .SkipDuplicates(true)
                        .Size(6)
                    )
                )
            );

            return response.Suggest.Values
                .SelectMany(x => x)
                .SelectMany(x => x.Options)
                .OrderByDescending(x => x.Score)
                .Select(x => x.Source)
                .DistinctBy(x => x.Id);
        }
    }
}
