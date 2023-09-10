using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared_.Dtos;

namespace FreeCourse.Services.Basket.Services
{
	public interface IBasketService
	{
		Task<Response<BasketDto>> GetBasketAsync(string userId);

		Task<Response<bool>> SaveOrUpdate(BasketDto basket);

		Task<Response<bool>> DeleteAsync(string userId);
	}
}
