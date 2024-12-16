using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiSound.Shared.Search
{
    public class SuggestionArtistItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BiographyEN { get; set; }
        public string ArtistLogo { get; set; }
        public string ArtistThumb { get; set; }
    }
}
