using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
