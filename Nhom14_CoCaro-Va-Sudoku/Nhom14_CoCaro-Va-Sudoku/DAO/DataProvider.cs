using System.Data.SqlClient;

namespace Nhom14_CoCaro_Va_Sudoku.DAO
{
    public class DataProvider
    {
        string connectionString =
        "Data Source=.;Initial Catalog=GameCenter;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public int ExecuteNonQuery(string query, SqlParameter[] param = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                if (param != null)
                    cmd.Parameters.AddRange(param);

                return cmd.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string query, SqlParameter[] param = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                if (param != null)
                    cmd.Parameters.AddRange(param);

                return cmd.ExecuteScalar();
            }
        }
    }
}