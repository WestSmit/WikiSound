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
    public class AlbumsController : ControllerBase
    {
        private readonly IArtistRepository _artistRepo;
        private readonly IMapper _mapper;
        private readonly IElasticSearchService _elastic;

        public AlbumsController(IArtistRepository artistRepo, IMapper mapper, IElasticSearchService elastic) 
        {
            _artistRepo = artistRepo;
            _mapper = mapper;
            _elastic = elastic;
        }

        [HttpGet]
        [Route("details")]
        public ActionResult<AlbumDetails> GetAlbumDetails(int id)
        {
            var album = _artistRepo.GetAlbumById(id);

            if (album is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AlbumDetails>(album));
        }
    }
}
