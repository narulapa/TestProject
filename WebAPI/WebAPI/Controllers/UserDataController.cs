using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.ResourceHandler;

namespace WebAPI.Controllers
{
    
    [ApiController]
    [Route("api")]
    public class UserDataController : ControllerBase
    {
        private readonly IUserDataHandler _userDataHandler;
        public UserDataController(IUserDataHandler userDataHandler)
        {
            _userDataHandler = userDataHandler;
        }

        // GET api/usersdata

        [Route("usersdata")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _userDataHandler.GetUsersData();

                if (data == null || !data.Any())
                {
                    return NotFound("Data not found.");
                }
                else
                {
                    return Ok(data);
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Something went wrong. Plese try again later.");
            }

        }

        // GET api/userdata/1
        [Route("userdata/{userId}")]
        public async Task<IActionResult> Get(int? userId)
        {
            try
            {
                var data = await _userDataHandler.GetUserData(userId);

                if (data == null || !data.Any())
                {
                    return NotFound("Please enter correct user id");
                }
                else
                {
                    return Ok(data);
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Something went wrong. Plese try again later");
            }

        }

    }
}
