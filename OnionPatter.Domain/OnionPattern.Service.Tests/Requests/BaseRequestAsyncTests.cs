﻿using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnionPattern.Domain.Repository;
using System;
using Serilog;

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
                Action ctor = () => new MockBaseRequestAsync(null, fakeRepositoryAggregate, fakeLogger);
                ctor.ShouldThrow<ArgumentNullException>()
                    .WithMessage($"Value cannot be null.{Environment.NewLine}Parameter name: repository cannot be null.");
            }

            [TestMethod]
            public void RepositoryAggregateIsNull()
            {
                Action ctor = () => new MockBaseRequestAsync(fakeRepository, null, fakeLogger);
                ctor.ShouldThrow<ArgumentNullException>()
                    .WithMessage($"Value cannot be null.{Environment.NewLine}Parameter name: repositoryAggregate cannot be null.");
            }

            [TestMethod]
            public void LoggerIsNull()
            {
                Action ctor = () => new MockBaseRequestAsync(fakeRepository, fakeRepositoryAggregate, null);
                ctor.ShouldThrow<ArgumentNullException>()
                    .WithMessage($"Value cannot be null.{Environment.NewLine}Parameter name: logger cannot be null.");
            }

            [TestMethod]
            public void IsValid()
            {
                var baseRequest = new MockBaseRequestAsync(fakeRepository, fakeRepositoryAggregate, fakeLogger);

                baseRequest.Should().NotBeNull();
                baseRequest.Should().BeAssignableTo<BaseServiceRequestAsync<FakeEntity>>();
                baseRequest.Should().BeOfType<MockBaseRequestAsync>();
            }
        }
    }


}