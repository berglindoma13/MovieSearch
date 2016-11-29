using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld.iOS
{
    using UIKit;
    public class MovieListController : UIViewController
    {
        private List<string> _nameList;

        public MovieListController(List<string> nameList)
        {
            this._nameList = nameList;
        }

        public override void ViewDidLoad()
        {
            this.View.BackgroundColor = UIColor.White;
        }
    }
}
