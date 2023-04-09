using Moq;
using NUnit.Framework;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Tests.UserServiceTests
{
    [TestFixture]
    public class UserAuthenticationTestscs
    {
        UserService userService;
        UserAuthenticationData authenticationData;
        Mock<IUserRepository> mock = new Mock<IUserRepository>();

        [SetUp]
        public void SetUp()
        {
            authenticationData = new UserAuthenticationData()
            {
                Email = "Ivan@mail.ru",
                Password = "12345678",
            };
            mock.Setup(m => m.FindByEmail(authenticationData.Email)).Returns(new UserEntity()
            {
                email= authenticationData.Email,
                password=authenticationData.Password
            });
            userService = new UserService();
            userService.SetPrivate("userRepository", mock.Object);
        }
        [Test]
        public void Authentication_UserNotInBase_MustThrowException()
        {
            mock.Setup(m => m.FindByEmail(authenticationData.Email)).Returns(value: null);
            Assert.Throws<UserNotFoundException>(() => userService.Authenticate(authenticationData));
        }
        [Test]
        public void Authentication_WrongPassword_MustThrowException()
        {
            mock.Setup(m => m.FindByEmail(authenticationData.Email)).Returns(new UserEntity()
            {
                email = authenticationData.Email,
                password = "123"
            });
            Assert.Throws<WrongPasswordException>(() => userService.Authenticate(authenticationData));
        }
        [Test]
        public void Authentication_Successfully()
        {
            var user = userService.Authenticate(authenticationData);
            Assert.That(user.Email == authenticationData.Email && user.Password == authenticationData.Password);
        }
    }
}
