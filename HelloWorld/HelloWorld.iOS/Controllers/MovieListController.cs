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
            this.View.BackgroundColor = UIColor.FromRGB(0.714f, 0.98f, 1);
            this.TableView.Source = new MovieListSource(this._movieList, OnSelectedMovie);
            this.TableView.ReloadData();
        }

        private void OnSelectedMovie(int row)
        {
			this.NavigationController.PushViewController(new DetailController(this._movieList[row]), true);
        }
    }
}
