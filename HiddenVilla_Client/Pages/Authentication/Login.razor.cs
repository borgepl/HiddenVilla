using HiddenVilla_Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Models.Dto.Login;
using System.Web;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Login
    {
        private AuthenticationDTO UserForAuthentication = new AuthenticationDTO();
        public bool IsProcessing { get; set; } = false;
        public bool ShowAuthenticationErrors { get; set; }
        public string? Errors { get; set; }
        public string? ReturnUrl { get; set; }

        [Inject]
        public required IAuthenticationService authenticationService { get; set; }

        [Inject]
        public required NavigationManager navigationManager { get; set; }

        private async Task LoginUser()
        {
            ShowAuthenticationErrors = false;
            IsProcessing = true;
            var result = await authenticationService.Login(UserForAuthentication);
            if (result.IsAuthSuccessful)
            {
                IsProcessing = false;

                var absoluteUri = new Uri(navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryParam["returnUrl"];
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + ReturnUrl);
                }
            }
            else
            {
                IsProcessing = false;
                Errors = result.ErrorMessage;
                ShowAuthenticationErrors = true;
            }
        }
    }
}