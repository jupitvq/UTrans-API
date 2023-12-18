using Utrans_API.Models;

namespace Utrans_API.Repository
{
    public interface IVendorRepository
    {
        IEnumerable<Vendors> GetVendors();

        Vendors GetVendorByID(int id);

        void InsertVendor(Vendors vendor);

        void DeleteVendor(int id);

        void UpdateVendor(Vendors vendor);
    }
}
