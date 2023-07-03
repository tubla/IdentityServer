using Movies.Client.Models;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        public MovieApiService()
        {
            
        }
        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> GetMovie(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return new List<Movie>
            {
                new Movie()
                {
                    Id = 1,
                    Title ="Test Movie",
                    ReleaseDate = DateTime.Now,
                    Rating = "5",
                    Genre ="Sci Fi",
                    ImageUrl = "image1.png",
                    Owner = "kyc"
                    
                }
            };
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
