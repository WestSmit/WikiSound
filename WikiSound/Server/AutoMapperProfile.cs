using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using WikiSound.Server.Configurations;
using WikiSound.Server.DTOs;
using WikiSound.Server.Models;
using WikiSound.Shared.Search;

namespace WikiSound.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Artist, Shared.ArtistListPageItem>();
            CreateMap<Models.Artist, Shared.ArtistDetails>();
            CreateMap<Models.Artist, Shared.Search.SuggestionArtistItem>();

            CreateMap<Models.Artist, ArtistForElasticSearch>();
            CreateMap<ArtistForElasticSearch, SuggestionArtistItem>();

            CreateMap<Models.Album, Shared.ArtistDetails.AlbumListItem>();

            CreateMap<Models.Album, Shared.AlbumDetails>()
                .ForMember(x => x.ArtistName, opt => opt.MapFrom(x => x.Artist.Name));

            CreateMap<Models.Track, Shared.TrackListItem>();

            CreateMap<Models.Track, Shared.TrackDetails>()
                .ForMember(x => x.AlbumName, opt => opt.MapFrom(x => x.Album.Name))
                .ForMember(x => x.ArtistName, opt => opt.MapFrom(x => x.Artist.Name));

            CreateMap<JwtTokenConfiguration, TokenValidationParameters>();
        }

    }
}
