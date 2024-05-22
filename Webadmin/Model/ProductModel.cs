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
    public class ProductModel
    {
        public class Record
        {
            public int _id { get; set; }
            public string _title { get; set; }
            public decimal _price { get; set; }
            public string _description { get; set; }
            public int _publish_year { get; set; }
            public decimal _rating { get; set; }
            public string _publisher { get; set; }
            public string _platform { get; set; }
        }

        public static IEnumerable<Record> GetProducts()
        {
            List<Record> products = new List<Record>();
            string query = "SELECT enGame.id, title, price, description, publish_year, rating, enPublisher.name AS publisher_name, enPlatform.name AS platform_name FROM (enGame INNER JOIN enPublisher ON enGame.publisher_id=enPublisher.id) INNER JOIN enPlatform ON enGame.platform_id = enPlatform.id";
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
                                record._title = reader.GetString(1);
                                record._price = reader.GetDecimal(2);
                                record._description = reader.GetString(3);
                                record._publish_year = reader.GetInt16(4);
                                record._rating = reader.GetDecimal(5);
                                record._publisher = reader.GetString(6);
                                record._platform = reader.GetString(7);
                                products.Add(record);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
            }
            return products;
        } 
        
        public static void DeleteProduct(int id)
        {
            string query = "DELETE FROM enGame WHERE id = @id";
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

        public static void AddProduct(string title, decimal price, string description, int publish_year, decimal rating, int publisher_id, int platform_id)
        {
            IEnumerable<Record> products = GetProducts();
            int lastId = products.Last()._id;
            string query = "INSERT INTO enGame (id, title, price, description, publish_year, rating, publisher_id, platform_id) VALUES (@id, @title, @price, @description, @publish_year, @rating, @publisher_id, @platform_id)";
            try
            {
                using (SqlConnection connection = Database.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@title", title);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@description", description);
                        command.Parameters.AddWithValue("@publish_year", publish_year);
                        command.Parameters.AddWithValue("@rating", rating);
                        command.Parameters.AddWithValue("@publisher_id", publisher_id);
                        command.Parameters.AddWithValue("@platform_id", platform_id);
                        command.Parameters.AddWithValue("@id", lastId + 1);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
            }
        }
        public static bool CheckProduct(string title)
        {
            string query = "SELECT * FROM enGame WHERE title = @title";
            try
            {
                using (SqlConnection connection = Database.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@title", title);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
                return false;
            }
            return false;
        }
    }
}
