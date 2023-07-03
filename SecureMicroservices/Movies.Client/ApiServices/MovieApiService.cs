using IdentityModel.Client;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MovieApiService(IHttpClientFactory httpFactory)
        {
            _httpClientFactory = httpFactory;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {

            return await Way_1_GetMoviesUsing_IHttpClientFactory();

           //return await Way_2_GetMoviesUsing_HttpClient();
        }

        private async Task<IEnumerable<Movie>> Way_1_GetMoviesUsing_IHttpClientFactory()
        {
            // 1. Create the API client
            var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");

            // 2. Prepare the http get request method
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/movies");

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            // 3. Deserialize response to MovieList

            var content = await response.Content.ReadAsStringAsync();
            var movieList = JsonConvert.DeserializeObject<IEnumerable<Movie>>(content);
            return movieList;
        }

        private async Task<IEnumerable<Movie>> Way_2_GetMoviesUsing_HttpClient()
        {
            // 1. Get token from identity server
            // 2. Send the requet to the API, by adding the token to the header message of the http request
            // 3. Deserialize response to MovieList

            // 1. Get token from IS, the below client details must be registerd on IS.
            var apiClientCredentials = new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5005/connect/token",
                ClientId = "movieClient", // client id of API Client
                ClientSecret = "secret", // API Client Secret

                Scope = "movieAPI" // API Client Scope
            };

            // Create a new HttpClient tot talk to IS(localhost : 5005)

            var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);
            if (tokenResponse.IsError)
            {
                return null;
            }

            // 2. Send request to the API, add access token to the header message

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:5001/api/movies");
            response.EnsureSuccessStatusCode();

            // 3. Deserialize response to MovieList

            var content = await response.Content.ReadAsStringAsync();
            var movieList = JsonConvert.DeserializeObject<IEnumerable<Movie>>(content);
            return movieList;
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

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
