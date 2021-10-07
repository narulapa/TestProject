using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.ResourceHandler;

namespace WebAPITests.Controller
{
    [TestFixture]
    public class UserDataControllerTests
    {
        private Mock<IUserDataHandler> _userDataHandlerMock;
        private UserDataController _userDataController;
        private IEnumerable<Albums> _albumList;

        [SetUp]
        public void SetUp()
        {
            _userDataHandlerMock = new Mock<IUserDataHandler>();
            _userDataController = new UserDataController(_userDataHandlerMock.Object);
            _albumList = new List<Albums>
            {
                new Albums
                {
                    Id=1,
                    UserId=1,
                    Title="test data",
                    Photos=null
                },
                new Albums
                {
                    Id=2,
                    UserId=2,
                    Title="test data",
                    Photos=null
                }
                ,new Albums
                {
                    Id=3,
                    UserId=2,
                    Title="test data",
                    Photos=null
                }
            };
        }

        [Test]
        public async Task Get_UsersData_Returns200OK()
        {
            _userDataHandlerMock.Setup(x => x.GetUsersData()).Returns(Task.FromResult(_albumList));

            var result = await _userDataController.Get() as ObjectResult;

            Assert.NotNull(result);

            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.OK);

        }

        [TestCase(2)]
        public async Task Get_UserDataById_Returns_200OK(int userId)
        {
            _albumList = new List<Albums>
            {
                new Albums
                {
                    Id=2,
                    UserId=2,
                    Title="test data",
                    Photos=null
                }
                ,new Albums
                {
                    Id=3,
                    UserId=2,
                    Title="test data",
                    Photos=null
                }
            };
            _userDataHandlerMock.Setup(x => x.GetUserData(userId)).Returns(Task.FromResult(_albumList));

            var result = await _userDataController.Get(userId) as ObjectResult;

            Assert.NotNull(result);

            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.OK);

        }

        [TestCase(0)]
        public async Task Get_UserDataById_Returns_404NotFound(int userId)
        {
            _albumList = new List<Albums>();
            _userDataHandlerMock.Setup(x => x.GetUserData(userId)).Returns(Task.FromResult(_albumList));

            var result = await _userDataController.Get(userId) as ObjectResult;

            Assert.NotNull(result);

            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.NotFound);

        }
    }
}
