using Microsoft.EntityFrameworkCore;
using WikiSound.Server.Database;
using WikiSound.Server.Models;
using Artist = WikiSound.Server.Models.Artist;

namespace WikiSound.Server.Repositories
{
    internal class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationContext _context;

        public ArtistRepository(ApplicationContext context) 
        {
            _context = context;
        }

        public IEnumerable<Artist> GetAllArtists()
        {
            return _context.Artists.ToArray();
        }

        public IEnumerable<Artist> GetArtistsForPage(out int count, int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Artist> source = _context.Artists;

            count = source.Count();

            return source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArray();
        }

        public Artist? GetArtistById(int id)
        {
            return _context.Artists
                .Include(x => x.Albums.OrderByDescending(x => x.Score))
                .SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Track> GetArtistTopTracks(int artistId, int count = 10)
        {
            return _context.Tracks
                .Where(x => x.ArtistId == artistId)
                .OrderByDescending(x => x.Score)
                .Take(count);
        }

        public Album? GetAlbumById(int id)
        {
            return _context.Albums
                .Include(x => x.Tracks)
                .Include(x => x.Artist)
                .SingleOrDefault(a => a.Id == id);
        }

        public Track? GetTrackById(int id)
        {
            return _context.Tracks
                .Include(x => x.Album)
                .Include(x => x.Artist)
                .SingleOrDefault(a => a.Id == id);
        }
    }
}
