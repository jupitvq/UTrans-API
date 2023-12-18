using Utrans_API.Models;


namespace Utrans_API.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Products> GetProducts();

        Products GetProductByID(int id);

        void InsertProduct(Products Product);

        void DeleteProduct(int id);

        void UpdateProduct(Products Product);
    }
}
