using FreeCourse.Web.Exceptions;
using FreeCourse.Web.Services;
using FreeCourse.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace FreeCourse.Web.Handler
{
	public class ClientCredentialTokenHandler : DelegatingHandler
	{
		private readonly IClientCredentialTokenService _clientCredentialTokenService;

		public ClientCredentialTokenHandler(IClientCredentialTokenService clientCredentialTokenService)
		{
			_clientCredentialTokenService = clientCredentialTokenService;
		}

		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var token = await _clientCredentialTokenService.GetToken();

			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response =  await base.SendAsync(request, cancellationToken);

			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				throw new UnauthorizedException();
			}

			return response;
		}
	}
}