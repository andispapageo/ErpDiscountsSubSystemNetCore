using NUnit.Framework;

namespace Integration.Tests
{
    [TestFixture]
    public class BaseTest
    {
        [SetUp]
        public async Task TestSetUp()
        {
            //await ResetState();
        }
    }
}
