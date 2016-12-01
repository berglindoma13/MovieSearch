using DM.MovieApi;

namespace HelloWorld
{
    public class ApiConnectionClass : IMovieDbSettings
    {
        public string ApiKey
        {
            get { return "933832d8d52af17d616ac7be600e4f8c"; }
        }
        public string ApiUrl
        {
            get { return "https://api.themoviedb.org/3/"; }
        }

        public ApiConnectionClass() {}
    }
}

