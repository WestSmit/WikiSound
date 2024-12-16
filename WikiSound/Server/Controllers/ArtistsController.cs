using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WikiSound.Server.Repositories;
using WikiSound.Server.Services;
using WikiSound.Shared;
using WikiSound.Shared.Search;

namespace WikiSound.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistRepository _artistRepo;
        private readonly IMapper _mapper;
        private readonly IElasticSearchService _elastic;

        public ArtistsController(IArtistRepository artistRepo, IMapper mapper, IElasticSearchService elastic) 
        {
            _artistRepo = artistRepo;
            _mapper = mapper;
            _elastic = elastic;
        }

        [HttpGet]
        [Route("page")]
        public Page<ArtistListPageItem> GetArtists(int pageNumber)
        {
            const int pageSize = 10;

            var artists = _artistRepo.GetArtistsForPage(out var count, pageNumber, pageSize).ToList();

            var items = _mapper.Map<List<ArtistListPageItem>>(artists);

            return new Page<ArtistListPageItem>(items, count, pageNumber, pageSize);
        }

        [HttpGet]
        [Route("details")]
        public ActionResult<ArtistDetails> GetArtist(int id)
        {
            var artist = _artistRepo.GetArtistById(id);

            if (artist is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ArtistDetails>(artist));
        }


        [HttpGet]
        [Route("suggest")]
        public async Task<IEnumerable<SuggestionArtistItem>> GetSuggestions(string searchString)
        {
            return _mapper.Map<IEnumerable<SuggestionArtistItem>>(await _elastic.GetArtistsSuggestions(searchString));
        }

        [HttpGet]
        [Route("toptracks")]
        public IEnumerable<TrackListItem> GetArtistTopTracks(int artistId)
        {
            return _mapper.Map<IEnumerable<TrackListItem>>( _artistRepo.GetArtistTopTracks(artistId));
        }
    }
}
