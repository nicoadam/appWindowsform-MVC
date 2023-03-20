using schad_app.Models;
using schad_app.Repositories;
using System.Collections.Generic;

namespace schad_app.Controllers
{
    public class InvoiceController
    {
        private IinvoiceRepository iinvoiceRepository;
        private IEnumerable<InvoiceModel> invoices;

        public InvoiceController(string connectionString)
        {
            this.iinvoiceRepository = new InvoiceRepository(connectionString);
        }

        public IEnumerable<InvoiceModel> GetByDataByCustomer(int idCustomer)
        {
            invoices= this.iinvoiceRepository.GetByValue(idCustomer);
            return invoices;
        }

        public bool saveInvoice(InvoiceModel invoice)
        {
            return this.iinvoiceRepository.Add(invoice);
        }
    }
}
