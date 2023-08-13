using Microsoft.AspNetCore.Identity;

namespace NZwalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtTokenAsync(IdentityUser user, List<string> roles);

    }
}
