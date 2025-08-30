using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Movie_Management_API.Classes;
using Movie_Management_API.Model;


namespace Movie_Management_API.DAOs
{
    public class MovieDAO : InterfaceMovieDAO
    {
        private readonly DatabaseHelper _databaseHelper;

        public MovieDAO(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // Get all movies
        public List<MoviesModel> GetAllMovies()
        {
            var movies = new List<MoviesModel>();

            string str = "select * from GenMovies";
            DataTable dt = _databaseHelper.ExecuteSelect(str);

            foreach (DataRow row in dt.Rows)
            {
                movies.Add(new MoviesModel
                {
                    nMovieId = int.Parse(row["nMovieId"].ToString()),
                    cTitle = row["cTitle"].ToString(),
                    cDirector = row["cDirector"].ToString(),
                    nReleaseYear = Convert.ToInt32(row["nReleaseYear"]),
                    cGenre = row["cGenre"].ToString(),
                    nRating = Convert.ToInt32(row["nRating"])
                });
            }
            return movies;
        }

        // Get movie by Id
        public MoviesModel? GetMovieById(int nMovieid)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@nMovieId", nMovieid)
            };
            string str = "SELECT * FROM GenMovies WHERE nMovieId = @nMovieId";

            DataTable dt = _databaseHelper.ExecuteSelect(str, parameters);

            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];
            return new MoviesModel
            {
                nMovieId = int.Parse(row["nMovieId"].ToString()),
                cTitle = row["cTitle"].ToString(),
                cDirector = row["cDirector"].ToString(),
                nReleaseYear = Convert.ToInt32(row["nReleaseYear"]),
                cGenre = row["cGenre"].ToString(),
                nRating = Convert.ToInt32(row["nRating"])
            };
        }

        // Insert new movie
        public string AddMovie(MoviesInsertModel movie)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@cTitle", movie.cTitle),
                new SqlParameter("@cDirector", movie.cDirector),
                new SqlParameter("@nReleaseYear", movie.nReleaseYear),
                new SqlParameter("@cGenre", movie.cGenre),
                new SqlParameter("@nRating", movie.nRating)
            };

            DataTable dt = _databaseHelper.ExecuteStoredProcedure("sp_AddMovie", parameters);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Message"].ToString() ?? "Unknown reuslt";
            }
            else
            {
                return "No response from database.";
            }
        }

        // Update movie
        public string UpdateMovie(MoviesModel movie)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@nMovieId", movie.nMovieId),

                new SqlParameter("@cTitle", string.IsNullOrWhiteSpace(movie.cTitle)
                    ? (object)DBNull.Value
                    : movie.cTitle),

                new SqlParameter("@cDirector", string.IsNullOrWhiteSpace(movie.cDirector)
                    ? (object)DBNull.Value
                    : movie.cDirector),

                new SqlParameter("@nReleaseYear", movie.nReleaseYear == 0
                    ? (object)DBNull.Value
                    : movie.nReleaseYear),

                new SqlParameter("@cGenre", string.IsNullOrWhiteSpace(movie.cGenre)
                    ? (object)DBNull.Value
                    : movie.cGenre),

                new SqlParameter("@nRating", movie.nRating == 0
                    ? (object)DBNull.Value
                    : movie.nRating)
            };

            DataTable dt = _databaseHelper.ExecuteStoredProcedure("sp_UpdateMovie", parameters);

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["Message"].ToString() ?? "Unknown result";
            else
                return "No response from database.";
        }


        // Delete movie
        public string DeleteMovie(int nMovieid)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@nMovieId", nMovieid)
            };

            DataTable dt = _databaseHelper.ExecuteStoredProcedure("sp_DeleteMovie", parameters);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Message"].ToString() ?? "Unknown result";
            }
            else
            {
                return "No response from database.";
            }
        }
    }
}
