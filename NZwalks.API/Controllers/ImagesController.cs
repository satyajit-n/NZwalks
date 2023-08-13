using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if(ModelState.IsValid)
            {
                //Dto to model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInByte = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };

                //upload the file to repository
                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg",".jpeg",".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Format");
            }
            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("fille", "File Size Exceeded the limit");
            }
        }
    }
}
