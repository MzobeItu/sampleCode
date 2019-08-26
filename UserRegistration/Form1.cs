using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UserRegistration
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=DVTLHC9CKC2\SQLEXPRESS;Initial Catalog=UserRegistrationDB;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
                MessageBox.Show("Please fill manatory fields");
            if (txtPassword.Text != txtConfirmPassword.Text)
                MessageBox.Show("Password does not match");
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("UserAdd", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                   
                    //sqlCmd.Parameters.ToString("@UserId", txtUserId.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Contact", txtContact.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    sqlCmd.ExecuteScalar();
                    MessageBox.Show("Registration is successful");
                    Clear();
                }
            }
        }
        void Clear()
        {
            txtFirstName.Text = txtLastName.Text = txtContact.Text = txtAddress.Text = txtUsername.Text = txtPassword.Text = txtConfirmPassword.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                //e.Cancel = true;
                //txtFirstName.Focus();
                epFirst.SetError(txtFirstName, "Please enter your first name!");
            }
            else
            {
                //e.Cancel = true;
                epFirst.SetError(txtFirstName, string.Empty);
            }
            
        }

        private void txtContact_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtContact.Text))
            {
                epContact.SetError(txtContact, " Please enter valid Cellphone number");
                txtContact.Focus();
            }
            else
            {
                epContact.SetError(txtContact, string.Empty);
            }
        }
    }
}
