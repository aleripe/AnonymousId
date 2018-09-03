# AnonymousId

Middleware for ASP.NET Core to enable the tracking of anonymous users

## Usage

Register without options:

```C#
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    app.UseAnonymousId();
    ....
}
```

Register with options:

```C#
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    app.UseAnonymousId(new AnonymousIdCookieOptionsBuilder()
      .SetCustomCookieName("MY_COOKIE_NAME")        // Custom cookie name
      .SetCustomCookieRequireSsl(true)              // Requires SSL
      .SetCustomCookieTimeout(120)                  // Custom timeout in seconds
      .SetCustomCookieDomain("www.contoso.com")     // Custom domain
      .SetCustomCookiePath("/path")                 // Custom path
      .SetCustomHeaderName("AnonymousId"));         // Custom header name
    ....
}
```

Get AnonymousId:

```C#
public class HomeController : Controller
{
  public ViewResult Index()
  {
      IAnonymousIdFeature feature = HttpContext.Features.Get<IAnonymousIdFeature>();
      if (feature != null)
      {
        string anonymousId = feature.AnonymousId;
      }
      ....
  }
}
```

## License

This project is licensed under the terms of the MIT license.
