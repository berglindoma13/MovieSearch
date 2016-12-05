using System;
using Android;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using DM.MovieApi;

namespace HelloWorld.Droid
{
	[Activity(Label = "TMDB", Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			MovieDbFactory.RegisterSettings(new ApiConnectionClass());

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			var button = FindViewById<Button>(Resource.Id.searchButton);
			var search = FindViewById<EditText>(Resource.Id.searchTextField);
			var movieResult = FindViewById<TextView>(Resource.Id.movieResult);
			//var movieResult = FindViewById<TextView>(2131099651);


			button.Click += async delegate
			{
				var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
				manager.HideSoftInputFromWindow(search.WindowToken, 0);

				var movieApi = MovieDbFactory.Create<DM.MovieApi.MovieDb.Movies.IApiMovieRequest>().Value;

				DM.MovieApi.ApiResponse.ApiSearchResponse<DM.MovieApi.MovieDb.Movies.MovieInfo> response = await movieApi.SearchByTitleAsync(search.Text);

				var movie = response.Results[0].Title;

				movieResult.Text = movie;

			};
		}
	}
}