using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace AnonymousId.Tests
{
    public class AnonymousIdMiddlewareTests
    {
        //[Fact]
        //public async Task TestCreateCookie()
        //{
        //    var requestMock = new Mock<HttpRequest>();
        //    requestMock.Setup(x => x.Cookies).Returns(new RequestCookieCollection());
        //    requestMock.Setup(x => x.Headers).Returns(new HeaderDictionary());

        //    var userMock = new Mock<ClaimsPrincipal>();
        //    userMock.Setup(x => x.Identity).Returns(new ClaimsIdentity());

        //    var responseMock = new Mock<HttpResponse>();
        //    responseMock.Setup(x => x.Cookies).Returns(new ResponseCookies());

        //    var contextMock = new Mock<HttpContext>();
        //    contextMock.Setup(x => x.Request).Returns(requestMock.Object);
        //    contextMock.Setup(x => x.User).Returns(userMock.Object);

        //    var cookieOptions = new AnonymousIdCookieOptionsBuilder().Build();
        //    var anonymousIdMiddleware = new AnonymousIdMiddleware(nextDelegate: (innerHttpContext) => Task.FromResult(0), cookieOptions: cookieOptions);
        //    await anonymousIdMiddleware.Invoke(contextMock.Object);

        //    Assert.True(!string.IsNullOrWhiteSpace(contextMock.Object.Request.Cookies[cookieOptions.Name]));
        //}
    }
}