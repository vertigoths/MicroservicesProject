using Duende.IdentityServer.Validation;
using FreeCourse.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace FreeCourse.IdentityServer.Services
{
	public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
		{
			var existsUser = await _userManager.FindByEmailAsync(context.UserName);

			if (existsUser == null)
			{
				var errors = new Dictionary<string, object>();

				errors.Add("errors", new List<string> { "Account not found!" });

				context.Result.CustomResponse = errors;

				return;
			}

			var passwordCheck = await _userManager.CheckPasswordAsync(existsUser, context.Password);

			if (!passwordCheck)
			{
				var errors = new Dictionary<string, object>();

				errors.Add("errors", new List<string> { "Password is wrong!" });

				context.Result.CustomResponse = errors;

				return;
			}

			context.Result = new GrantValidationResult(existsUser.Id.ToString(),
				OidcConstants.AuthenticationMethods.Password);
		}
	}
}