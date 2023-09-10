using FreeCourse.Shared_.ControllerBases;
using FreeCourse.Shared_.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
	public class FakePaymentsController : CustomControllerBase
	{
		[HttpPost]
		public IActionResult ReceivePayment()
		{
			var response = Response<NoContent>.Success(200);

			return CreateActionResultInstance<NoContent>(response);
		}
	}
}