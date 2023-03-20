using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schad_app.Models
{
    public interface IinvoiceRepository
    {
        bool Add(InvoiceModel invoiceModel);
        IEnumerable<InvoiceModel> GetByValue(int idCustomer);
    }
}
