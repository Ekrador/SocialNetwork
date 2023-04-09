using Moq;
using NUnit.Framework;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.Tests.UserServiceTests
{

    [TestFixture]
    public class UserRegistrationTests
    {

        UserService userService;
        UserRegistrationData registrationData;
        Mock<IUserRepository> mock = new Mock<IUserRepository>();
        string invalidPassword = "123";
        string invalidEmail = "123";

        [SetUp]
        public void SetUp()
        {
            registrationData = new UserRegistrationData()
            {
                Email = "Ivan@mail.ru",
                FirstName = "Ivan",
                LastName = "Ivanov",
                Password = "12345678",
            };
            mock.Setup(m => m.FindByEmail(registrationData.Email)).Returns(value: null);
            mock.Setup(m => m.Create(It.IsAny<UserEntity>())).Returns(1);
            userService = new UserService();
            userService.SetPrivate("userRepository", mock.Object);
        }
        [Test]
        public void Registration_EmptyFirstName_MustThrowException()
        {
            registrationData.FirstName = null;

            Assert.Throws<ArgumentNullException>(() => userService.Register(registrationData));
        }
        [Test]
        public void Registration_EmptyLastName_MustThrowException()
        {
            registrationData.FirstName = null;

            Assert.Throws<ArgumentNullException>(() => userService.Register(registrationData));
        }
        [Test]
        public void Registration_EmptyEmail_MustThrowException()
        {
            registrationData.Email = null;

            Assert.Throws<ArgumentNullException>(() => userService.Register(registrationData));
        }
        [Test]
        public void Registration_EmptyPassword_MustThrowException()
        {
            registrationData.Password = null;

            Assert.Throws<ArgumentNullException>(() => userService.Register(registrationData));
        }
        [Test]
        public void Registration_PasswordLessThen8_MustThrowException()
        {
            registrationData.Password = invalidPassword;

            Assert.Throws<ArgumentNullException>(() => userService.Register(registrationData));
        }
        [Test]
        public void Registration_NotValidEmail_MustThrowException()
        {
            registrationData.Email = invalidEmail;

            Assert.Throws<ArgumentNullException>(() => userService.Register(registrationData));
        }
        [Test]
        public void Registration_EmailAlreadyInBase_MustThrowException()
        {
            mock.Setup(m => m.FindByEmail(registrationData.Email)).Returns(new UserEntity());
            Assert.Throws<ArgumentNullException>(() => userService.Register(registrationData));
        }
        [Test]
        public void Registration_CompleteSuccessfully()
        {
            Assert.DoesNotThrow(() => userService.Register(registrationData));
        }
    }
}