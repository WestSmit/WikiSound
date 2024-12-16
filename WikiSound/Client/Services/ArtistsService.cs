using WikiSound.Shared;
using static System.Net.Http.Json.HttpClientJsonExtensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components;
using static System.Net.WebRequestMethods;
using WikiSound.Shared.Search;

namespace WikiSound.Client.Services
{
    internal class ArtistsService : BaseHttpService, IArtistsService
    {
        private readonly HttpClient _http;
        public ArtistsService(
            IWebAssemblyHostEnvironment hostEnvironmen,
            IConfiguration config,
            HttpClient http) : base(hostEnvironmen, config)
        {
            _http = http;
        }

        public async Task<Page<ArtistListPageItem>> GetArtistListPageDataAsync(int page)
        {
            var url = new Uri(_apiUrl, $"api/artists/page?pageNumber={page}");

            return await _http.GetFromJsonAsync<Page<ArtistListPageItem>>(url) ?? new Page<ArtistListPageItem>();
        }

        public async Task<ArtistDetails?> GetArtistDetailsAsync(int id)
        {
            var url = new Uri(_apiUrl, $"api/artists/details?id={id}");

            return await _http.GetFromJsonAsync<ArtistDetails>(url);
        }

        public async Task<TrackListItem[]> GetArtistTopTracksAsync(int id)
        {
            var url = new Uri(_apiUrl, $"api/artists/toptracks?artistId={id}");

            return await _http.GetFromJsonAsync<TrackListItem[]>(url);
        }

        public async Task<SuggestionArtistItem[]> GetArtistSearchSaggestionsAsync(string searchString)
        {
            var url = new Uri(_apiUrl, $"api/artists/suggest?searchString={searchString}");

            return await _http.GetFromJsonAsync<SuggestionArtistItem[]>(url);
        }

        public async Task<AlbumDetails?> GetAlbumDetailsAsync(int id)
        {
            var url = new Uri(_apiUrl, $"api/albums/details?id={id}");

            return await _http.GetFromJsonAsync<AlbumDetails>(url);
        }

        public async Task<TrackDetails?> GetTrackDetailsAsync(int id)
        {
            var url = new Uri(_apiUrl, $"api/tracks/details?id={id}");

            return await _http.GetFromJsonAsync<TrackDetails>(url);
        }
    }
}
