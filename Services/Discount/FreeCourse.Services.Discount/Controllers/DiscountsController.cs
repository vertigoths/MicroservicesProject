using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared_.ControllerBases;
using FreeCourse.Shared_.Dtos;
using FreeCourse.Shared_.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Discount.Controllers
{
	public class DiscountsController : CustomControllerBase
	{
		private IDiscountService _discountService;

		private ISharedIdentityService _identityService;

		public DiscountsController(IDiscountService discountService, ISharedIdentityService identityService)
		{
			_discountService = discountService;
			_identityService = identityService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var discounts = await _discountService.GetAll();

			return CreateActionResultInstance(discounts);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var discount = await _discountService.GetById(id);

			return CreateActionResultInstance(discount);
		}

		[Route("/api/[controller]/[action]/{code}")]
		[HttpGet]
		public async Task<IActionResult> GetByCode(string code)
		{
			var userId = _identityService.GetUserId;

			var discount = await _discountService.GetByCodeAndUserId(code, userId);

			return CreateActionResultInstance(discount);
		}

		[HttpPost]
		public async Task<IActionResult> Save(Models.Discount discount)
		{
			var response = await _discountService.Save(discount);

			return CreateActionResultInstance(response);
		}

		[HttpPut]
		public async Task<IActionResult> Update(Models.Discount discount)
		{
			var response = await _discountService.Update(discount);

			return CreateActionResultInstance(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var response = await _discountService.Delete(id);

			return CreateActionResultInstance(response);
		}
	}
}