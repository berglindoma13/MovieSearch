using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;

namespace HelloWorld.iOS
{
    using UIKit;

    public class CustomCell : UITableViewCell
    {
        private UILabel _nameLabel, _actorLabel;
        private UIImageView _imageView;

        public CustomCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            _imageView = new UIImageView();
            _nameLabel = new UILabel()
            {
                Font = UIFont.FromName("Cochin-BoldItalic", 18f),
                TextColor = UIColor.FromRGB(127, 51, 0),
            };
            _actorLabel = new UILabel()
            {
                Font = UIFont.FromName("AmericanTypewriter", 12f),
                TextColor = UIColor.FromRGB(38, 127, 0),
                TextAlignment = UITextAlignment.Left,
            };

            ContentView.AddSubviews(new UIView[] {_imageView, _nameLabel, _actorLabel});
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            _imageView.Frame = new CGRect(5, 5, 33, 33);
            _nameLabel.Frame = new CGRect(45, 5, ContentView.Bounds.Width - 50, 25);
            _actorLabel.Frame = new CGRect(45, 25, ContentView.Bounds.Width - 50, 20);
        }

        public void UpdateCell(string title, string year, string imageName, string actors, int runtime,
            string genre, string review)
        {
            if (imageName != null)
            {
                _imageView.Image = UIImage.FromFile(imageName);

            }
            else
            {
                _imageView.Image = UIImage.FromFile("not_found-full.png");
            }
            _nameLabel.Text = title + " (" + year + ")";


            _actorLabel.Text = actors;
       
            Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }
    }
}