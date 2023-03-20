using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace schad_app.Models
{
    public class CustomerModel
    {
        private int id;
        private string custName;
        private string adress;
        private string status;
        private string customerTypeId;

         [DisplayName("ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Customer name is requerid")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Customer name must be between 3 and 50 characters")]
        public string Name
        {
            get { return custName; }
            set { custName = value; }
        }

        [DisplayName("Customer Type")]
        [Required(ErrorMessage = "client type is requerid")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Client type must be between 3 and 50 characters")]
        public string Type
        {
            get { return customerTypeId; }
            set { customerTypeId = value; }
        }

        [DisplayName("Adress")]
        [Required(ErrorMessage = "Client Adress is requerid")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Client adress must be between 3 and 50 characters")]
        public string Adress
        {
            get { return adress; }
            set { adress = value; }
        }


        [DisplayName("Status")]
        [Required(ErrorMessage = "Status is requerid")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Status must be between 3 and 10 characters")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

    }
}
