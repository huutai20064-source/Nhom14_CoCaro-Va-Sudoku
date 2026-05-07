using System;
using Nhom14_CoCaro_Va_Sudoku.DAL;
using Nhom14_CoCaro_Va_Sudoku.DTO;

namespace Nhom14_CoCaro_Va_Sudoku.BUS
{
    public class CaroBUS
    {
        CaroDAL dal = new CaroDAL();

        public int LayNguoiChoi()
        {
            return dal.LayNguoiChoi();
        }

        public void Thang(int id, int dokho, bool laAI)
        {
            if (!laAI) return;

            dal.LuuTran(new CaroDTO
            {
                NguoiChoiId = id,
                KetQua = "Win",
                DoKho = dokho,
                ThoiGianChoi = 0,
                NgayChoi = DateTime.Now
            });

            dal.CapNhatDiem(id, true);
        }

        public void Thua(int id, int dokho, bool laAI)
        {
            if (!laAI) return;

            dal.LuuTran(new CaroDTO
            {
                NguoiChoiId = id,
                KetQua = "Lose",
                DoKho = dokho,
                ThoiGianChoi = 0,
                NgayChoi = DateTime.Now
            });

            dal.CapNhatDiem(id, false);
        }
    }
}