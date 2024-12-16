using AutoMapper;
using Nest;
using Newtonsoft.Json;
using WikiSound.Server.DTOs;
using WikiSound.Server.Models;
using WikiSound.Server.Services;
using WikiSound.Shared.Search;

namespace WikiSound.Server.Database
{
    internal class Seed
    {
        private readonly ApplicationContext _context;
        private readonly IElasticSearchService _elastic;
        private readonly IMapper _mapper;

        public Seed(ApplicationContext context, IElasticSearchService elastic, IMapper mapper)
        {
            _context = context;
            _elastic = elastic;
            _mapper = mapper;
        }

        public async Task SeedData()
        {

            if (!_context.Artists.Any())
            {
                var artistsFileData = File.ReadAllText("Database/SeedData/Artists.json");
                var artists = JsonConvert.DeserializeObject<Artist[]>(artistsFileData);

                var albumsFileData = File.ReadAllText("Database/SeedData/Albums.json");
                var albums = JsonConvert.DeserializeObject<Album[]>(albumsFileData);

                var tracksFileData = File.ReadAllText("Database/SeedData/Tracks.json");
                var tracks = JsonConvert.DeserializeObject<Track[]>(tracksFileData);

                foreach (var track in tracks)
                {
                    track.Artist = artists.Single(a => a.Id == track.ArtistId);
                }

                foreach (var album in albums)
                {
                    album.Tracks = tracks.Where(t => t.AlbumId == album.Id).ToList();
                }

                foreach (var artist in artists)
                {
                    artist.Albums = albums.Where(a => a.ArtistId == artist.Id).ToList();
                }

                if (artists.Any())
                {
                    _context.Artists.AddRange(artists);
                    _context.SaveChanges();
                }
            }

            var exist = await _elastic.IndexExists("artists");

            if (!exist)
            {
                var res = await _elastic.CreateArtistsIndex();

                var artists = _mapper.Map<ArtistForElasticSearch[]>(_context.Artists.ToArray());

                _elastic.IndexAllArtists(artists).Wait();
            }
        }

    }
}
