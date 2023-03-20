using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schad_app.Models
{
    public interface ICustomerRepository
    {
        bool Add(CustomerModel customerModel);
        bool Edit(CustomerModel customerModel);
        bool Delete(int id);
        IEnumerable<CustomerModel> GetAll();
        IEnumerable<CustomerModel> GetByValue(int id);
    }
}
