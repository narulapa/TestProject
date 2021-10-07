using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;
using WebAPI.Helper;
using WebAPI.Models;

namespace WebAPI.ResourceHandler
{
    /// <summary>
    /// UserData Handler is used for fetching user album
    /// and related photos 
    /// </summary>
    public class UserDataHandler : IUserDataHandler
    {
        private readonly IApiClientHelper _apiClientHelper;

        public UserDataHandler(IApiClientHelper apiClientHelper)
        {
            _apiClientHelper = apiClientHelper;

        }
        public async Task<IEnumerable<Albums>> GetUsersData()
        {
            try
            {
                var albums = await _apiClientHelper.GetAsync<List<Albums>>(ResourceUrl.GetUserAlbumData);

                if (albums != null && albums.Count > 0)
                {
                    var albumPhotos = await _apiClientHelper.GetAsync<List<Photos>>(ResourceUrl.GetUserPhotosData);

                    foreach (var item in albums)
                    {
                        item.Photos = albumPhotos?.Where(x => x.AlbumId == item.Id);
                    }
                }

                return albums;
            }
            catch (Exception)
            {
                return null;
            }
        }
       
        public async Task<IEnumerable<Albums>> GetUserData(int? userId)
        {
            try
            {
                if (userId == null)
                {
                    return null;
                }

                var albums = await _apiClientHelper.GetAsync<List<Albums>>(ResourceUrl.GetUserAlbumData);
                albums = albums?.Where(x => x.UserId == userId).ToList();

                if (albums != null && albums.Count > 0)
                {
                    var albumPhotos =
                        await _apiClientHelper.GetAsync<List<Photos>>(ResourceUrl.GetUserPhotosData);

                    foreach (var item in albums)
                    {
                        item.Photos = albumPhotos?.Where(x => x.AlbumId == item.Id);
                    }
                }

                return albums;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
    }
}
