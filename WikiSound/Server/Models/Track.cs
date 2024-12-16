using System.ComponentModel.DataAnnotations;

namespace WikiSound.Server.Models
{
    public class Track
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public int? Lyric { get; set; }
        public string? IMVDB { get; set; }
        public string Name { get; set; }
        public string? ArtistAlternate { get; set; }
        public int? CD { get; set; }
        public int Duration { get; set; }
        public string? Genre { get; set; }
        public string? Mood { get; set; }
        public string? Style { get; set; }
        public string? Theme { get; set; }
        public string? DescriptionEN { get; set; }
        public string? DescriptionDE { get; set; }
        public string? DescriptionFR { get; set; }
        public string? DescriptionCN { get; set; }
        public string? DescriptionIT { get; set; }
        public string? DescriptionJP { get; set; }
        public string? DescriptionRU { get; set; }
        public string? DescriptionPT { get; set; }
        public string? DescriptionSE { get; set; }
        public string? DescriptionNL { get; set; }
        public string? DescriptionHU { get; set; }
        public string? DescriptionNO { get; set; }
        public string? DescriptionIL { get; set; }
        public string? DescriptionPL { get; set; }
        public string? TrackThumb { get; set; }
        public string? Track3DCase { get; set; }
        public string? TrackLyrics { get; set; }
        public string? MusicVid { get; set; }
        public string? MusicVidDirector { get; set; }
        public string? MusicVidCompany { get; set; }
        public string? MusicVidScreen1 { get; set; }
        public string? MusicVidScreen2 { get; set; }
        public string? MusicVidScreen3 { get; set; }
        public long? MusicVidViews { get; set; }
        public int? MusicVidLikes { get; set; }
        public int? MusicVidDislikes { get; set; }
        public int? MusicVidFavorites { get; set; }
        public int? MusicVidComments { get; set; }
        public int TrackNumber { get; set; }
        public int Loved { get; set; }
        public float? Score { get; set; }
        public int? ScoreVotes { get; set; }
        public int? TotalListeners { get; set; }
        public int? TotalPlays { get; set; }
        public string MusicBrainzID { get; set; }
        public string MusicBrainzAlbumID { get; set; }
        public string MusicBrainzArtistID { get; set; }
        public string Locked { get; set; }
    }
}
