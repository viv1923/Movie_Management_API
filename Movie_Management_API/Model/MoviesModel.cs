namespace Movie_Management_API.Model
{
    public class MoviesModel
    {

        public int nMovieId { get; set; }
        public string? cTitle { get; set; }
        public string? cDirector { get; set; }
        public int? nReleaseYear { get; set; }
        public string? cGenre { get; set; }
        public int? nRating { get; set; }
    }

    public class MoviesInsertModel
    {
        public string cTitle { get; set; }
        public string cDirector { get; set; }
        public int nReleaseYear { get; set; }
        public string cGenre { get; set; }
        public int nRating { get; set; }
    }
}
