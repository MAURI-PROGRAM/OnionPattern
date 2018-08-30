﻿using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnionPattern.Domain.Repository;
using OnionPattern.Service.Tests.Requests.Mocks;
using OnionPattern.TestUtils;
using Serilog;
using System;

namespace OnionPattern.Service.Tests.Requests
{
    public class BaseRequestAsyncTests
    {
        [TestClass]
        public class ConstructorTests
        {
            private IRepositoryAsync<FakeEntity> fakeRepository;
            private IRepositoryAsyncAggregate fakeRepositoryAggregate;
            private ILogger fakeLogger;

            [TestInitialize]
            public void TestInitialize()
            {
                fakeRepository = A.Fake<IRepositoryAsync<FakeEntity>>();
                fakeRepositoryAggregate = A.Fake<IRepositoryAsyncAggregate>();
                fakeLogger = A.Fake<ILogger>();
            }

            [TestCleanup]
            public void TestCleanup()
            {
                Fake.ClearConfiguration(fakeRepository);
                Fake.ClearConfiguration(fakeRepositoryAggregate);
                Fake.ClearConfiguration(fakeLogger);
            }

            [TestMethod]
            public void RepositoryIsNull()
            {
                Action ctor = () => new MockBaseRequestAsync(null, fakeRepositoryAggregate);
                ctor.Should()
                    .Throw<ArgumentNullException>()
                    .WithMessage(ExceptionMessages.ArgumentNull("repository"));
            }

            [TestMethod]
            public void RepositoryAggregateIsNull()
            {
                Action ctor = () => new MockBaseRequestAsync(fakeRepository, null);
                ctor.Should()
                    .Throw<ArgumentNullException>()
                    .WithMessage(ExceptionMessages.ArgumentNull("repositoryAggregate"));
            }

            [TestMethod]
            public void IsValid()
            {
                var baseRequest = new MockBaseRequestAsync(fakeRepository, fakeRepositoryAggregate);

                baseRequest.Should().NotBeNull();
                baseRequest.Should().BeAssignableTo<BaseServiceRequestAsync<FakeEntity>>();
                baseRequest.Should().BeOfType<MockBaseRequestAsync>();
            }
        }
    }


}
