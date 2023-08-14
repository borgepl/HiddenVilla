using Models.Dto.Login;
using Models.Dto.Registration;

namespace HiddenVilla_Client.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegisteration);

        Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication);

        Task Logout();
    }
}
