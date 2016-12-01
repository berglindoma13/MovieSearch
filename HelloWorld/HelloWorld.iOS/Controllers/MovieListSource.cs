using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;

namespace HelloWorld.iOS
{
    public class MovieListSource : UITableViewSource
    {
        public readonly NSString MovieListCellId = new NSString("MovieListCell");
        private List<Movie> _movieList;
        private Action<int> _onSelectedMovie;

        public MovieListSource(List<Movie> movieList, Action<int> onSelectedMovie)
        {
            this._movieList = movieList;
            this._onSelectedMovie = onSelectedMovie;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (CustomCell)tableView.DequeueReusableCell(MovieListCellId);
            if (cell == null)
            {
                cell = new CustomCell((NSString)this.MovieListCellId);
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
            this._onSelectedMovie(indexPath.Row);
        }
    }
}
