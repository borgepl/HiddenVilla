﻿using Blazored.LocalStorage;
using Common;
using HiddenVilla_Client.Services.Contracts;
using Models.Dto.Login;
using Models.Dto.Registration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace HiddenVilla_Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO userForAuthentication)
        {
            var content = JsonConvert.SerializeObject(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signin", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(SD.Local_Token, result.Token);
                await _localStorage.SetItemAsync(SD.Local_UserDetails, result.UserDTO);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new AuthenticationResponseDTO { IsAuthSuccessful = true };
            }
            else
            {
                return result;
            }
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegistration)
        {
            throw new NotImplementedException();
        }
    }
}