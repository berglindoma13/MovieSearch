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
        private List<string> _movieList;

        public MovieListSource(List<string> movieList)
        {
            this._movieList = movieList;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(this.MovieListCellId);
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, (NSString)this.MovieListCellId);
            }

            int row = indexPath.Row;
            cell.TextLabel.Text = this._movieList[row];

            return cell;

        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._movieList.Count;
        }
    }
}
