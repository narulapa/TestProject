using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.ResourceHandler
{
    public interface IUserDataHandler
    {
        Task<IEnumerable<Albums>> GetUsersData();
        Task<IEnumerable<Albums>> GetUserData(int? userId);

    }
}
