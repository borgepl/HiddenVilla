using HiddenVilla_Client.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject] IAuthenticationService AuthenticationService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }