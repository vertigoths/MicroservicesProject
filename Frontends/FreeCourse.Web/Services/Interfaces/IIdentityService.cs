using FreeCourse.Shared_.Dtos;
using FreeCourse.Web.Models;
using IdentityModel.Client;

namespace FreeCourse.Web.Services.Interfaces
{
	public interface IIdentityService
	{
		Task<Response<bool>> SignIn(SigninInput input);

		Task<TokenResponse> GetAccessTokenByRefreshToken();

		Task RevokeRefreshToken();
	}
}