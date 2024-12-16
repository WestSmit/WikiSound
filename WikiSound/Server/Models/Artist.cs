using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WikiSound.Server.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Track> Tracks { get; set; }
        public virtual IEnumerable<Album> Albums { get; set; }
        public string? ArtistStripped { get; set; }
        public string? ArtistAlternate { get; set; }
        public string? Label { get; set; }
        public int? LabelId { get; set; }
        public int? FormedYear { get; set; }
        public int? BornYear { get; set; }
        public int? DiedYear { get; set; }
        public string? Disbanded { get; set; }
        public string? Style { get; set; }
        public string Genre { get; set; }
        public string? Mood { get; set; }
        public string? Website { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? BiographyEN { get; set; }
        public string? BiographyDE { get; set; }
        public string? BiographyFR { get; set; }
        public string? BiographyCN { get; set; }
        public string? BiographyIT { get; set; }
        public string? BiographyJP { get; set; }
        public string? BiographyRU { get; set; }
        public string? BiographyES { get; set; }
        public string? BiographyPT { get; set; }
        public string? BiographySE { get; set; }
        public string? BiographyNL { get; set; }
        public string? BiographyHU { get; set; }
        public string? BiographyNO { get; set; }
        public string? BiographyIL { get; set; }
        public string? BiographyPL { get; set; }
        public string? Gender { get; set; }
        public int? Members { get; set; }
        public string? Country { get; set; }
        public string CountryCode { get; set; }
        public string? ArtistThumb { get; set; }
        public string? ArtistLogo { get; set; }
        public string? ArtistCutout { get; set; }
        public string? ArtistClearart { get; set; }
        public string? ArtistWideThumb { get; set; }
        public string? ArtistFanart { get; set; }
        public string? ArtistFanart2 { get; set; }
        public string? ArtistFanart3 { get; set; }
        public string? ArtistFanart4 { get; set; }
        public string? ArtistBanner { get; set; }
        public string MusicBrainzID { get; set; }
        public string? ISNIcode { get; set; }
        public string? LastFMChart { get; set; }
        public int Charted { get; set; }
        public string Locked { get; set; }
    }
}
