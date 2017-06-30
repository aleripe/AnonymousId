using ReturnTrue.AspNetCore.Identity.Anonymous;
using Xunit;

namespace AnonymousId.Tests
{
    public class AnonymousIdCookieOptionsBuilderTests
    {
        [Fact]
        public void TestDefaultValues()
        {
            AnonymousIdCookieOptions options = new AnonymousIdCookieOptionsBuilder().Build();

            Assert.Equal(options.Name, ".ASPXANONYMOUS");
            Assert.Equal(options.Path, "/");
            Assert.Equal(options.Timeout, 100000);
            Assert.Equal(options.Domain, null);
            Assert.Equal(options.Secure, false);
        }

        [Fact]
        public void TestCustomValues()
        {
            AnonymousIdCookieOptions options = new AnonymousIdCookieOptionsBuilder()
                .SetCustomCookieName("CUSTOMCOOKIENAME")
                .SetCustomCookiePath("/path")
                .SetCustomCookieTimeout(1000000)
                .SetCustomCookieDomain("www.example.com")
                .SetCustomCookieRequireSsl(true)
                .Build();

            Assert.Equal("CUSTOMCOOKIENAME", options.Name);
            Assert.Equal("/path", options.Path);
            Assert.Equal(1000000, options.Timeout);
            Assert.Equal("www.example.com", options.Domain);
            Assert.Equal(true, options.Secure);
        }

        [Fact]
        public void TestMinimumTimeout()
        {
            AnonymousIdCookieOptions options = new AnonymousIdCookieOptionsBuilder()
                .SetCustomCookieTimeout(0)
                .Build();
            
            Assert.Equal(1, options.Timeout);
        }

        [Fact]
        public void TestMaximumTimeout()
        {
            AnonymousIdCookieOptions options = new AnonymousIdCookieOptionsBuilder()
                .SetCustomCookieTimeout(2000000000)
                .Build();

            Assert.Equal(60 * 24 * 365 * 2, options.Timeout);
        }
    }
}