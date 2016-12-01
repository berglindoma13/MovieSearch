using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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

        public async void setInfo(MovieInfo i, IApiMovieRequest movieApi, Movie movie)
        {
            ApiQueryResponse<MovieCredit> resp = await movieApi.GetCreditsAsync(i.Id);
            var details = await movieApi.FindByIdAsync(i.Id);

            List<string> actors = new List<string>();

            for (int j = 0; (j < resp.Item.CastMembers.Count); j++)
            {
                actors.Add(resp.Item.CastMembers[j].Name);
            }
            var posterlink = i.PosterPath;

            var localFilePath = imdown.LocalPathForFilename(posterlink);

            var poster = imdown.DownloadImage(posterlink, localFilePath, CancellationToken.None);

            movie.Id = i.Id;
            movie.Title = i.Title;
            movie.Year = i.ReleaseDate.Year;
            movie.ImageName = localFilePath;
            movie.Actors = actors;

        }
    }
}
