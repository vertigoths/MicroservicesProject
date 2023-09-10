using System.Text.Json;
using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared_.Dtos;

namespace FreeCourse.Services.Basket.Services
{
	public class BasketService : IBasketService
	{
		private readonly RedisService _redisService;

		public BasketService(RedisService redisService)
		{
			_redisService = redisService;
		}

		public async Task<Response<bool>> DeleteAsync(string userId)
		{
			var status = await _redisService.GetDb().KeyDeleteAsync(userId);

			return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found!", 404);
		}

		public async Task<Response<BasketDto>> GetBasketAsync(string userId)
		{
			var exists = await _redisService.GetDb().StringGetAsync(userId);

			if (string.IsNullOrEmpty(exists))
			{
				return Response<BasketDto>.Fail("Basket not found!", 404);
			}

			var basketDto = JsonSerializer.Deserialize<BasketDto>(exists);

			var response = Response<BasketDto>.Success(basketDto, 200);

			return response;
		}

		public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
		{
			var basket = JsonSerializer.Serialize<BasketDto>(basketDto);

			var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, basket);

			return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket couldn't update or save!", 500);
		}
	}
}
