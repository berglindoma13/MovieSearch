using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch;
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

        public MovieController()
        {
            MovieDbFactory.RegisterSettings(new MyClass());
            this._movies = new Movies();
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


            greetingButton.TouchUpInside += async (sender, args) =>
                {
                    this._movies.AllMovies.Clear();
                    nameField.ResignFirstResponder();
                    greetingButton.Enabled = false;
                    
                    //create the spinner whilst finding movies
                    var spinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
                    spinner.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50);
                    this.View.AddSubview(spinner);
                    spinner.StartAnimating();

                    var movieApi = MovieDbFactory.Create<DM.MovieApi.MovieDb.Movies.IApiMovieRequest>().Value;

                    DM.MovieApi.ApiResponse.ApiSearchResponse<DM.MovieApi.MovieDb.Movies.MovieInfo> response = await movieApi.SearchByTitleAsync(nameField.Text);

                    foreach (var i in response.Results)
                    {
                        ApiQueryResponse<MovieCredit> resp = await movieApi.GetCreditsAsync(i.Id);
                        var actor1 = resp.Item.CastMembers[0].Name;
                        var actor2 = resp.Item.CastMembers[1].Name;
                        var actor3 = resp.Item.CastMembers[2].Name;

                        var movie = new Movie()
                        {
                            Title = i.Title,
                            Year = i.ReleaseDate.Year,
                            ImageName = string.Empty,
                            Actor1 = actor1,
                            Actor2 = actor2,
                            Actor3 = actor3
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
