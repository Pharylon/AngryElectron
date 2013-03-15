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
    public partial class Form2 : Form
    {
        SqlConnection cm = new SqlConnection("Data Source=SuperHappy;Initial Catalog=PrimitiveContact;Integrated Security=True");
        SqlCommand Command = new SqlCommand("");

        public bool _AddContact;
        public bool _EditContact;
        public bool _DeletContact;

        private void ExecuteNonQuerySqlCommand(string cmd)
        {
            cm.Open();
            Command.Connection = cm;
            Command.CommandText = cmd;
            Command.ExecuteNonQuery();
            cm.Close();
        }
        public Form2(bool Add, bool Edit, bool Delete)
        {
            InitializeComponent();
            _AddContact = Add;
            _EditContact = Edit;
            _DeletContact = Delete;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (_AddContact)
            {
                ContactAdd();
            }
            if (_EditContact)
            { 
                //ContactEdit();
            }
            this.Close();
        }
        private void ContactAdd()
        {
            PhoneContactAdd(PhoneAdd(), PersonAdd(AddressAdd()));
        }
        private int AddressAdd()
        {
            int ZipCode = Convert.ToInt32(textBox5.Text);
            String insert = "Insert into Address(Street, City, zip) values ('" + textBox4.Text + "','" + textBox3.Text + "','" + ZipCode + "')";
            ExecuteNonQuerySqlCommand(insert);
            Command.Connection = cm;
            Command.CommandText = "select top(1) Address_ID From Address ORDER BY Address_ID DESC";
            cm.Open();
            int AddressId=Convert.ToInt32((int)Command.ExecuteScalar());
            cm.Close();
            return AddressId;
        }
        private int PersonAdd(int AddressID)
        {
            String insert = " Insert into Person ( F_Name, L_Name, Address_ID) values ('" + textBox1.Text + "','" + textBox2.Text + "','"+AddressID+"')";
            ExecuteNonQuerySqlCommand(insert);
            Command.Connection = cm;
            Command.CommandText = "select top(1) Person_ID From Person ORDER BY Person_ID DESC";
            cm.Open();
            int PersonID = Convert.ToInt32((int)Command.ExecuteScalar());
            cm.Close();
            return PersonID;
        }
        private int PhoneAdd()
        {
            ulong PhoneDigits = Convert.ToUInt64 (textBox6.Text);
            String insert = "Insert into Phone( Phone_Number) values (" +( PhoneDigits) + ")";
            ExecuteNonQuerySqlCommand(insert);
            Command.Connection = cm;
            cm.Open();
            Command.CommandText = "select top(1) Phone_ID From Phone ORDER BY Phone_ID DESC";
            int PhoneID= Convert.ToInt32(Command.ExecuteScalar());
            cm.Close();
            return PhoneID;

        }
        private void PhoneContactAdd(int PhoneID, int PersonID)
        {
            String insert = "Insert into Person_Phone (Phone_ID, Person_ID) values ('" + PhoneID + "','" + PersonID + "')";
            ExecuteNonQuerySqlCommand(insert);
        }
    }
}
