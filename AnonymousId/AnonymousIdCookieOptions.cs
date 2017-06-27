using Microsoft.AspNetCore.Http;

namespace ReturnTrue.AspNetCore.Identity.Anonymous
{
    public class AnonymousIdCookieOptions : CookieOptions
    {
        public string Name { get; set; }
        public string HeaderName { get; set; }
        public bool SlidingExpiration { get; set; } = true;
        public int Timeout { get; set; }
    }
}