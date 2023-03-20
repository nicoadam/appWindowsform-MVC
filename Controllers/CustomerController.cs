using schad_app.Models;
using schad_app.Repositories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace schad_app.Controller
{
    public class CustomerController
    {

        private ICustomerRepository repositoryCustomer;
        private IEnumerable<CustomerModel> custList;

        public CustomerController(string connectionString)
        {
            this.repositoryCustomer = new CustomerRepository(connectionString);            
        }

        public IEnumerable<CustomerModel> LoadAllCustomerList()
        {
            custList = repositoryCustomer.GetAll();
            return custList;
        }
        public IEnumerable<CustomerModel> SearchCustomer(int id)
        {

            if(id > 0)
            {
                custList = repositoryCustomer.GetByValue(id);
            }
            else 
            {
                custList= repositoryCustomer.GetAll();
            }
            return custList;
        }
        
        public bool saveCustomer(CustomerModel customer)
        {
            return this.repositoryCustomer.Add(customer);
        }

        public bool updateCustomer(CustomerModel customer)
        {
            return this.repositoryCustomer.Edit(customer);
        }

        public bool deleteCustomer(int id)
        {
            return this.repositoryCustomer.Delete(id);
        }
    }
}
