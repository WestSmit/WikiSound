using WikiSound.Shared;
using WikiSound.Shared.Search;

namespace WikiSound.Client.Services
{
    internal interface IArtistsService
    {
        Task<Page<ArtistListPageItem>> GetArtistListPageDataAsync(int page);
        Task<ArtistDetails?> GetArtistDetailsAsync(int id);
        Task<TrackListItem[]> GetArtistTopTracksAsync(int id);
        Task<SuggestionArtistItem[]> GetArtistSearchSaggestionsAsync(string searchString);
        Task<AlbumDetails?> GetAlbumDetailsAsync(int id);
        Task<TrackDetails?> GetTrackDetailsAsync(int id);
    }
}