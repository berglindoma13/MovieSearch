using System;
using System.Collections.Generic;
using HelloWorld.iOS.Views;

namespace HelloWorld.iOS.Controllers
{
    using Foundation;

    using UIKit;

    public class MovieCollectionSource : UICollectionViewSource
    {
        public static readonly NSString MovieCollectionCellId = new NSString("MovieCollectionCell");

        private List<Movie> _movieList;

        private Action<int> _onSelectedMovie;

        public MovieCollectionSource(List<Movie> movieList)
        {
            this._movieList = movieList;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (CustomCollectionCell)collectionView.DequeueReusableCell(MovieCollectionCellId, indexPath);

            int row = indexPath.Row;
            cell.UpdateCell(this._movieList[row].Title, this._movieList[row].ImageName);

            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return this._movieList.Count;
        }

        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            Console.WriteLine("Row {0} selected", indexPath.Row);
        }

        public override bool ShouldSelectItem(UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }
    }
}
