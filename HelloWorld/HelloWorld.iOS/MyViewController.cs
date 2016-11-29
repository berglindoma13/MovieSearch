using CoreGraphics;
using DM.MovieApi;
using MovieSearch;
using UIKit;

namespace HelloWorld.iOS
{
    public class MyViewController : UIViewController
    {
        private const int HorizontalMargin = 20;

        private const int StartY = 80;

        private const int StepY = 50;

        private int _yCoord;

        private Movies _movies;

        public MyViewController()
        {
            MovieDbFactory.RegisterSettings(new MyClass());
        }
            
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this._movies = new Movies();

            this.Title = "Movie Search";

            this.View.BackgroundColor = UIColor.White;

            this._yCoord = StartY;

            var prompt = CreatePrompt();

            var nameField = CreateNameField();

            var greetingButton = CreateButton("Get movies");

            var greetingLabel = CreateGreetingLabel();

            //var navigateButton = CreateButton("See Movie List");

            greetingButton.TouchUpInside += async (sender, args) =>
                {
                    nameField.ResignFirstResponder();
                    
                    //create the spinner whilst finding movies
                    var spinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.White);
                    spinner.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50);
                    this._yCoord += StepY;
                    //spinner.AutoresizingMask = UIViewAutoresizing.All;
                    this.View.AddSubview(spinner);
                    spinner.StartAnimating();

                    var movieApi = MovieDbFactory.Create<DM.MovieApi.MovieDb.Movies.IApiMovieRequest>().Value;

                    DM.MovieApi.ApiResponse.ApiSearchResponse<DM.MovieApi.MovieDb.Movies.MovieInfo> response = await movieApi.SearchByTitleAsync(nameField.Text);

                    foreach (var i in response.Results)
                    {
                        this._movies.AllMovies.Add(i.Title);
                    }
                    
                    this.NavigationController.PushViewController(new MovieListController(this._movies.AllMovies), true);
                    //greetingLabel.Text = response.Results[0].Title; //results from query

                };

            /*navigateButton.TouchUpInside += (sender, args) =>
            {
                nameField.ResignFirstResponder();
                this.NavigationController.PushViewController(new MovieListController(this._movies.AllMovies), true);
                
            };*/

            this.View.AddSubview(prompt);
            this.View.AddSubview(nameField);
            this.View.AddSubview(greetingButton);
            this.View.AddSubview(greetingLabel);
            //this.View.AddSubview(navigateButton);
        }


        private UILabel CreateGreetingLabel()
        {
            var greetingLabel = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50)
            };
            this._yCoord += StepY;
            return greetingLabel;
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
