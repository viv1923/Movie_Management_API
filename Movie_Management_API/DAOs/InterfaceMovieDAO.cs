using Movie_Management_API.Model;

namespace Movie_Management_API.DAOs
{
    public interface InterfaceMovieDAO
    {
        List<MoviesModel> GetAllMovies();
        MoviesModel GetMovieById(int nMovieid);
        string AddMovie(MoviesInsertModel movie);
        string UpdateMovie(MoviesModel movie);
        string DeleteMovie(int nMovieid);
    }
}
