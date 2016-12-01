using System.Collections.Generic;
using System.Text;
using System.Threading;
using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;

namespace HelloWorld.iOS
{
	using UIKit;
	public class DetailController : UIViewController
	{
		private Movie _movie;
		private const int HorizontalMargin = 20;
		private const int StartY = 80;
		private const int StepY = 30;
		private int _yCoord;

		public DetailController(Movie movie)
		{
			this._movie = movie;
            MovieDbFactory.RegisterSettings(new ApiConnectionClass());
        }

		public override async void ViewDidLoad()
		{
			this.Title = "Movie info";

            var movieApi = MovieDbFactory.Create<DM.MovieApi.MovieDb.Movies.IApiMovieRequest>().Value;

            DM.MovieApi.ApiResponse.ApiQueryResponse<DM.MovieApi.MovieDb.Movies.Movie> response = await movieApi.FindByIdAsync(_movie.Id);

            //list the types of genres the movie falls under
            string genreList;

		    if (response.Item.Genres.Count == 0)
		    {
		        genreList = "";
		    }
		    else
		    {
				genreList = response.Item.Genres[0].Name.ToString();
		    }

		    for (int j = 1; j < response.Item.Genres.Count; j++)
            {
                if (!response.Item.Genres[j].Equals(null))
                {
					genreList += ", " + response.Item.Genres[j].Name.ToString();
                }
            }

            //movie runtime added to instance
            _movie.Runtime = response.Item.Runtime;
            _movie.Review = response.Item.Overview;
            _movie.Genre = genreList;

			this.View.BackgroundColor = UIColor.White;
			this._yCoord = StartY;

			var movieImage = createImage();
			var movieName = createMovieName();
			var details = createDetails();
			var overview = createOverview();

			this.View.AddSubview(movieImage);
			this.View.AddSubview(movieName);
			this.View.AddSubview(details);
			this.View.AddSubview(overview);
		}

		private UIImageView createImage()
		{
			var movieImage = new UIImageView()
			{
				Frame = new CGRect(20, 80, this.View.Bounds.Width - 2*HorizontalMargin, 200),
				Image = UIImage.FromFile(_movie.ImageName)

			};
			this._yCoord += 210;
			return movieImage;
		}

		private UILabel createMovieName()
		{
			var movieName = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50),
				Text = _movie.Title + " (" + _movie.Year + ")"
			};
			this._yCoord += StepY;
			return movieName;
		}

		private UILabel createDetails()
		{
			var details = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50),
				Text = _movie.Runtime.ToString() + " min | " + _movie.Genre
			
			};
			this._yCoord += StepY;
			return details;
		}

		private UILabel createOverview()
		{
			var overview = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 150),
				Text = _movie.Review,
                Font = UIFont.FromName("AmericanTypewriter-Light", 10f),
                Lines = 0
			};
			this._yCoord += StepY;
			return overview;
		}
	}
}