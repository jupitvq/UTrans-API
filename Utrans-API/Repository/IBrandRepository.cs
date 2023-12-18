using Utrans_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
