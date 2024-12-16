using Nest;
using WikiSound.Server.DTOs;
using WikiSound.Server.Models;
using WikiSound.Shared.Search;

namespace WikiSound.Server.Services
{
    public interface IElasticSearchService
    {
        Task<CreateIndexResponse> CreateArtistsIndex();
        Task<bool> IndexExists(string indexName);
        Task<BulkResponse> IndexAllArtists(IEnumerable<ArtistForElasticSearch> artists);
        Task<IEnumerable<ArtistForElasticSearch>> GetArtistsSuggestions(string searchString);
    }
}
