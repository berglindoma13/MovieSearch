using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    public class Movies
    {
        private List<Movie> _movies;

        public Movies()
        {
            this._movies = new List<Movie>();
            
        }

        public void addMovie(string title, int year, string imageName)
        {
            var movie = new Movie()
            {
                Title = title,
                Year = year,
                ImageName = imageName
            };

            this._movies.Add(movie);
        }

        public List<Movie> AllMovies => this._movies;
    }

}
