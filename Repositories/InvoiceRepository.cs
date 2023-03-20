using schad_app.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schad_app.Repositories
{
    public class InvoiceRepository : BaseRepository, IinvoiceRepository
    {
        private StringBuilder sb = new StringBuilder();

        public InvoiceRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

       public bool Add(InvoiceModel invoiceModel)
        {
            bool save = false;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                sb.Length = 0;
                sb.Append("insert into Invoice values (@CustomerID, @TotalItbis, @SubTotal, @Total)");
                command.CommandText = sb.ToString();
                command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = invoiceModel.CustomerID;
                command.Parameters.Add("@TotalItbis", SqlDbType.Decimal).Value = invoiceModel.TotalItbis;
                command.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = invoiceModel.SubTotal;
                command.Parameters.Add("@Total", SqlDbType.Decimal).Value = invoiceModel.Total;
                if (command.ExecuteNonQuery() > 0)
                {
                    sb.Length = 0;
                    sb.Append("insert into InvoiceDetail values (@CustomerID_D, @Qty_D, @Price_D, @TotalItbis_D, @SubTotal_D, @Total_D)");
                    command.CommandText = sb.ToString();
                    command.Parameters.Add("@CustomerID_D", SqlDbType.Int).Value = invoiceModel.CustomerID;
                    command.Parameters.Add("@Qty_D", SqlDbType.Int).Value = invoiceModel.Qty;
                    command.Parameters.Add("@Price_D", SqlDbType.Decimal).Value = invoiceModel.Price;
                    command.Parameters.Add("@TotalItbis_D", SqlDbType.Decimal).Value = invoiceModel.TotalItbis;
                    command.Parameters.Add("@SubTotal_D", SqlDbType.Decimal).Value = invoiceModel.SubTotal;
                    command.Parameters.Add("@Total_D", SqlDbType.Decimal).Value = invoiceModel.Total;
                    if (command.ExecuteNonQuery() > 0)
                    {
                        save = true;
                    }
                }
                else
                {
                    save = false;
                }
                return save;
            }
        }

        public IEnumerable<InvoiceModel> GetByValue(int idCustomer)
        {
           var invoices = new List<InvoiceModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                sb.Length = 0;
                sb.Append("select i.customerId, d.qty, d.price, i.totalItbis, i.subtotal, i.total ")
                    .Append("from Invoice i , InvoiceDetail d ")
                    .Append("where i.CustomerId=d.CustomerId ")
                    .Append("and i.CustomerId=@idCustomer");

                command.CommandText = sb.ToString();
                command.Parameters.Add("@idCustomer", SqlDbType.Int).Value = idCustomer;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var invoiceModel = new InvoiceModel();
                        invoiceModel.CustomerID = (int)reader[0];
                        invoiceModel.Qty = (int)reader[1];
                        invoiceModel.Price = (decimal)reader[2];
                        invoiceModel.TotalItbis = (decimal)reader[3];
                        invoiceModel.SubTotal = (decimal)reader[4];
                        invoiceModel.Total = (decimal)reader[5];
                        invoices.Add(invoiceModel);
                    }
                }
            }
            return invoices;
        }
    }
}
