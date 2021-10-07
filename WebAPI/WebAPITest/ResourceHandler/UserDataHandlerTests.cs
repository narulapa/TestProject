using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebAPI.Helper;
using WebAPI.Models;
using WebAPI.ResourceHandler;

namespace WebAPITests.ResourceHandler
{
    [TestFixture]
    public class UserDataHandlerTests
    {
        private IUserDataHandler _userDataHandler;
        private List<Albums> _albumList;
        private Mock<IApiClientHelper> _apiClientHelperMock;

        [SetUp]
        public void SetUp()
        {
            _apiClientHelperMock = new Mock<IApiClientHelper>();
            _userDataHandler = new UserDataHandler(_apiClientHelperMock.Object);

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
        public async Task Get_UsersData_Returns_All_UserData()
        {
            _apiClientHelperMock.Setup(x => x.GetAsync<List<Albums>>(It.IsAny<string>()))
                .Returns(Task.FromResult(_albumList));
         
            var result =await _userDataHandler.GetUsersData();
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToList().Count, _albumList.Count);

        }

        [TestCase(2)]
        public async Task Get_UserData_Returns_UserDataById(int? userId)
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
            _apiClientHelperMock.Setup(x => x.GetAsync<List<Albums>>(It.IsAny<string>()))
                .Returns(Task.FromResult(_albumList));

            var result = await _userDataHandler.GetUserData(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToList().Count, _albumList.Count);

        }

        [TestCase(0)]
        public async Task Get_UserData_Returns_NoData_When_UserId_NotFound(int? userId)
        {
            _albumList = new List<Albums>();
            _apiClientHelperMock.Setup(x => x.GetAsync<List<Albums>>(It.IsAny<string>()))
                .Returns(Task.FromResult(_albumList));

            var result = await _userDataHandler.GetUserData(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToList().Count, 0);

        }
    }
}
