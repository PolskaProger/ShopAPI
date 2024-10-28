using ShopAPI.Domain.DTO;

namespace ShopAPI.services
{
    public interface IImageService
    {
        Task<ImageDto> UploadImageAsync(ImageUploadDto imageUploadDto);
        Task<ImageDto> GetImageAsync(int imageId);
        Task<bool> DeleteImageAsync(int imageId);
    }

}
