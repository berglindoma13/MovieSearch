using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld.iOS.Controllers
{
    using UIKit;
    public class TabBarController : UITabBarController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.TabBar.BackgroundColor = UIColor.FromRGB(0.714f, 0.98f, 1);
			this.TabBar.TintColor = UIColor.Blue;
            this.SelectedIndex = 0;
        }
    }
}