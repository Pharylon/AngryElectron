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

namespace BetterContactTracker
{
    public partial class Form1 : Form
    {
        SqlConnection cm = new SqlConnection("Data Source=SuperHappy;Initial Catalog=PrimitiveContact;Integrated Security=True");
        //SqlCommand Command = new SqlCommand("");

        private bool DeleteContact;
        private bool EditContact;
        private bool AddContact;
        public Form1()
        {
            InitializeComponent();
        }
        private void ResetAddEditDelete()
        { 
            DeleteContact= false;
            EditContact= false;
            AddContact= false;
        }
        private void MakeForm2()
        {
            Form2 child = new Form2(AddContact,EditContact,DeleteContact);
            child.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddContact = true;
            MakeForm2();
            ResetAddEditDelete();
        }

    }
}
