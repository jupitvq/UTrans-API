using Utrans_API.Models;

namespace Utrans_API.Repository
{
    public interface IBrandRepository
    {
        IEnumerable<Brands> GetBrands();

        Brands GetBrandByID(int id);

        void InsertBrand(Brands brand);

        void DeleteBrand(int id);

        void UpdateBrand(Brands brand);
    }
}
