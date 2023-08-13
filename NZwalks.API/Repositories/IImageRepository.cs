using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public interface IImageRepository
    {
        Task <Image> Upload(Image image);
    }
}
