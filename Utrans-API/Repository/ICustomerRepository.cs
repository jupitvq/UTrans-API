using Utrans_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
