using System;
using System.Data.SqlClient;
using Nhom14_CoCaro_Va_Sudoku.DTO;

namespace Nhom14_CoCaro_Va_Sudoku.DAO
{
    public class CaroDAO
    {
        DataProvider dp = new DataProvider();

        public int LayNguoiChoi()
        {
            object kq = dp.ExecuteScalar("SELECT TOP 1 Id FROM NguoiChoi");
            return kq != null ? Convert.ToInt32(kq) : 1;
        }

        public void LuuTran(CaroDTO dto)
        {
            string query = @"INSERT INTO CaroTranChoi
                            (NguoiChoiId, KetQua, NgayChoi)
                            VALUES (@id,@kq,@ngay)";

            SqlParameter[] p =
            {
                new SqlParameter("@id", dto.NguoiChoiId),
                new SqlParameter("@kq", dto.KetQua),
                new SqlParameter("@ngay", DateTime.Now)
            };

            dp.ExecuteNonQuery(query, p);
        }

        public void CapNhatDiem(int id, bool thang)
        {
            string query;

            if (thang)
            {
                query = @"UPDATE NguoiChoi 
                          SET DiemCaro = DiemCaro + 10,
                              SoTranThang = SoTranThang + 1
                          WHERE Id=@id";
            }
            else
            {
                query = @"UPDATE NguoiChoi 
                          SET SoTranThua = SoTranThua + 1
                          WHERE Id=@id";
            }

            SqlParameter[] p =
            {
                new SqlParameter("@id", id)
            };

            dp.ExecuteNonQuery(query, p);
        }
    }
}