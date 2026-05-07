using Nhom14_CoCaro_Va_Sudoku.DAO;
using Nhom14_CoCaro_Va_Sudoku.DTO;

namespace Nhom14_CoCaro_Va_Sudoku.BUS
{
    public class SudokuBUS
    {
        SudokuDAO dao = new SudokuDAO();

        public int LayNguoiChoi()
        {
            return dao.LayNguoiChoi();
        }

        public void Thang(SudokuDTO dto)
        {
            dao.LuuTran(dto);
            dao.CapNhatDiem(dto.NguoiChoiId, true);
        }

        public void Thua(SudokuDTO dto)
        {
            dao.LuuTran(dto);
            dao.CapNhatDiem(dto.NguoiChoiId, false);
        }
    }
}