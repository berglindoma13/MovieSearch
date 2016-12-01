using System;
using System.Collections.Generic;
using System.Text;
using HelloWorld.iOS.Views;

namespace HelloWorld.iOS.Controllers
{
    using HelloWorld;

    using UIKit;
    public class MovieCollectionController : UITableViewController
    {
        private List<Movie> _movieList;

        public MovieCollectionController(List<Movie> movieList)
        {
            this._movieList = movieList;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 0);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.White;
            this.Title = "Top Rated Movies";

            this.TableView.Source = new MovieListSource(this._movieList, OnSelectedMovie);
        }

        private void OnSelectedMovie(int row)
        {

            this.NavigationController.PushViewController(new DetailController(this._movieList[row]), true);

        }
    }
}
