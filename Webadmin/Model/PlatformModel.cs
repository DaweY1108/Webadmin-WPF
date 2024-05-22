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
    public class PlatformModel
    {
        public class Record
        {
            public int _id { get; set; }
            public string _name { get; set; }
        }

        public static IEnumerable<Record> GetPlatforms()
        {
            List<Record> platforms = new List<Record>();
            string query = "SELECT * FROM enPlatform";
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
                                platforms.Add(record);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
            }
            return platforms;
        }

        public static void AddPlatform(string name)
        {
            IEnumerable<Record> platforms = GetPlatforms();
            int lastId = platforms.Last()._id;
            string query = "INSERT INTO enPlatform (id, name) VALUES (@id, @name)";
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

        public static Record GetPlatform(string name)
        {
            string query = "SELECT * FROM enPlatform WHERE name = @name";
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

        public static bool CheckPlatform(string name)
        {
            string query = "SELECT * FROM enPlatform WHERE name = @name";
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
