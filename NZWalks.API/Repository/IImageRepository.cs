using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Domain.DTO;

namespace NZWalks.API.Repository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image imageUploadRequestDto);
        Task<Image?> Get(Guid id);
    }
}
