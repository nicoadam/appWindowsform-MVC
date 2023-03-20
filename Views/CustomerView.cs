using schad_app.Controller;
using schad_app.Controllers;
using schad_app.Models;
using schad_app.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using PdfSharp.Drawing;
using System.Diagnostics;
using PdfSharp.Pdf;
using System.Xml.Linq;
using System.Reflection;

namespace schad_app
{
    public partial class CustomerView : Form
    {
        private CustomerModel customer = null;
        private InvoiceModel invoiceModel= null;

        //Singleton pattern (Open a single form instance)
        private static CustomerView instance;
        private BindingSource customersBindingSource;
        private CustomerController customerController= null;
        private InvoiceController invoiceController= null;

        public CustomerView()
        {
            InitializeComponent();
        }

        public CustomerView(CustomerController customerController, InvoiceController invoiceController)
        {
            InitializeComponent();
            this.customerController = customerController; 
            this.invoiceController = invoiceController; 
            this.dataGridView1.DataSource = this.customerController.LoadAllCustomerList();
        }


        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtName.Text.Length>0 && txtAddress.Text.Length > 0)
            {
                var result = MessageBox.Show(Constants.QUESTION, "question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    customer = new CustomerModel();
                    customer.Name = txtName.Text;
                    customer.Adress = txtAddress.Text;
                    customer.Status = cboStatus.Text;
                    customer.Type = cboTypeClient.Text;

                    bool save = this.customerController.saveCustomer(customer);

                    if (save)
                    {
                        MessageBox.Show(Constants.SUCCESS);
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show(Constants.FAILED);
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            cleanClient();
        }

        private void cleanClient()
        {
            txtName.Text = "";
            txtAddress.Text = "";
            cboStatus.Text = Constants.ACTIVO;
            cboTypeClient.Text = Constants.VISITA;
            clearInvoice();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();

        }

        private void loadData()
        {

            cboStatus.Items.Clear();
            cboStatus.Items.Add(Constants.ACTIVO);
            cboStatus.Items.Add(Constants.INACTIVO);
            cboStatus.Text = Constants.ACTIVO;
            cboTypeClient.Items.Clear();
            cboTypeClient.Items.Add(Constants.LEAL);
            cboTypeClient.Items.Add(Constants.VISITA);
            cboTypeClient.Text = Constants.VISITA;

            cleanClient();
            this.dataGridView1.DataSource = this.customerController.LoadAllCustomerList();
        }

        private void Invoices_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lblSubTotal_Click(object sender, EventArgs e)
        {

        }

        private void btnNewInvoice_Click(object sender, EventArgs e)
        {
            clearInvoice();
        }

        private void clearInvoice()
        {
            numericUpDownQTY.Value = 0;
            txtPrice.Text = "0";
            txtTotalITBIS.Text = Constants.TAX.ToString();
            lblSubTotal.Text = "0.00";
            lblTotal.Text = "0.00";
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            getAmountToPay();
        }

        private void txtTotalITBIS_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
            getAmountToPay();
        }

        private void numericUpDownQTY_ValueChanged(object sender, EventArgs e)
        {
            getAmountToPay();
        }

        private void getAmountToPay()
        {
            double price = Double.Parse(txtPrice.Text);
            double cant = Double.Parse(numericUpDownQTY.Value.ToString());
            double tax = (Double.Parse(txtTotalITBIS.Text)/100);

            lblSubTotal.Text = (price * cant).ToString();
            lblTotal.Text = ((price + (price*tax)) * cant).ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtName.Text.Length>0 && txtAddress.Text.Length > 0)
            {
                var result = MessageBox.Show(Constants.QUESTION, "question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    customer = new CustomerModel();
                    customer.Id = int.Parse(txtID.Text);
                    customer.Name = txtName.Text;
                    customer.Adress = txtAddress.Text;
                    customer.Status = cboStatus.Text;
                    customer.Type = cboTypeClient.Text;

                    bool save = this.customerController.updateCustomer(customer);

                    if (save)
                    {
                        MessageBox.Show(Constants.SUCCESS);
                        loadData();

                    }
                    else
                    {
                        MessageBox.Show(Constants.FAILED);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtID.Text.Length > 1)
            {
                var result = MessageBox.Show(Constants.QUESTION, "question",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.customerController.deleteCustomer(int.Parse(txtID.Text));
                    loadData();
                }
            }
            else
            {
                MessageBox.Show("choose one");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
 
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView2.Rows[index];
            numericUpDownQTY.Value = int.Parse(selectedRow.Cells[1].Value.ToString());
            txtPrice.Text = selectedRow.Cells[2].Value.ToString();
            txtTotalITBIS.Text = selectedRow.Cells[3].Value.ToString();
            lblSubTotal.Text = selectedRow.Cells[4].Value.ToString();
            lblTotal.Text = selectedRow.Cells[5].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            txtID.Text = selectedRow.Cells[0].Value.ToString();
            txtName.Text = selectedRow.Cells[1].Value.ToString();
            cboTypeClient.Text = selectedRow.Cells[2].Value.ToString();
            txtAddress.Text = selectedRow.Cells[3].Value.ToString();
            cboStatus.Text = selectedRow.Cells[4].Value.ToString();
            this.dataGridView2.DataSource = this.invoiceController.GetByDataByCustomer(int.Parse(txtID.Text));
        }


        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
                if(txtID.Text.Length > 0 && lblTotal.Text.Length > 0 && txtPrice.Text.Length>0 
                && int.Parse(numericUpDownQTY.Value.ToString())>0)
                 {
                    invoiceModel = new InvoiceModel();
                    invoiceModel.CustomerID = int.Parse(txtID.Text);
                    invoiceModel.Qty = int.Parse(numericUpDownQTY.Value.ToString());
                    invoiceModel.Price = decimal.Parse(txtPrice.Text);
                    invoiceModel.TotalItbis = decimal.Parse(txtTotalITBIS.Text);
                    invoiceModel.SubTotal = decimal.Parse(lblSubTotal.Text);
                    invoiceModel.Total = decimal.Parse(lblTotal.Text);

                    bool save = this.invoiceController.saveInvoice(invoiceModel);
                    if (save)
                    {
                        MessageBox.Show(Constants.SUCCESS);
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show(Constants.FAILED);
                    }
                 }    
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
        if (this.dataGridView2.SelectedRows.Count > 0)
            {
                printOrder();  
            }
        }

        private void printOrder()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("------------------------------------------------------------")
                .Append("INFO PAYMENT ")
                .Append("Name: ").Append(txtName.Text).Append(Environment.NewLine)
                .Append("Type Client: ").Append(cboTypeClient.Text).Append(Environment.NewLine)
                .Append("Qty: ").Append(numericUpDownQTY.Value.ToString()).Append(Environment.NewLine)
                .Append("Price: ").Append(txtPrice.Text).Append(Environment.NewLine)
                .Append("ITBIS: ").Append(txtTotalITBIS.Text).Append(Environment.NewLine)
                .Append("SubTotal: ").Append(lblSubTotal.Text).Append(Environment.NewLine)
                .Append("Total: ").Append(lblTotal.Text).Append(Environment.NewLine)
                .Append("--------------------------------------------------------------\n").Append(Environment.NewLine)
                .Append("Thank for your order!")
                ;


            PdfDocument document = new PdfDocument();
            document.Info.Title = "Bill - " + txtName.Text;
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 6, XFontStyle.Regular);
            gfx.DrawString(sb.ToString(), font, XBrushes.DarkRed,
            new XRect(0, 0, page.Width, page.Height),
            XStringFormats.Center);
            string filename = "bill-"+txtName.Text;
            document.Save(filename);
            Process.Start(filename);
        }

    }




}
