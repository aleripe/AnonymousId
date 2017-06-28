using ReturnTrue.AspNetCore.Identity.Anonymous;
using System;
using Xunit;

namespace AnonymousId.Tests
{
    public class AnonymousIdDataTests
    {
        [Fact]
        public void TestValidConstructor()
        {
            string anonymousId = Guid.NewGuid().ToString();
            DateTime expireDate = DateTime.UtcNow.AddDays(5);

            AnonymousIdData anonymousIdData = new AnonymousIdData(anonymousId, expireDate);

            Assert.Equal(anonymousId, anonymousIdData.AnonymousId);
            Assert.Equal(expireDate, anonymousIdData.ExpireDate);
        }

        [Fact]
        public void TestInvalidConstructor()
        {
            string anonymousId = Guid.NewGuid().ToString();
            DateTime expireDate = DateTime.UtcNow.AddDays(-2);

            AnonymousIdData anonymousIdData = new AnonymousIdData(anonymousId, expireDate);

            Assert.Equal(null, anonymousIdData.AnonymousId);
            Assert.Equal(expireDate, anonymousIdData.ExpireDate);
        }
    }
}