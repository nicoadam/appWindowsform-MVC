using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using schad_app.Models;
using schad_app.Util;

namespace schad_app.Repositories
{

    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        private StringBuilder sb = new StringBuilder();

        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        private string getStatusFromDB(string ind)
        {
            if (ind.ToUpper().Equals(Constants.TRUE))
            {
                return Constants.ACTIVO;
            }
            else
            {
                return Constants.INACTIVO;
            }
        }

        private Byte getStatusFromModel(string ind)
        {
            if (ind.ToUpper().Equals(Constants.ACTIVO))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        private int getTypeFromDB(string ind)
        {
            int id = 0;
            using (var connection = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                sb.Length = 0;
                sb.Append("select id, description from CustomerTypes");
                command.CommandText = sb.ToString();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader[1].ToString().ToUpper().Equals(ind))
                        {
                            id = (int)reader[0];
                            break;
                        }
                    }
                }
            }
            return id;
        }

        private string getTypeFromModel(string ind)
        {
            if (ind.ToUpper().Equals(Constants.LEAL))
            {
                return Constants.LEAL;
            }
            else
            {
                return Constants.VISITA;
            }
        }


        public bool Add(CustomerModel customerModel)
        {
            bool save = false;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Customers values (@name, @adress, @status, @type)";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = customerModel.Name;
                command.Parameters.Add("@adress", SqlDbType.NVarChar).Value = customerModel.Adress;
                command.Parameters.Add("@status", SqlDbType.Bit).Value = getStatusFromModel(customerModel.Status);
                command.Parameters.Add("@type", SqlDbType.Int).Value = getTypeFromDB(customerModel.Type);
                if (command.ExecuteNonQuery() > 0)
                {
                    save = true;
                }
                else
                {
                    save = false;
                }
                return save;
            }
        }
        public bool Delete(int id)
        {
            bool delete = false;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                sb.Length = 0;
                sb.Append("delete from Customers where id=@id");
                command.CommandText = sb.ToString();
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                if (command.ExecuteNonQuery() > 0)
                {
                    delete = true;
                }
                else
                {
                    delete = false;
                }
                return delete;
            }
        }
        public bool Edit(CustomerModel customerModel)
        {
            bool update = false;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                sb.Length = 0;
                sb.Append("update Customers set custname=@name, adress=@adress, status=@status, customerTypeId=@type ")
                    .Append("where id=@id");
                command.CommandText = sb.ToString();
                command.Parameters.Add("@id", SqlDbType.Int).Value = customerModel.Id;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = customerModel.Name;
                command.Parameters.Add("@adress", SqlDbType.NVarChar).Value = customerModel.Adress;
                command.Parameters.Add("@status", SqlDbType.NVarChar).Value = getStatusFromModel(customerModel.Status);
                command.Parameters.Add("@type", SqlDbType.Int).Value = getTypeFromDB(customerModel.Type);
                if (command.ExecuteNonQuery() > 0)
                {
                    update = true;
                }
                else
                {
                    update = false;
                }
                return update;
            }
        }

        public IEnumerable<CustomerModel> GetAll()
        {
  
            var custList = new List<CustomerModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                sb.Length = 0;
                sb.Append("select c.id, c.CustName, c.Adress, c.Status, t.Description ")
                    .Append("from Customers c, CustomerTypes t ")
                    .Append("where c.CustomerTypeId=t.Id");

                command.CommandText = sb.ToString();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customerModel = new CustomerModel();
                        customerModel.Id = (int)reader[0];
                        customerModel.Name = reader[1].ToString();
                        customerModel.Adress = reader[2].ToString();
                        customerModel.Status = getStatusFromDB(reader[3].ToString());
                        customerModel.Type = reader[4].ToString();
                        custList.Add(customerModel);
                    }
                }
            }
            return custList;
        }

        public IEnumerable<CustomerModel> GetByValue(int id)
        {
            var custList = new List<CustomerModel>();
            int clientId = id;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                sb.Length = 0;
                sb.Append("select c.id, c.CustName, c.Adress, c.Status, t.Description ")
                    .Append("from Customers c, CustomerTypes t")
                    .Append("where c.CustomerTypeId=t.Id")
                    .Append("and c.id=@id");

                command.CommandText = sb.ToString();
                command.Parameters.Add("@id", SqlDbType.Int).Value = clientId;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customerModel = new CustomerModel();
                        customerModel.Id = (int)reader[0];
                        customerModel.Name = reader[1].ToString();
                        customerModel.Adress = reader[2].ToString();
                        customerModel.Status = reader[3].ToString();
                        customerModel.Type = reader[4].ToString();
                        custList.Add(customerModel);
                    }
                }
            }
            return custList;
        }
    }
}
