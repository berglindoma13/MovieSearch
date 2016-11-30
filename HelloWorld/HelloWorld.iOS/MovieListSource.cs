﻿using System;
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
            var cell = (CustomCell)tableView.DequeueReusableCell(this.MovieListCellId);
            if (cell == null)
            {
                //cell = new UITableViewCell(UITableViewCellStyle.Default, (NSString)this.MovieListCellId);
                cell = new CustomCell((NSString)this.MovieListCellId);
            }

            int row = indexPath.Row;
            cell.UpdateCell(this._movieList[row].Title, this._movieList[row].Year.ToString(), this._movieList[row].ImageName);
            cell.TextLabel.Text = this._movieList[row].Title;

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
