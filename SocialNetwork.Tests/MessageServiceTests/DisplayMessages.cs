using Moq;
using NUnit.Framework;
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
    public class DisplayMessages
    {
        MessageService messageService;
        [SetUp]
        public void SetUp()
        {
            messageService = new MessageService();
        }
        [Test]
        public void GetIncomingMessages_MustReturnListOFMessages()
        {
            Assert.That(messageService.GetIncomingMessagesByUserId(1) is List<Message>);
        }
        [Test]
        public void GetOutcomingMessages_MustReturnListOFMessages()
        {
            Assert.That(messageService.GetOutcomingMessagesByUserId(1) is List<Message>);
        }
    }
}
