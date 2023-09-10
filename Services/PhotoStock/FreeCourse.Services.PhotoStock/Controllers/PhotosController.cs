using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared_.ControllerBases;
using FreeCourse.Shared_.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.PhotoStock.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class PhotosController : CustomControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
		{
			if (photo != null && photo.Length > 0)
			{
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

				await using var stream = new FileStream(path, FileMode.Create);
				await photo.CopyToAsync(stream, cancellationToken);

				var returnPath = "photos/" + photo.FileName;

				var photoDto = new PhotoDto() { Url = returnPath };

				var response = Response<PhotoDto>.Success(photoDto, 200);

				return CreateActionResultInstance(response);
			}

			return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty!", 400));
		}

		[HttpDelete]
		public IActionResult PhotoDelete(string photoUrl)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);

			if (!System.IO.File.Exists(path))
			{
				return CreateActionResultInstance(Response<NoContent>.Fail("Photo not found!", 404));
			}

			System.IO.File.Delete(path);

			var response = Response<NoContent>.Success(204);

			return CreateActionResultInstance(response);
		}
	}
}