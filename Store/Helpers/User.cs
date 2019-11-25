using Microsoft.AspNetCore.Http;

namespace Store.Helpers
{
    public class User : IUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public string UserId
        {
            get
            {
                return httpContextAccessor.HttpContext.User.Identity.Name;
            }
        }
        public User(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}
