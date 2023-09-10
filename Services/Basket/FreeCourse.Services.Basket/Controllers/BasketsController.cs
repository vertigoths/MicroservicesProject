using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared_.ControllerBases;
using FreeCourse.Shared_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Basket.Controllers
{
	public class BasketsController : CustomControllerBase
	{
		private readonly IBasketService _basketService;

		private readonly ISharedIdentityService _sharedIdentityService;

		public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
		{
			_basketService = basketService;
			_sharedIdentityService = sharedIdentityService;
		}

		[HttpGet]
		public async Task<IActionResult> GetBasket()
		{
			var response = await _basketService.GetBasketAsync(_sharedIdentityService.GetUserId);

			return CreateActionResultInstance(response);
		}

		[HttpPost]
		public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
		{
			var response = await _basketService.SaveOrUpdate(basketDto);

			return CreateActionResultInstance(response);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteBasket()
		{
			var response = await _basketService.DeleteAsync(_sharedIdentityService.GetUserId);

			return CreateActionResultInstance(response);
		}
	}
}