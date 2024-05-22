using AdminPanel.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Webadmin.Model
{
    public class OrderModel
    {
        public class Record
        {
            public int _id { get; set; }
            public DateTime _date { get; set; }
            public int _amount { get; set; }
            public string _state { get; set; }
            public string _title { get; set; }
            public decimal _price { get; set; }
            public string _first_name { get; set; }
            public string _last_name { get; set; }
            public string _phone { get; set; }
            public string _email { get; set; }
            public string _country { get; set; }
            public string _postal_code { get; set; }
            public string _city { get; set; }
            public string _street { get; set; }
        }

        public static IEnumerable<Record> GetOrders()
        {
            List<Record> orders = new List<Record>();
            string query = "SELECT enOrder.id, enOrder.date, enOrder.amount, enState.name as state, enGame.title, enGame.price, enUser.first_name, enUser.last_name, enUser.phone, enUser.email, enAddress.country, enAddress.postal_code, enAddress.city, enAddress.street FROM (((enOrder INNER JOIN enUser ON enOrder.user_id = enUser.id) INNER JOIN enGame ON enOrder.game_id = enGame.id) INNER JOIN enState ON enOrder.state_id = enState.id) INNER JOIN enAddress ON enUser.address_id = enAddress.id;";
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
                                record._date = reader.GetDateTime(1);
                                record._amount = reader.GetInt32(2);
                                record._state = reader.GetString(3);
                                record._title = reader.GetString(4);
                                record._price = reader.GetDecimal(5);
                                record._first_name = reader.GetString(6);
                                record._last_name = reader.GetString(7);
                                record._phone = reader.GetString(8);
                                record._email = reader.GetString(9);
                                record._country = reader.GetString(10);
                                record._postal_code = reader.GetString(11);
                                record._city = reader.GetString(12);
                                record._street = reader.GetString(13);
                                orders.Add(record);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
            }
            return orders;
        }

        public static void DeleteOrder(int id)
        {
            string query = "DELETE FROM enOrder WHERE id = @id";
            try
            {
                using (SqlConnection connection = Database.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
            }
        }

    }
}
