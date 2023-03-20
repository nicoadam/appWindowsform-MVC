using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schad_app.Models
{
    public class InvoiceModel
    {
        private int id;
        private int customerId;
        private int qty;
        private decimal price;
        private decimal totalItbis;
        private decimal subTotal;
        private decimal total;

        [DisplayName("ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DisplayName("CustomerId")]
        public int CustomerID
        {
            get { return customerId; }
            set { customerId = value; }
        }

        [DisplayName("QTY")]
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        [DisplayName("Price")]
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }


        [DisplayName("TotalItbis")]
        public decimal TotalItbis
        {
            get { return totalItbis; }
            set { totalItbis = value; }
        }

        [DisplayName("SubTotal")]
        public decimal SubTotal
        {
            get { return subTotal; }
            set { subTotal = value; }
        }


        [DisplayName("Total")]
        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }




    }
}
