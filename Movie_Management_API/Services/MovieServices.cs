using System;
using System.Collections.Generic;
using Movie_Management_API.DAOs;
using Movie_Management_API.Model;

namespace Movie_Management_API.Services
{
    public class MovieService
    {
        private readonly InterfaceMovieDAO                                                                           _movieDAO;

        public MovieService(InterfaceMovieDAO movieDAO)
        {
            _movieDAO = movieDAO;
        }

        public List<MoviesModel> GetAllMovies()
        {
            return _movieDAO.GetAllMovies();
        }

        public MoviesModel? GetMovieById(int id)
        {
            return _movieDAO.GetMovieById(id);
        }

        public bool AddMovie(MoviesInsertModel movie)
        {
            if (string.IsNullOrWhiteSpace(movie.cTitle))
            {
                throw new ArgumentException("Movie title is required.");
            }

            string result = _movieDAO.AddMovie(movie) ;
            return result.StartsWith("Success");

        }

        public bool UpdateMovie(MoviesModel movie)
        {
            if (movie.nMovieId <= 0)
            {
                throw new ArgumentException("Invalid movie ID.");
            }


            string result =  _movieDAO.UpdateMovie(movie);
            return result.StartsWith("Success");
        }

        public bool DeleteMovie(int id)
        {
            string result = _movieDAO.DeleteMovie(id);
            return result.StartsWith("Success");
        }

    }
}
