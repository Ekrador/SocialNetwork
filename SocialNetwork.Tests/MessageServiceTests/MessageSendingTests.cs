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

namespace SocialNetwork.Tests.MessageServiceTests
{
    [TestFixture]
    public class MessageServiceTests
    {
        MessageService messageService;
        Mock<IMessageRepository> messageRepositoryMock = new Mock<IMessageRepository>();
        Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        MessageSendingData messageSendingData;

        [SetUp]
        public void SetUp()
        {
            messageSendingData = new MessageSendingData()
            {
                Content = "qwerty",
                RecipientEmail = "Ivan@mail.ru",
                SenderId = 1
            };
            userRepositoryMock.Setup(m => m.FindByEmail(messageSendingData.RecipientEmail)).Returns(new UserEntity());
            messageRepositoryMock.Setup(m => m.Create(It.IsAny<MessageEntity>())).Returns(1);
            messageService = new MessageService();
            messageService.SetPrivate("userRepository", userRepositoryMock.Object);
            messageService.SetPrivate("messageRepository", messageRepositoryMock.Object);
        }
        [Test]
        public void SendMessage_EmptyContent_MustThrowException()
        {
            messageSendingData.Content = string.Empty;
            Assert.Throws<ArgumentNullException>(() => messageService.SendMessage(messageSendingData));
        }
        [Test]
        public void SendMessage_ContentBiggerThan5000_MustThrowException()
        {
            messageSendingData.Content = new string(' ', 5001);
            Assert.Throws<ArgumentOutOfRangeException>(() => messageService.SendMessage(messageSendingData));

        }
        [Test]
        public void SendMessage_RecipientEmailNotInBase_MustThrowException()
        {
            userRepositoryMock.Setup(m => m.FindByEmail(messageSendingData.RecipientEmail)).Returns(value: null);
            Assert.Throws<UserNotFoundException>(() => messageService.SendMessage(messageSendingData));
        }
        [Test]
        public void SendMessage_Successfully()
        {
            Assert.DoesNotThrow(() => messageService.SendMessage(messageSendingData));
        }
    }
}
