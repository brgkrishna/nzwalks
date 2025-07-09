using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Domain.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _repository;

        public ImagesController(IMapper mapper, IImageRepository repository)
        {
             _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);

            if(ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileSizeInBytes = imageUploadRequestDto.File.Length,
                    FileName = imageUploadRequestDto.FileName,
                    FileDescription = imageUploadRequestDto.FileDescription
                };

                var imageDomainModelWithPath = await _repository.Upload(imageDomainModel);

                var response = _mapper.Map<ImageUploadRequestDto>(imageDomainModelWithPath);
                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var imageDomainModel = await _repository.Get(id);

            if(imageDomainModel == null)
                return BadRequest(ModelState);

            return Ok(imageDomainModel.FilePath);
        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "UnSupported file extension");
            }

            // if the file size is > 10 megaBytes..throw error
            if(imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "file size is more than 10MB.");
            }
        }
    }
}
