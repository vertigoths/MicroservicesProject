using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeCourse.Shared_.Dtos;

namespace FreeCourse.Shared_.ControllerBases
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomControllerBase : ControllerBase
	{
		public IActionResult CreateActionResultInstance<T>(Response<T> response)
		{
			return new ObjectResult(response)
			{
				StatusCode = response.StatusCode
			};
		}
	}
}