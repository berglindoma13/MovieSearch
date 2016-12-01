using System;
using System.Collections.Generic;
using HelloWorld.iOS.Views;

namespace HelloWorld.iOS.Controllers
{
    using Foundation;

    using UIKit;

    public class MovieCollectionSource : UITableViewSource
    {
        public readonly NSString MovieCollectionCellId = new NSString("MovieCollectionCell");
        private List<Movie> _movieList;
        private Action<int> _onSelectedMovie;

        public MovieCollectionSource(List<Movie> movieList)
        {
            this._movieList = movieList;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (CustomCell)tableView.DequeueReusableCell(MovieCollectionCellId);

            if (cell == null)
            {
                cell = new CustomCell((NSString)this.MovieCollectionCellId);
            }

            int row = indexPath.Row;
            cell.UpdateCell(this._movieList[row].Title, this._movieList[row].Year.ToString(), this._movieList[row].ImageName, this._movieList[row].Actors, this._movieList[row].Runtime, this._movieList[row].Genre, this._movieList[row].Review);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._movieList.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            _onSelectedMovie(indexPath.Row);
        }
    }
}
