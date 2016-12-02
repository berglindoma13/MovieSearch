using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieDownload;

namespace HelloWorld.iOS
{
    public class SetMovieInfo
    {

        private ImageDownloader imdown;
        public SetMovieInfo()
        {
            imdown = new ImageDownloader(new StorageClient());
        }

        public async Task setInfo(MovieInfo i, IApiMovieRequest movieApi, Movie movie)
        {
            ApiQueryResponse<MovieCredit> resp = await movieApi.GetCreditsAsync(i.Id);

            string[] actors = new string[3];
            string actors_3;

			if (resp.Item.CastMembers.Count == 0)
			{
				actors_3 = "";
			}
			else 
			{
				int j = 0;
				int k = 0;
				while ((j < 3) && (k < resp.Item.CastMembers.Count))
				{
					if (!resp.Item.CastMembers[k].Equals(null))
					{
						actors[k] = resp.Item.CastMembers[k].Name;
						j++;
					}
					k++;
				}

				actors_3 = actors[0] + ", " + actors[1] + ", " + actors[2];
				
			}

            var posterlink = i.PosterPath;

            var localFilePath = imdown.LocalPathForFilename(posterlink);

			if (localFilePath != "")
			{
				await imdown.DownloadImage(posterlink, localFilePath, CancellationToken.None);
			}
			else 
			{
				localFilePath = "not_found-full.png";
			}

            movie.Id = i.Id;
            movie.Title = i.Title;
            movie.Year = i.ReleaseDate.Year;
            movie.ImageName = localFilePath;
            movie.Actors = actors_3;
        }
    }
}
