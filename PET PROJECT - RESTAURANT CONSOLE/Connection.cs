using MySql.Data.MySqlClient;
using PET_PROJECT___RESTAURANT_CONSOLE.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PET_PROJECT___RESTAURANT_CONSOLE
{
    public class Connection
    {


        //global variable
        private MySql.Data.MySqlClient.MySqlConnection _connect;
        //constructor

        private static string myConnectionString = "server=localhost;uid=root; pwd=admin1;database=pet_proj";
        //private object userinput;

        public Connection()
        {
            //initialize constructor
            try
            {
                _connect = new MySql.Data.MySqlClient.MySqlConnection();
                _connect.ConnectionString = myConnectionString;
                _connect.Open();
                Console.WriteLine("Connection successful!");
                Console.WriteLine();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }





        //public void SaveToCustomer()
        //{
        //    var stm = "INSERT INTO tbl_customer (fname,lname)" + " values ('menisha','jugoo')";
        //    var cmd = new MySqlCommand(stm, _connect);
        //    cmd.Prepare();
        //    cmd.ExecuteNonQuery();
        //}


        //public void SaveToOrder()
        //{
        //    var stm = "INSERT INTO tbl_order (order_details,customer_id,total_price) values ('chicken burger, 2, 195', 2,390)";
        //    var cmd = new MySqlCommand(stm, _connect);

        //    cmd.Prepare();
        //    cmd.ExecuteNonQuery();
        //}



        public Customer InsertCustomer(string fname, string lname)
        {
            var statementInsrt = $"INSERT INTO tbl_customer(fname, lname) values('{fname}','{lname}')";
            // var string2 = "sfds" + fname + "dfsdf";
            // var string3 = string.Concat("asfdf", fname, "fdgdfg");

            var cmd = new MySqlCommand(statementInsrt, _connect);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            return FetchCustomerByName(fname, lname);


        }
        public void InsertOrderDetails(int total, string fname, string lname, string menulistObj)
        {
            //call fetchCustomerByName 
            var customer = FetchCustomerByName(fname, lname);

            if (customer.ID <= 0)
            {
                //create customer                 
                customer = InsertCustomer(fname, lname);               

            }                     

            var stmTotal = $"INSERT INTO tbl_order(order_details,customer_id,total_price) values('{menulistObj}', { customer.ID},{total})";   
            var cmd = new MySqlCommand(stmTotal, _connect);
            cmd.Prepare();
            cmd.ExecuteNonQuery();           

        }
        public Customer FetchCustomerByName(string fname, string lname)
        {
            Customer cust = new Customer();

            var stsSelect = $"SELECT * FROM tbl_customer where fname='{fname}' and lname = '{lname}'";
            var cmd2 = new MySqlCommand(stsSelect, _connect);
            cmd2.Parameters.AddWithValue(@fname, fname);
            cmd2.Parameters.AddWithValue(@lname, lname);
            cmd2.Prepare();
            cmd2.ExecuteNonQuery();
            using (var dr = cmd2.ExecuteReader())
            {
                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        cust.ID = dr.GetInt32(0);
                        cust.FirstName = dr.GetString(1);
                        cust.LastName = dr.GetString(2);

                    }


                }

       
            return cust;     }
        }


        public void DisplayReport()
        {
            var stsSelectAllOrder = $"SELECT * FROM tbl_order ord, tbl_customer cust where cust.customer_id=ord.customer_id ";
            var cmd2 = new MySqlCommand(stsSelectAllOrder, _connect);
            cmd2.Prepare();
            cmd2.ExecuteNonQuery();
            Console.WriteLine(stsSelectAllOrder);


        }


    }
}