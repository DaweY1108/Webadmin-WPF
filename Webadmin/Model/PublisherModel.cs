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
    public class PublisherModel
    {
        public class Record
        {
            public int _id { get; set; }
            public string _name { get; set; }
        }

        public static IEnumerable<Record> GetPublishers()
        {
            List<Record> publishers = new List<Record>();
            string query = "SELECT * FROM enPublisher";
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
                                record._name = reader.GetString(1);
                                publishers.Add(record);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
            }
            return publishers;
        }

        public static void AddPublisher(string name)
        {
            IEnumerable<Record> publishers = GetPublishers();
            int lastId = publishers.Last()._id;
            string query = "INSERT INTO enPublisher (id, name) VALUES (@id, @name)";
            try
            {
                using (SqlConnection connection = Database.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
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

        public static void DeletePublisher(int id)
        {
            string query = "DELETE FROM enPublisher WHERE id = @id";
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

        public static Record GetPublisher(string name)
        {
            string query = "SELECT * FROM enPublisher WHERE name = @name";
            Record record = new Record();
            try
            {
                using (SqlConnection connection = Database.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                record._id = reader.GetInt32(0);
                                record._name = reader.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
            }
            return record;
        }

        public static bool CheckPublisher(string name)
        {
            string query = "SELECT * FROM enPublisher WHERE name = @name";
            try
            {
                using (SqlConnection connection = Database.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
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
            }
            return false;
        }
    }
}
