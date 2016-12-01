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

        public void addMovie(int id, string title, int year, string imageName, string actors, int runtime, List<string> genre, string review)
        {
			var movie = new Movie()
			{
                Id = id,
                Title = title,
				Year = year,
				ImageName = imageName,
				Actors = actors,
				Runtime = runtime,
				Genre = genre,
				Review = review
            };

            this._movies.Add(movie);
        }

        public List<Movie> AllMovies => this._movies;
    }

}
