using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Model
{
    public class Database
    {
        public static SqlConnection GetConnection()
        {
            string _connectionString = "Server=DESKTOP-M3QM42D;Database=AdminPanel;User Id=sa;Trusted_Connection=true;TrustServerCertificate=True;Password=sasa;";
            return new SqlConnection(_connectionString);
        }
    }
}
