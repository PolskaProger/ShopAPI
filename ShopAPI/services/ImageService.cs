using ShopAPI.Domain.DB;
using ShopAPI.Domain.DTO;
using ShopAPI.Domain.Entities;

namespace ShopAPI.services
{
    public class ImageService : IImageService
    {
        private readonly AppDbContext _context;

        public ImageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ImageDto> UploadImageAsync(ImageUploadDto imageUploadDto)
        {
            if (imageUploadDto.File == null || imageUploadDto.File.Length == 0)
                throw new ArgumentException("Файл изображения не может быть пустым");

            using var memoryStream = new MemoryStream();
            await imageUploadDto.File.CopyToAsync(memoryStream);

            var image = new Image
            {
                Data = memoryStream.ToArray(),
                ContentType = imageUploadDto.File.ContentType,
                FileName = imageUploadDto.File.FileName,
                Size = imageUploadDto.File.Length,
                ProductId = imageUploadDto.ProductId
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return new ImageDto
            {
                Id = image.Id,
                ContentType = image.ContentType,
                FileName = image.FileName,
                Size = image.Size,
                ProductId = image.ProductId,
                Data = image.Data
            };
        }

        public async Task<ImageDto> GetImageAsync(int imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null) return null;

            return new ImageDto
            {
                Id = image.Id,
                ContentType = image.ContentType,
                FileName = image.FileName,
                Size = image.Size,
                ProductId = image.ProductId,
                Data = image.Data
            };
        }

        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null) return false;

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
