﻿using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnionPattern.Api.Controllers;
using OnionPattern.Domain.Services.Requests.Platform;
using System;

namespace OnionPattern.Api.Tests.Controllers
{
    public class PlatformControllerTests
    {

        [TestClass]
        public class ConstructorTests
        {
            private IPlatformRequestAggregate fakePlatformRequestAggregate;

            [TestInitialize]
            public void TestInitalize()
            {
                fakePlatformRequestAggregate = A.Fake<IPlatformRequestAggregate>();
            }

            [TestCleanup]
            public void TestCleanup()
            {
                Fake.ClearConfiguration(fakePlatformRequestAggregate);
            }

            [TestMethod]
            public void GetAllGamesRequestIsNull()
            {
                Action ctor = () => new PlatformsController(null);

                ctor.ShouldThrow<ArgumentNullException>()
                    .WithMessage($"Value cannot be null.{Environment.NewLine}Parameter name: requestAggregate cannot be null.");
            }

            [TestMethod]
            public void Inheritence()
            {
                var controller = new PlatformsController(fakePlatformRequestAggregate);

                controller.Should().NotBeNull();
                controller.Should().BeAssignableTo<Controller>();
                controller.Should().BeAssignableTo<BaseController>();
                controller.Should().BeOfType<PlatformsController>();
            }
        }
    }
}
