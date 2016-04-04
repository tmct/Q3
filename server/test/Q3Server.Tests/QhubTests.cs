﻿using Q3Server;
using Xunit;
using Moq;
using Microsoft.Owin.Logging;

namespace Q3Tests
{
    public class QHubTests
    {
        private QHub hub;

        public QHubTests()
        {
            var manager = new Mock<IQManager>();
            var logger = new Mock<ILogger>();
            var userManager = new Mock<IUserManager>();
            hub = new QHub(manager.Object, logger.Object, userManager.Object);
        }

        [Fact(Skip = "Failing test after revival of UTs - needs fixing")]
        public void ICanStartAQueue()
        {
            hub.StartQueue("New Queue", null);
        }
    }
}
