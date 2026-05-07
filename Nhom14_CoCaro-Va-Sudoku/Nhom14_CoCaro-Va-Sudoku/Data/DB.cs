using System.Data.SqlClient;

namespace Nhom14_CoCaro_Va_Sudoku
{
    public class DB
    {
        public static string connStr =
            "Data Source=.;Initial Catalog=GameCenter;Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connStr);
        }
    }
}