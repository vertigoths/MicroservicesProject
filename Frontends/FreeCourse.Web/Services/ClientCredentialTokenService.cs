using FreeCourse.Shared_.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;

namespace FreeCourse.Web.Services
{
	public class ClientCredentialTokenService : IClientCredentialTokenService
	{
		private readonly HttpClient _httpClient;
		private readonly ServiceApiSettings _serviceApiSettings;
		private readonly ClientSettings _clientSettings;
		private readonly IClientAccessTokenCache _clientAccessTokenCache;

		public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings,
			IOptions<ClientSettings> clientSettings, IClientAccessTokenCache clientAccessTokenCache, HttpClient httpClient)
		{
			_serviceApiSettings = serviceApiSettings.Value;
			_clientSettings = clientSettings.Value;
			_clientAccessTokenCache = clientAccessTokenCache;
			_httpClient = httpClient;
		}

		public async Task<string> GetToken()
		{
			var currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken", default);

			if(currentToken != null)
			{
				return currentToken.AccessToken;
			}

			var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
			{
				Address = _serviceApiSettings.IdentityBaseUri,
				Policy = new DiscoveryPolicy { RequireHttps = true }
			});

			if (disco.IsError)
			{
				throw disco.Exception;
			}

			var clientCredentialsTokenRequest = new ClientCredentialsTokenRequest
			{
				ClientId = _clientSettings.WebClient.ClientId,
				ClientSecret = _clientSettings.WebClient.ClientSecret,
				Address = disco.TokenEndpoint
			};

			var token = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

			if (token.IsError)
			{
				throw token.Exception;
			}

			await _clientAccessTokenCache.SetAsync("WebClientToken", token.AccessToken, token.ExpiresIn, default);

			return token.AccessToken;
		}
	}
}