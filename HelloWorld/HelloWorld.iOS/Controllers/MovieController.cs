using System.Collections.Generic;
using System.Threading;
using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieDownload;
using UIKit;

namespace HelloWorld.iOS
{
    public class MovieController : UIViewController
    {
        private const int HorizontalMargin = 20;
        private const int StartY = 80;
        private const int StepY = 50;
        private int _yCoord;
        private Movies _movies;
        private SetMovieInfo setMovieInfo;

        public MovieController(List<Movie> movieList)
        {
            MovieDbFactory.RegisterSettings(new ApiConnectionClass());
            this._movies = new Movies();
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
            
            setMovieInfo = new SetMovieInfo();
        }
            
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Movie Search";

			this.View.BackgroundColor = UIColor.FromRGB(0.714f, 0.98f, 1);

            this._yCoord = StartY;

            var prompt = CreatePrompt();

            var nameField = CreateNameField();

            var greetingButton = CreateButton("Get movies");

            greetingButton.TouchUpInside += async (sender, args) =>
                {
					if (nameField.Text != "")
					{
						this._movies.AllMovies.Clear();
						nameField.ResignFirstResponder();
						greetingButton.Enabled = false;

						//create the spinner whilst finding movies
						var spinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);

						spinner.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
						this.View.AddSubview(spinner);
						spinner.StartAnimating();

						var movieApi = MovieDbFactory.Create<DM.MovieApi.MovieDb.Movies.IApiMovieRequest>().Value;
						DM.MovieApi.ApiResponse.ApiSearchResponse<DM.MovieApi.MovieDb.Movies.MovieInfo> response = await movieApi.SearchByTitleAsync(nameField.Text);

						foreach (var i in response.Results)
						{
							var movie = new Movie();
							await setMovieInfo.setInfo(i, movieApi, movie);
							this._movies.AllMovies.Add(movie);
						}

						this.NavigationController.PushViewController(new MovieListController(this._movies.AllMovies), true);
						spinner.StopAnimating();
						greetingButton.Enabled = true;
					}
                    
                };

            this.View.AddSubview(prompt);
            this.View.AddSubview(nameField);
            this.View.AddSubview(greetingButton);
        }

        private UIButton CreateButton(string title)
        {
            var greetingButton = UIButton.FromType(UIButtonType.RoundedRect);
			greetingButton.Frame = new CGRect(HorizontalMargin, this._yCoord+10, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
            greetingButton.SetTitle(title, UIControlState.Normal);

			greetingButton.Layer.CornerRadius = 5f;
			greetingButton.Layer.BorderWidth = 0.5f;
			greetingButton.BackgroundColor = UIColor.FromRGB(0.259f, 0.678f, 0.988f);
			greetingButton.SetTitleColor(UIColor.Black, UIControlState.Normal);

            this._yCoord += StepY + 10;
            return greetingButton;
        }

        private UITextField CreateNameField()
        {
            var nameField = new UITextField()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2*HorizontalMargin, 50),
                BorderStyle = UITextBorderStyle.RoundedRect,
                Placeholder = "Movie title"
            };
            this._yCoord += StepY;
            return nameField;
        }

        private UILabel CreatePrompt()
        {
            var prompt = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50),
                Text = "Enter words in movie title: "
            };
            this._yCoord += StepY;
            return prompt;
        }
    }
}
