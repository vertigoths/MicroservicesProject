using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FreeCourse.Shared_.Services
{
	public class SharedIdentityService : ISharedIdentityService
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public SharedIdentityService(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public string GetUserId => _contextAccessor.HttpContext.User.FindFirst("sub").Value;
	}
}