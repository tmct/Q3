﻿using Microsoft.Owin.Logging;
using Moq;
using Q3Server.Interfaces;
using Xunit;

namespace Q3Server.Tests
{
    public class QHubTests
    {
        private QHub hub;

        public QHubTests()
        {
            var manager = new Mock<IQManager>();
            var logger = new Mock<ILogger>();
            var userGetter = new Mock<IObjectGetter<User>>();
            hub = new QHub(manager.Object, logger.Object, userGetter.Object);
        }

        [Fact(Skip = "Failing test after revival of UTs - needs fixing")]
        public void ICanStartAQueue()
        {
            hub.StartQueue("New Queue", null);
        }
    }
}
