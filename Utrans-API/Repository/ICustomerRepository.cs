using Utrans_API.Models;

namespace Utrans_API.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customers> GetCustomers();

        Customers GetCustomerByID(int id);

        void InsertCustomer(Customers customer);

        void DeleteCustomer(int id);

        void UpdateCustomer(Customers customer);
    }
}
