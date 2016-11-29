using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld.iOS
{
    using UIKit;
    public class MovieListController : UITableViewController
    {
        private List<string> _movieList;

        public MovieListController(List<string> movieList)
        {
            this._movieList = movieList;
        }

        public override void ViewDidLoad()
        {
            this.Title = "Movie LisT";

            this.View.BackgroundColor = UIColor.White;

            this.TableView.Source = new MovieListSource(this._movieList);

        }
    }
}
