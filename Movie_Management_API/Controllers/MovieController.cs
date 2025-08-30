using Microsoft.AspNetCore.Mvc;
using Movie_Management_API.Model;
using Movie_Management_API.Services;
using System;
using System.Collections.Generic;

namespace Movie_Management_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Get all movies from the database with pagination.
        /// </summary>
        /// <param name="pageNumber">Page number (default = 1)</param>
        /// <param name="pageSize">Number of items per page (default = 10)</param>
        /// <returns>Paged list of movies</returns>
        [HttpGet("GetAllMovies")]
        public ActionResult GetAllMovies(int pageNumber = 1, int pageSize = 10)
        {
            var movies = _movieService.GetAllMovies();

            if (movies == null || movies.Count == 0)
                return NotFound("No movies available.");

            var pagedMovies = movies
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                pageNumber,
                pageSize,
                totalRecords = movies.Count,
                totalPages = (int)Math.Ceiling(movies.Count / (double)pageSize),
                data = pagedMovies
            };

            return Ok(response);
        }


        /// <summary>
        /// Get a movie by its ID.
        /// </summary>
        /// <param name="id">The ID of the movie</param>
        /// <returns>The movie with the specified ID</returns>
        [HttpGet("GetMoviesBy{id}")]
        public ActionResult<MoviesModel> GetMovieById(int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null) return NotFound("Movie not found.");
            return Ok(movie);
        }

        /// <summary>
        /// Add a new movie to the database.
        /// </summary>
        /// <param name="movie">Movie object containing title, director, release year, genre, and rating</param>
        /// <returns>Success or failure message</returns>
        [HttpPost("AddMovies")]
        public ActionResult AddMovie([FromBody] MoviesInsertModel movie)
        {
            if (_movieService.AddMovie(movie))
            {
                return Ok("Movie added successfully.");
            }
            return BadRequest("Failed to add movie.");
        }

        /// <summary>
        /// Update an existing movie.
        /// </summary>
        /// <param name="id">The ID of the movie to update</param>
        /// <param name="movie">Movie object with updated values (only changed fields are required)</param>
        /// <returns>Success or failure message</returns>
        [HttpPut("(UpdateMovie){id}")]
        public ActionResult UpdateMovie(int id, [FromBody] MoviesModel movie)
        {
            movie.nMovieId = id;
            if (_movieService.UpdateMovie(movie))
            {
                return Ok("Movie updated successfully.");
            }
            return BadRequest("Failed to update movie.");
        }

        /// <summary>
        /// Delete a movie by its ID.
        /// </summary>
        /// <param name="id">The ID of the movie to delete</param>
        /// <returns>Success or failure message</returns>
        [HttpDelete("DeleteMovie{id}")]
        public ActionResult DeleteMovie(int id)
        {
            if (_movieService.DeleteMovie(id))
            {
                return Ok("Movie deleted successfully.");
            }
            return BadRequest("Failed to delete movie.");
        }
    }
}
