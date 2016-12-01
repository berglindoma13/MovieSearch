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

        public MovieController(List<Movie> movieList)
        {
            MovieDbFactory.RegisterSettings(new ApiConnectionClass());
            this._movies = new Movies();
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
        }
            
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Movie Search";

            this.View.BackgroundColor = UIColor.White;

            this._yCoord = StartY;

            var prompt = CreatePrompt();

            var nameField = CreateNameField();

            var greetingButton = CreateButton("Get movies");

            ImageDownloader imdown = new ImageDownloader(new StorageClient());

            greetingButton.TouchUpInside += async (sender, args) =>
                {
                    this._movies.AllMovies.Clear();
                    nameField.ResignFirstResponder();
                    greetingButton.Enabled = false;
                    
                    //create the spinner whilst finding movies
                    var spinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			//THIS SHIT --->
                    spinner.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
                    this.View.AddSubview(spinner);
                    spinner.StartAnimating();

                    var movieApi = MovieDbFactory.Create<DM.MovieApi.MovieDb.Movies.IApiMovieRequest>().Value;

                    DM.MovieApi.ApiResponse.ApiSearchResponse<DM.MovieApi.MovieDb.Movies.MovieInfo> response = await movieApi.SearchByTitleAsync(nameField.Text);

                    foreach (var i in response.Results)
                    {
                        ApiQueryResponse<MovieCredit> resp = await movieApi.GetCreditsAsync(i.Id);

						List<string> actors = new List<string>();
						List<string> genere = new List<string>();
                        //string[] actor = new string[3];

                        var posterlink = i.PosterPath;

                        var localFilePath = imdown.LocalPathForFilename(posterlink);

                        var poster = imdown.DownloadImage(posterlink,localFilePath, CancellationToken.None);

						for (int j = 0; (j < resp.Item.CastMembers.Count); j++)
						{
							actors.Add(resp.Item.CastMembers[j].Name);
						}

						for (int j = 0; (j < i.Genres.Count); j++)
						{
							genere.Add(i.Genres[j].Name);
						}

						var movie = new Movie()
						{
							Title = i.Title,
							Year = i.ReleaseDate.Year,
							ImageName = localFilePath,
							Actors = actors,
							
							Runtime = 0,
							Genre = genere,
							Review = i.Overview
                        };
                        this._movies.AllMovies.Add(movie);
                    }
                    
                    this.NavigationController.PushViewController(new MovieListController(this._movies.AllMovies), true);
                    spinner.StopAnimating();
                    greetingButton.Enabled = true;
                };

            this.View.AddSubview(prompt);
            this.View.AddSubview(nameField);
            this.View.AddSubview(greetingButton);
            
        }

        private UIButton CreateButton(string title)
        {
            var greetingButton = UIButton.FromType(UIButtonType.RoundedRect);
            greetingButton.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2*HorizontalMargin, 50);
            greetingButton.SetTitle(title, UIControlState.Normal);
            this._yCoord += StepY;
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
