using HiddenVilla_Client.Services;
using HiddenVilla_Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Models.Dto.Registration;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Register
    {
        private UserRequestDTO UserForRegistration = new UserRequestDTO();
        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        [Inject] public required IAuthenticationService AuthenticationService { get; set; }
        [Inject] public required NavigationManager NavManager { get; set; }
        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;
            var result = await AuthenticationService.RegisterUser(UserForRegistration);
            if (result.IsRegisterationSuccessful)
            {
                IsProcessing = false;
                NavManager.NavigateTo("/login");
            }
            else
            {
                IsProcessing = false;
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
        }
    }
}