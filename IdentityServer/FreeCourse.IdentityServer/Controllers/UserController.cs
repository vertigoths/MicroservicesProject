using Duende.IdentityServer;
using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using FreeCourse.Shared_.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FreeCourse.IdentityServer.Controllers
{
	[Authorize(IdentityServerConstants.LocalApi.PolicyName)]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpApplicationUserDto signUpApplicationUser)
		{
			var newUser = new ApplicationUser()
			{
				UserName = signUpApplicationUser.UserName,
				Email = signUpApplicationUser.Email,
				City = signUpApplicationUser.City,
			};

			var result = await _userManager.CreateAsync(newUser, signUpApplicationUser.Password);

			var response = Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400);

			if (!result.Succeeded)
			{
				return BadRequest(response);
			}

			return NoContent();
		}

		[HttpGet]
		public async Task<IActionResult> GetUser()
		{
			var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

			if (userIdClaim == null)
			{
				return BadRequest();
			}

			var user = await _userManager.FindByIdAsync(userIdClaim.Value);

			if (user == null)
			{
				return BadRequest();
			}

			return Ok(new { Id = user.Id, UserName = user.UserName, Email = user.Email, City = user.City });
		}
	}
}