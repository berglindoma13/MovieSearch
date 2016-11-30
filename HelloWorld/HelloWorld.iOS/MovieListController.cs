using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld.iOS
{
    using UIKit;
    public class MovieListController : UITableViewController
    {
        private List<Movie> _movieList;

        public MovieListController(List<Movie> movieList)
        {
            this._movieList = movieList;
        }

        public override void ViewDidLoad()
        {
            this.Title = "Movie List";

            this.View.BackgroundColor = UIColor.White;

            this.TableView.Source = new MovieListSource(this._movieList, OnSelectedMovie);

        }

        private void OnSelectedMovie(int row)
        {
		//THIS SHIT---->
			this.NavigationController.PushViewController(new DetailController(this._movieList[row]), true);
            /*var okAlertController = UIAlertController.Create("Movie selected", this._movieList[row].Title, UIAlertControllerStyle.Alert);

            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            this.PresentViewController(okAlertController, true, null);*/
        }
    }
}
