using Microsoft.AspNetCore.Identity;

namespace TruyenAPI.Repositories.User
{
    public interface IUserRespository
    {
        string CreateJwtToken(IdentityUser user, List<string> role);
    }
}
