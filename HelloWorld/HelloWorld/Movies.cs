using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    public class Movies
    {
        private List<string> _movies;

        public Movies()
        {
            this._movies = new List<string>(){};
        }

        public List<string> AllMovies => this._movies;
    }

}
