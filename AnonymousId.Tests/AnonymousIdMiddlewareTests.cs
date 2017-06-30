using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace AnonymousId.Tests
{
    public class AnonymousIdMiddlewareTests
    {
        [Fact]
        public async Task TestCreateCookie()
        {
            var cookieOptions = new AnonymousIdCookieOptionsBuilder().Build();

            var requestMock = new Mock<HttpRequest>();
            requestMock.Setup(x => x.Cookies).Returns(new RequestCookieCollection());

            var userMock = new Mock<ClaimsPrincipal>();
            userMock.Setup(x => x.Identity).Returns(new ClaimsIdentity());

            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.Features).Returns(new FeatureCollection());
            contextMock.Setup(x => x.Request).Returns(requestMock.Object);
            contextMock.Setup(x => x.Response).Returns(new DefaultHttpResponse(new DefaultHttpContext()));
            contextMock.Setup(x => x.User).Returns(userMock.Object);

            var anonymousIdMiddleware = new AnonymousIdMiddleware(nextDelegate: (innerHttpContext) => Task.FromResult(0), cookieOptions: cookieOptions);
            await anonymousIdMiddleware.Invoke(contextMock.Object);

            Assert.True(!string.IsNullOrWhiteSpace(GetCookieValueFromResponse(contextMock.Object.Response, cookieOptions.Name)));
        }

        [Fact]
        public async Task TestAvailableFeature()
        {
            var cookieOptions = new AnonymousIdCookieOptionsBuilder().Build();

            var requestMock = new Mock<HttpRequest>();
            requestMock.Setup(x => x.Cookies).Returns(new RequestCookieCollection());

            var userMock = new Mock<ClaimsPrincipal>();
            userMock.Setup(x => x.Identity).Returns(new ClaimsIdentity());

            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.Features).Returns(new FeatureCollection());
            contextMock.Setup(x => x.Request).Returns(requestMock.Object);
            contextMock.Setup(x => x.Response).Returns(new DefaultHttpResponse(new DefaultHttpContext()));
            contextMock.Setup(x => x.User).Returns(userMock.Object);

            var anonymousIdMiddleware = new AnonymousIdMiddleware(nextDelegate: (innerHttpContext) => Task.FromResult(0), cookieOptions: cookieOptions);
            await anonymousIdMiddleware.Invoke(contextMock.Object);

            Assert.NotNull(contextMock.Object.Features.Get<IAnonymousIdFeature>());
            Assert.True(!string.IsNullOrWhiteSpace(contextMock.Object.Features.Get<IAnonymousIdFeature>().AnonymousId));
        }

        [Fact]
        public async Task TestReadExistingCookie()
        {
            var cookieOptions = new AnonymousIdCookieOptionsBuilder().Build();

            Dictionary<string, string> cookies = new Dictionary<string, string>();
            cookies.Add(cookieOptions.Name, "QOOUV4vy0gEkAAAANDQ3ZGU5OTUtNTQ4Ny00OTNlLThhM2QtMDQ5ZDUyNmY2NzIw");

            var requestMock = new Mock<HttpRequest>();
            requestMock.Setup(x => x.Cookies).Returns(new RequestCookieCollection(cookies));

            var userMock = new Mock<ClaimsPrincipal>();
            userMock.Setup(x => x.Identity).Returns(new ClaimsIdentity());

            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.Features).Returns(new FeatureCollection());
            contextMock.Setup(x => x.Request).Returns(requestMock.Object);
            contextMock.Setup(x => x.Response).Returns(new DefaultHttpResponse(new DefaultHttpContext()));
            contextMock.Setup(x => x.User).Returns(userMock.Object);

            var anonymousIdMiddleware = new AnonymousIdMiddleware(nextDelegate: (innerHttpContext) => Task.FromResult(0), cookieOptions: cookieOptions);
            await anonymousIdMiddleware.Invoke(contextMock.Object);

            Assert.NotNull(contextMock.Object.Features.Get<IAnonymousIdFeature>());
            Assert.Equal("447de995-5487-493e-8a3d-049d526f6720", contextMock.Object.Features.Get<IAnonymousIdFeature>().AnonymousId);
        }

        [Fact]
        public async Task TestDeleteExistingNonSecureCookie()
        {
            var cookieOptions = new AnonymousIdCookieOptionsBuilder()
                .SetCustomCookieRequireSsl(true)
                .Build();

            Dictionary<string, string> cookies = new Dictionary<string, string>();
            cookies.Add(cookieOptions.Name, "QOOUV4vy0gEkAAAANDQ3ZGU5OTUtNTQ4Ny00OTNlLThhM2QtMDQ5ZDUyNmY2NzIw");

            var requestMock = new Mock<HttpRequest>();
            requestMock.Setup(x => x.IsHttps).Returns(false);
            requestMock.Setup(x => x.Cookies).Returns(new RequestCookieCollection(cookies));

            var userMock = new Mock<ClaimsPrincipal>();
            userMock.Setup(x => x.Identity).Returns(new ClaimsIdentity());

            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.Features).Returns(new FeatureCollection());
            contextMock.Setup(x => x.Request).Returns(requestMock.Object);
            contextMock.Setup(x => x.Response).Returns(new DefaultHttpResponse(new DefaultHttpContext()));
            contextMock.Setup(x => x.User).Returns(userMock.Object);

            var anonymousIdMiddleware = new AnonymousIdMiddleware(nextDelegate: (innerHttpContext) => Task.FromResult(0), cookieOptions: cookieOptions);
            await anonymousIdMiddleware.Invoke(contextMock.Object);

            Assert.NotNull(contextMock.Object.Features.Get<IAnonymousIdFeature>());
            Assert.Equal(null, contextMock.Object.Features.Get<IAnonymousIdFeature>().AnonymousId);
            Assert.True(string.IsNullOrWhiteSpace(GetCookieValueFromResponse(contextMock.Object.Response, cookieOptions.Name)));
        }

        private string GetCookieValueFromResponse(HttpResponse response, string cookieName)
        {
            foreach (var headers in response.Headers.Values)
            {
                foreach (var header in headers)
                {
                    if (header.StartsWith(cookieName))
                    {
                        {
                            var p1 = header.IndexOf('=');
                            var p2 = header.IndexOf(';');
                            return header.Substring(p1 + 1, p2 - p1 - 1);
                        }
                    }
                }
            }

            return null;
        }
    }
}