using WikiSound.Server.Models;

namespace WikiSound.Server.Repositories
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetAllArtists();
        IEnumerable<Artist> GetArtistsForPage(out int count, int pageNumber, int pageSize);
        Artist? GetArtistById(int id);
        IEnumerable<Track> GetArtistTopTracks(int artistId, int count = 10);
        Album? GetAlbumById(int id);
        Track? GetTrackById(int id);

    }
}
