using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    public class Movie
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public string ImageName { get; set; }

		public List<string> Actors { get; set; }

		public int Runtime { get; set; }
		public List<string> Genre { get; set; }
		public string Review { get; set; }
    }
}
