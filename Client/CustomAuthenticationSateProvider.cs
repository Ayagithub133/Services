using Microsoft.AspNetCore.Components.Authorization;
using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Client
{
    public class CustomAuthenticationSateProvider : AuthenticationStateProvider
    {

        private readonly HttpClient _httpClient;
        public CustomAuthenticationSateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var httpResponse = await _httpClient.GetAsync("https://localhost:44312/api/AccountUser/currentuser");
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<UserManagerResponse>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            
            if (result.IsSuccess)
            {
                var claim = new Claim(ClaimTypes.Name, "");
                var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

    }
}
