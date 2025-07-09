using Microsoft.AspNetCore.Identity;
using NZWalks.API.Models.Domain.DTO;

namespace NZWalks.API.Repository
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser identityUser, List<string> roles);
    }
}
