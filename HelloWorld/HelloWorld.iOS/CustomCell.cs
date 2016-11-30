using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using Foundation;

namespace HelloWorld.iOS
{
    using UIKit;
    class CustomCell : UITableViewCell 
    {
        private UILabel _nameLabel, _actorLabel;
        private UIImageView _imageView;

        public CustomCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            this._imageView = new UIImageView();
            this._nameLabel = new UILabel()
            {
                Font = UIFont.FromName("Cochin-BoldItalic", 22f),
                TextColor = UIColor.FromRGB(127, 51, 0),
            };
            this._actorLabel = new UILabel()
            {
                Font = UIFont.FromName("AmericanTypewriter", 12f),
                TextColor = UIColor.FromRGB(38, 127, 0),
                TextAlignment = UITextAlignment.Left,
            };

            this.ContentView.AddSubviews(new UIView[] { this._imageView, this._nameLabel, this._actorLabel });
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            this._imageView.Frame = new CGRect(5, 5, 33, 33);
            this._nameLabel.Frame = new CGRect(40, 5, this.ContentView.Bounds.Width - 60, 25);
            this._actorLabel.Frame = new CGRect(40, 25, 100, 20);
        }

        public void UpdateCell(string title, string year, string imageName, string actor1, string actor2, string actor3)
        {

            this._imageView.Image = UIImage.FromFile(imageName);
            this._nameLabel.Text = title + " (" + year + ")";
            this._actorLabel.Text = actor1 + ", " + actor2 + ", " + actor3; 

            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;

        }
    }
}
