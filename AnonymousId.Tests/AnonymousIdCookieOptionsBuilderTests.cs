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
            Assert.Equal(options.HeaderName, "AnonymousId");
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
                .SetCustomHeaderName("CUSTOMHEADERNAME")
                .Build();

            Assert.Equal("CUSTOMCOOKIENAME", options.Name);
            Assert.Equal("/path", options.Path);
            Assert.Equal(1000000, options.Timeout);
            Assert.Equal("www.example.com", options.Domain);
            Assert.Equal(true, options.Secure);
            Assert.Equal("CUSTOMHEADERNAME", options.HeaderName);
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
                .SetCustomCookieTimeout(0)
                .Build();

            Assert.Equal(60 * 24 * 365 * 2, options.Timeout);
        }
    }
}