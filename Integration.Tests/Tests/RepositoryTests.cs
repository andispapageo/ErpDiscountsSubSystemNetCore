using Domain.Core.Entities;
using Domain.Core.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Integration.Tests.Tests
{
    public class RepositoryTests : BaseTest
    {

        [Test]
        public async Task UnitOfWorkOrderTest() => await GenericTests(new Mock<IUnitOfWork<TbOrder>>());
        [Test]
        public async Task UnitOfWorkOrderDiscountTest() => await GenericTests(new Mock<IUnitOfWork<TbOrderDiscount>>());
        [Test]
        public async Task UnitOfWorkDiscountTest() => await GenericTests(new Mock<IUnitOfWork<TbDiscount>>());
        [Test]
        public async Task UnitOfWorkCustomerTest() => await GenericTests(new Mock<IUnitOfWork<TbCustomer>>());
        [Test]
        public async Task UnitOfWorkSubscriptionTest() => await GenericTests(new Mock<IUnitOfWork<TbSubscription>>());
        [Test]
        public async Task UnitOfWorkCurrencyTest() => await GenericTests(new Mock<IUnitOfWork<TbCurrency>>());

        public async Task GenericTests<T>(Mock<IUnitOfWork<T>> mock) where T : class
        {
            mock.Object.Should().NotBeNull();
            mock.Object.Should().As<IUnitOfWork<T>>();

            var repoMock = new Mock<IRepository<T>>();
            repoMock.Object.Should().NotBeNull();
            repoMock.Object.Should().As<IRepository<T>>();

            var enumerable = await repoMock.Object.GetCollectionAsync();
            var list = enumerable.ToList();
            list.Should().NotBeNull();
        }
    }
}
