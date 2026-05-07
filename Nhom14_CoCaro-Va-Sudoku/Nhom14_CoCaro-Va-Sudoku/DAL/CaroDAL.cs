using System;
using System.Data;
using System.Data.SqlClient;
using Nhom14_CoCaro_Va_Sudoku.DTO;

namespace Nhom14_CoCaro_Va_Sudoku.DAL
{
    public class CaroDAL
    {
        string connStr = @"Data Source=.;Initial Catalog=GameCenter;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";

        public int LayNguoiChoi()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = "SELECT TOP 1 Id FROM NguoiChoi";
                SqlCommand cmd = new SqlCommand(sql, conn);

                object kq = cmd.ExecuteScalar();

                if (kq != null)
                    return Convert.ToInt32(kq);

                SqlCommand insert = new SqlCommand(
                    "INSERT INTO NguoiChoi (Ten, DiemCaro, SoTranThang, SoTranThua) VALUES (N'Player',0,0,0)",
                    conn);

                insert.ExecuteNonQuery();

                return (int)new SqlCommand("SELECT TOP 1 Id FROM NguoiChoi", conn).ExecuteScalar();
            }
        }

        public void LuuTran(CaroDTO dto)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = @"INSERT INTO CaroTranChoi
                               (NguoiChoiId, TenGame, KetQua, DoKho, ThoiGianChoi, NgayChoi)
                               VALUES (@id, 'Caro', @kq, @dk, @tg, @ngay)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = dto.NguoiChoiId;
                cmd.Parameters.Add("@kq", SqlDbType.NVarChar).Value = dto.KetQua;
                cmd.Parameters.Add("@dk", SqlDbType.Int).Value = dto.DoKho;
                cmd.Parameters.Add("@tg", SqlDbType.Int).Value = dto.ThoiGianChoi;
                cmd.Parameters.Add("@ngay", SqlDbType.DateTime).Value = dto.NgayChoi;

                cmd.ExecuteNonQuery();
            }
        }

        public void CapNhatDiem(int id, bool thang)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql;

                if (thang)
                {
                    sql = @"UPDATE NguoiChoi 
                            SET DiemCaro = DiemCaro + 10,
                                SoTranThang = SoTranThang + 1
                            WHERE Id=@id";
                }
                else
                {
                    sql = @"UPDATE NguoiChoi 
                            SET SoTranThua = SoTranThua + 1
                            WHERE Id=@id";
                }

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                cmd.ExecuteNonQuery();
            }
        }
    }
}