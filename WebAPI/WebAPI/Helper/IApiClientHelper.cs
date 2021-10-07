using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helper
{
    public interface IApiClientHelper
    {
        Task<T> GetAsync<T>(string apiEndPoint) where T : class;
    }
}
