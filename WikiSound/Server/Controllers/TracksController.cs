using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WikiSound.Server.Repositories;
using WikiSound.Server.Services;
using WikiSound.Shared;
using WikiSound.Shared.Search;

namespace WikiSound.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly IArtistRepository _artistRepo;
        private readonly IMapper _mapper;
        private readonly IElasticSearchService _elastic;

        public TracksController(IArtistRepository artistRepo, IMapper mapper, IElasticSearchService elastic) 
        {
            _artistRepo = artistRepo;
            _mapper = mapper;
            _elastic = elastic;
        }

        [HttpGet]
        [Route("details")]
        public ActionResult<AlbumDetails> GetAlbum(int id)
        {
            var track = _artistRepo.GetTrackById(id);

            if (track is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TrackDetails>(track));
        }
    }
}
