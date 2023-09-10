using System.Data;
using Dapper;
using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared_.Dtos;
using Npgsql;

namespace FreeCourse.Services.Discount.Services
{
	public class DiscountService : IDiscountService
	{
		private readonly IConfiguration _configuration;

		private readonly IDbConnection _dbConnection;

		public DiscountService(IConfiguration configuration)
		{
			_configuration = configuration;
			_dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQL"));
		}

		public async Task<Response<NoContent>> Delete(int id)
		{
			var status = await _dbConnection.ExecuteAsync($"delete from discount where id={id}");

			if (status > 0)
			{
				return Response<NoContent>.Success(204);
			}

			return Response<NoContent>.Fail("Discount not found!", 404);
		}

		public async Task<Response<List<Models.Discount>>> GetAll()
		{
			var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");

			var response = Response<List<Models.Discount>>.Success(discounts.ToList(), 200);

			return response;
		}

		public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
		{
			var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount where " +
			$"code='{code}' and userid='{userId}'");

			var discount = discounts.SingleOrDefault();

			if (discount == null)
			{
				return Response<Models.Discount>.Fail("Discount not found!", 404);
			}

			return Response<Models.Discount>.Success(discount, 200);
		}

		public async Task<Response<Models.Discount>> GetById(int id)
		{
			var discounts = await _dbConnection.QueryAsync<Models.Discount>($"select * from discount where id={id}");

			var discount = discounts.SingleOrDefault();

			if (discount == null)
			{
				return Response<Models.Discount>.Fail("Discount not found!", 404);
			}

			return Response<Models.Discount>.Success(discount, 200);
		}

		public async Task<Response<NoContent>> Save(Models.Discount discount)
		{
			var status = await _dbConnection.ExecuteAsync("insert into discount (userid, rate, code) " +
			                                              $"values('{discount.UserId}',{discount.Rate},'{discount.Code}')");

			if (status > 0)
			{
				return Response<NoContent>.Success(204);
			}

			return Response<NoContent>.Fail("An error occured!", 500);
		}

		public async Task<Response<NoContent>> Update(Models.Discount discount)
		{
			var status = await _dbConnection.ExecuteAsync($"update discount set " +
			                                              $"userid='{discount.UserId}', code={discount.Code}, " +
			                                              $"rate={discount.Rate} where id='{discount.Id}'");

			if (status > 0)
			{
				return Response<NoContent>.Success(204);
			}

			return Response<NoContent>.Fail("Discount not found!", 404);
		}
	}
}