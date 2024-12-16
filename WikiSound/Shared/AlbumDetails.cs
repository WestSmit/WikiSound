namespace WikiSound.Shared
{
    public class AlbumDetails
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public TrackListItem[] Tracks { get; set; }
        public int? LabelId { get; set; }
        public string Name { get; set; }
        public string AlbumStripped { get; set; }
        public string? ArtistStripped { get; set; }
        public int YearReleased { get; set; }
        public string? Style { get; set; }
        public string? Genre { get; set; }
        public string? Label { get; set; }
        public string ReleaseFormat { get; set; }
        public int Sales { get; set; }
        public string? AlbumThumb { get; set; }
        public string? AlbumThumbHQ { get; set; }
        public string? AlbumThumbBack { get; set; }
        public string? AlbumCDart { get; set; }
        public string? AlbumSpine { get; set; }
        public string? Album3DCase { get; set; }
        public string? Album3DFlat { get; set; }
        public string? Album3DFace { get; set; }
        public string? Album3DThumb { get; set; }
        public string? DescriptionEN { get; set; }
        public string? DescriptionDE { get; set; }
        public string? DescriptionFR { get; set; }
        public string? DescriptionCN { get; set; }
        public string? DescriptionIT { get; set; }
        public string? DescriptionJP { get; set; }
        public string? DescriptionRU { get; set; }
        public string? DescriptionES { get; set; }
        public string? DescriptionPT { get; set; }
        public string? DescriptionSE { get; set; }
        public string? DescriptionNL { get; set; }
        public string? DescriptionHU { get; set; }
        public string? DescriptionNO { get; set; }
        public string? DescriptionIL { get; set; }
        public string? DescriptionPL { get; set; }
        public int? Loved { get; set; }
        public float? Score { get; set; }
        public int? ScoreVotes { get; set; }
        public string? Review { get; set; }
        public string? Mood { get; set; }
        public string? Theme { get; set; }
        public string? Speed { get; set; }
        public string? Location { get; set; }
        public string MusicBrainzID { get; set; }
        public string MusicBrainzArtistID { get; set; }
        public string? AllMusicID { get; set; }
        public string? BBCReviewID { get; set; }
        public string? RateYourMusicID { get; set; }
        public string? DiscogsID { get; set; }
        public string? WikidataID { get; set; }
        public string? WikipediaID { get; set; }
        public string? GeniusID { get; set; }
        public string? LyricWikiID { get; set; }
        public string? MusicMozID { get; set; }
        public string? ItunesID { get; set; }
        public string? AmazonID { get; set; }
        public string Locked { get; set; }
    }
}
