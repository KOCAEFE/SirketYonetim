using SirketYonetim.Models.Product;

namespace SirketYonetim.Services.Abstract
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAllAsync();

        Task<ProductViewModel> GetByIdAsync(Guid id);

        Task AddAsync(ProductCreateViewModel model);

        Task UpdateAsync(ProductUpdateViewModel model);

        Task DeleteAsync(Guid id);
    }
}
