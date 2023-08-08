using DataAccess.Data.Identity;

namespace HiddenVilla_Api.Services.Contracts
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);

    }
}
