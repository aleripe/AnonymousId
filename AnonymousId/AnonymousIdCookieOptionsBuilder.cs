using System;

namespace ReturnTrue.AspNetCore.Identity.Anonymous
{
    public class AnonymousIdCookieOptionsBuilder
    {
        private const string DEFAULT_COOKIE_NAME = ".ASPXANONYMOUS";
        private const string DEFAULT_COOKIE_PATH = "/";
        private const int DEFAULT_COOKIE_TIMEOUT = 120;
        private const int MINIMUM_COOKIE_TIMEOUT = 120;
        private const int MAXIMUM_COOKIE_TIMEOUT = 60 * 24 * 365 * 2;
        private const bool DEFAULT_COOKIE_REQUIRE_SSL = false;
        
        private string cookieName;
        private string cookiePath;
        private int? cookieTimeout;
        private string cookieDomain;
        private bool? cookieRequireSsl;

        public AnonymousIdCookieOptionsBuilder SetCustomCookieName(string cookieName)
        {
            this.cookieName = cookieName;
            return this;
        }

        public AnonymousIdCookieOptionsBuilder SetCustomCookiePath(string cookiePath)
        {
            this.cookiePath = cookiePath;
            return this;
        }

        public AnonymousIdCookieOptionsBuilder SetCustomCookieTimeout(int cookieTimeout)
        {
            this.cookieTimeout = Math.Min(Math.Max(MINIMUM_COOKIE_TIMEOUT, cookieTimeout), MAXIMUM_COOKIE_TIMEOUT);
            return this;
        }

        public AnonymousIdCookieOptionsBuilder SetCustomCookieDomain(string cookieDomain)
        {
            this.cookieDomain = cookieDomain;
            return this;
        }

        public AnonymousIdCookieOptionsBuilder SetCustomCookieRequireSsl(bool cookieRequireSsl)
        {
            this.cookieRequireSsl = cookieRequireSsl;
            return this;
        }

        public AnonymousIdCookieOptions Build()
        {
            AnonymousIdCookieOptions options = new AnonymousIdCookieOptions
            {
                Name = cookieName ?? DEFAULT_COOKIE_NAME,
                Path = cookiePath ?? DEFAULT_COOKIE_PATH,
                Timeout = cookieTimeout ?? DEFAULT_COOKIE_TIMEOUT,
                Secure = cookieRequireSsl ?? DEFAULT_COOKIE_REQUIRE_SSL
            };

            if (!string.IsNullOrWhiteSpace(cookieDomain))
            {
                options.Domain = cookieDomain;
            }

            return options;
        }
    }
}