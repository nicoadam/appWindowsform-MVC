using schad_app.Controller;
using schad_app.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace schad_app
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            CustomerView view = new CustomerView(new CustomerController(sqlConnectionString), new InvoiceController(sqlConnectionString));
            Application.Run((Form)view);


        }
    }
}
