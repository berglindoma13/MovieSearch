using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld.iOS.Controllers
{
    using HelloWorld;

    using UIKit;
    public class MovieCollectionController : UICollectionViewController
    {
        private List<Movie> _movieList;

        public MovieCollectionController(UICollectionViewFlowLayout layout, List<Movie> movieList) : base(layout)
        {
            this._movieList = movieList;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Favorites, 0);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.White;
            this.Title = "Collection";
        }
    }
}
