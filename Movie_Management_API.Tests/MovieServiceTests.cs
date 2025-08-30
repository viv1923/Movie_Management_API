using Moq;
using Movie_Management_API.DAOs;
using Movie_Management_API.Model;// fixed from Model to Models
using Movie_Management_API.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Movie_Management_API.Tests
{                                                   
    public class MovieServiceTests
    {
        [Fact]
        public void GetAllMovies_ReturnsMovies()
        {
            // setup
            var dao = new Mock<InterfaceMovieDAO>();
            dao.Setup(x => x.GetAllMovies()).Returns(new List<MoviesModel>
            {
                new MoviesModel { nMovieId = 1, cTitle = "Inception" },
                new MoviesModel { nMovieId = 2, cTitle = "big hero", cDirector = "vvk", nReleaseYear = 2025, cGenre = "sad", nRating=12}
            });

            var service = new MovieService(dao.Object);

            // act
            var result = service.GetAllMovies();

            // assert
            Assert.True(result.Count > 0);
            Assert.Equal("Inception", result[0].cTitle);
        }

        [Fact]
        public void GetMovieById_ReturnsCorrectMovie()
        {
            // arrange
            var dao = new Mock<InterfaceMovieDAO>();
            dao.Setup(x => x.GetMovieById(2)).Returns(
                new MoviesModel
                {
                    nMovieId = 2,
                    cTitle = "Big Hero",
                    cDirector = "vvk",
                    nReleaseYear = 2025,
                    cGenre = "sad",
                    nRating = 12
                }
            );

            var service = new MovieService(dao.Object);

            // act
            var result = service.GetMovieById(2);

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.nMovieId);
            Assert.Equal("Big Hero", result.cTitle);
        }


        [Fact]
        public void GetMovieById_ReturnsNull_WhenMovieNotFound()
        {
            // arrange
            var dao = new Mock<InterfaceMovieDAO>();
            dao.Setup(x => x.GetMovieById(99)).Returns((MoviesModel?)null);

            var service = new MovieService(dao.Object);

            // act
            var result = service.GetMovieById(99);

            // assert
            Assert.Null(result);
        }
        [Fact]
        public void AddMovie_ValidMovie_ReturnsTrue()
        {
            // arrange
            var dao = new Mock<InterfaceMovieDAO>();
            var movieToInsert = new MoviesInsertModel
            {
                cTitle = "doomsday",
                cDirector = "panwar",
                nReleaseYear = 2026,
                cGenre = "superhero",
                nRating = 9
            };

            dao.Setup(x => x.AddMovie(movieToInsert)).Returns("Success: Movie inserted");

            var service = new MovieService(dao.Object);

            // act
            var result = service.AddMovie(movieToInsert);

            // assert
            Assert.True(result);
        }

        [Fact]
        public void AddMovie_InvalidMovie_ThrowsException()
        {
            // arrange
            var dao = new Mock<InterfaceMovieDAO>();
            var movieToInsert = new MoviesInsertModel { cTitle = "" }; // empty title

            var service = new MovieService(dao.Object);

            // act & assert
            Assert.Throws<ArgumentException>(() => service.AddMovie(movieToInsert));
        }

        [Fact]
        public void UpdateMovie_ValidMovie_ReturnsTrue()
        {
            // arrange
            var dao = new Mock<InterfaceMovieDAO>();
            var movieToUpdate = new MoviesModel
            {
                nMovieId = 5,
                cTitle = "Titanic",
                cDirector = "James Cameron",
                nReleaseYear = 1997,
                cGenre = "Drama",
                nRating = 10
            };

            dao.Setup(x => x.UpdateMovie(movieToUpdate)).Returns("Success: Movie updated");

            var service = new MovieService(dao.Object);

            // act
            var result = service.UpdateMovie(movieToUpdate);

            // assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateSpecificField_ValidMovie_ReturnsTrue()
        {
            // arrange
            var dao = new Mock<InterfaceMovieDAO>();
            var movieToUpdate = new MoviesModel
            {
                nMovieId = 5,
                cTitle = "Titanic",
                cDirector = "James Cameron"
                //nReleaseYear = 1997,
                //cGenre = "Drama",
                //nRating = 10
            };

            dao.Setup(x => x.UpdateMovie(movieToUpdate)).Returns("Success: Movie updated");

            var service = new MovieService(dao.Object);

            // act
            var result = service.UpdateMovie(movieToUpdate);

            // assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateSpecificFieldINT_ValidMovie_ReturnsTrue()
        {
            // arrange
            var dao = new Mock<InterfaceMovieDAO>();
            var movieToUpdate = new MoviesModel
            {
                nMovieId = 5,
                cTitle = "sunkenTitanic",
                //cDirector = "James Cameron"
                nReleaseYear = null,
                //cGenre = "Drama",
                nRating = null
            };

            dao.Setup(x => x.UpdateMovie(movieToUpdate)).Returns("Success: Movie updated");

            var service = new MovieService(dao.Object);

            // act
            var result = service.UpdateMovie(movieToUpdate);

            // assert
            Assert.True(result);
        }
        [Fact]
        public void DeleteMovie_ValidId_ReturnsTrue()
        {
            // arrange
            var dao = new Mock<InterfaceMovieDAO>();
            int movieId = 10;

            dao.Setup(x => x.DeleteMovie(movieId)).Returns("Success: Movie deleted");

            var service = new MovieService(dao.Object);

            // act
            var result = service.DeleteMovie(movieId);

            // assert
            Assert.True(result);
        }


    }
}
