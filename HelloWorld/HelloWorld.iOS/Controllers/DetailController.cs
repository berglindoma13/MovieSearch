﻿using System.Collections.Generic;
using System.Text;
using System.Threading;
using CoreGraphics;

namespace HelloWorld.iOS
{
	using UIKit;
	public class DetailController : UIViewController
	{
		private Movie _movie;

		private const int HorizontalMargin = 20;

		private const int StartY = 80;

		private const int StepY = 50;

		private int _yCoord;

		public DetailController(Movie movie)
		{
			this._movie = movie;
		}

		public override void ViewDidLoad()
		{
			this.Title = "Movie info";

			this.View.BackgroundColor = UIColor.White;

			this._yCoord = StartY;

			var movieImage = createImage();
			var movieName = createMovieName();
			var details = createDetails();
			var overview = createOverview();


			this.View.AddSubview(movieImage);
			this.View.AddSubview(movieName);
			this.View.AddSubview(details);
			this.View.AddSubview(overview);

		}

		private UIImageView createImage()
		{
			var movieImage = new UIImageView()
			{
				Frame = new CGRect(5, 5, 33, 33),
				Image = UIImage.FromFile(_movie.ImageName)

			};
			this._yCoord += StepY;
			return movieImage;
		}

		private UILabel createMovieName()
		{
			var movieName = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50),
				Text = _movie.Title + " (" + _movie.Year + ")"
			};
			this._yCoord += StepY;
			return movieName;
		}

		private UILabel createDetails()
		{
			var genreString = "";
			for (int i = 0; i < _movie.Genre.Count; i++)
			{
				genreString += _movie.Genre[i];
				if (i < _movie.Genre.Count-1)
				{
					genreString += ", ";
				}
			}

			var details = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50),
				Text = _movie.Runtime.ToString() + " min | " + genreString
			
			};
			this._yCoord += StepY;
			return details;
		}

		private UILabel createOverview()
		{
			var overview = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50),
				Text = _movie.Review,
				Lines = 0
			};
			this._yCoord += StepY;
			return overview;
		}
	}
}