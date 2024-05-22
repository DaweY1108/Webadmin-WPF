using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;
using AdminPanel.Model;
using System.Windows;

namespace Webadmin.Model
{
    public class UserModel
    {
        public class Record
        {
            public int _id { get; set; } = 0;
            public string _username { get; set; } = "";
            public string _first_name { get; set; } = "";
            public string _last_name { get; set; } = "";
            public string _email { get; set; } = "";
            public string _phone { get; set; } = "";
            public DateTime _reg_date { get; set; } = new DateTime();
            public string _street { get; set; } = "";
            public string _city { get; set; } = "";
            public string _postal_code { get; set; } = "";
            public string _country { get; set; } = "";
        }

        public static Record GetUser(string username)
        {
            Record record = new Record();
            string query = "SELECT enUser.id, username, first_name, last_name, email, phone, reg_date, street, city, postal_code, country FROM enUser INNER JOIN enAddress ON enUser.address_id = enAddress.id WHERE username = @username";
            try { 
                using (SqlConnection connection = Database.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                record._id = reader.GetInt32(0);
                                record._username = reader.GetString(1);
                                record._first_name = reader.GetString(2);
                                record._last_name = reader.GetString(3);
                                record._email = reader.GetString(4);
                                record._phone = reader.GetString(5);
                                record._reg_date = reader.GetDateTime(6);
                                record._street = reader.GetString(7);
                                record._city = reader.GetString(8);
                                record._postal_code = reader.GetString(9);
                                record._country = reader.GetString(10);
                            }
                        }
                    }
                }
        } catch (Exception e)
        {
                MessageBox.Show(e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
        }
            return record;
        }

        public static bool CheckUser(string username, string password)
        {
            string query = "SELECT * FROM enUser WHERE username = @username AND password = @password";
            try
            {
                using (SqlConnection connection = Database.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return true;
                            }
                        }
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
                return false;
            }   
            return false;
        }   

        public static bool RemoveUser(string username)
        {
            string query = "DELETE FROM users WHERE username = @username";
            try
            {
                using (SqlConnection connection = Database.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
                return false;
            }
        }
        
        public static IEnumerable<Record> GetUsers()
        {
            List<Record> records = new List<Record>();
            string query = "SELECT enUser.id, username, first_name, last_name, email, phone, reg_date, street, city, postal_code, country FROM enUser INNER JOIN enAddress ON enUser.address_id = enAddress.id";
            try
            {
                using (SqlConnection connection = Database.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Record record = new Record();
                                record._id = reader.GetInt32(0);
                                record._username = reader.GetString(1);
                                record._first_name = reader.GetString(2);
                                record._last_name = reader.GetString(3);
                                record._email = reader.GetString(4);
                                record._phone = reader.GetString(5);
                                record._reg_date = reader.GetDateTime(6);
                                record._street = reader.GetString(7);
                                record._city = reader.GetString(8);
                                record._postal_code = reader.GetString(9);
                                record._country = reader.GetString(10);
                                records.Add(record);
                            }
                        }
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
            }
            return records;
        }
    }
}
