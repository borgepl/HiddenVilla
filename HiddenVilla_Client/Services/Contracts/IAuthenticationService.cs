using Models.Dto.Login;
using Models.Dto.Registration;

namespace HiddenVilla_Client.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegistration);

        Task<AuthenticationResponseDTO> Login(AuthenticationDTO userForAuthentication);

        Task Logout();
    }
}
