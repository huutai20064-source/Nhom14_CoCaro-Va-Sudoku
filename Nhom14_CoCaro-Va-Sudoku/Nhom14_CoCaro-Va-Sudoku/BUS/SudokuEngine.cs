using System;

namespace Nhom14_CoCaro_Va_Sudoku.BUS
{
    public class SudokuEngine
    {
        public int[,] board = new int[9, 9];
        public int[,] solution = new int[9, 9];

        Random rand = new Random();

        public void TaoGame(int level)
        {
            board = new int[9, 9];
            solution = new int[9, 9];

            Solve(0, 0);

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    solution[i, j] = board[i, j];

            int remove = level == 1 ? 30 : level == 2 ? 45 : 55;

            while (remove > 0)
            {
                int i = rand.Next(9);
                int j = rand.Next(9);

                if (board[i, j] != 0)
                {
                    board[i, j] = 0;
                    remove--;
                }
            }
        }

        bool Solve(int r, int c)
        {
            if (r == 9) return true;
            if (c == 9) return Solve(r + 1, 0);
            if (board[r, c] != 0) return Solve(r, c + 1);

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < 9; i++)
            {
                int j = rand.Next(9);
                int t = nums[i];
                nums[i] = nums[j];
                nums[j] = t;
            }

            foreach (int num in nums)
            {
                if (KiemTraHopLe(r, c, num))
                {
                    board[r, c] = num;

                    if (Solve(r, c + 1)) return true;

                    board[r, c] = 0;
                }
            }

            return false;
        }

        public bool KiemTraHopLe(int r, int c, int num)
        {
            for (int i = 0; i < 9; i++)
                if (board[r, i] == num || board[i, c] == num)
                    return false;

            int sr = r / 3 * 3;
            int sc = c / 3 * 3;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[sr + i, sc + j] == num)
                        return false;

            return true;
        }

        // 🔥 FIX QUAN TRỌNG: check theo USER board (KHÔNG dùng board gốc)
        public bool KiemTraHopLeUser(int[,] user, int r, int c, int num)
        {
            for (int i = 0; i < 9; i++)
                if (i != c && user[r, i] == num)
                    return false;

            for (int i = 0; i < 9; i++)
                if (i != r && user[i, c] == num)
                    return false;

            int sr = r / 3 * 3;
            int sc = c / 3 * 3;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    int x = sr + i;
                    int y = sc + j;

                    if ((x != r || y != c) && user[x, y] == num)
                        return false;
                }

            return true;
        }

        public bool KiemTraHoanThanh(int[,] user)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (user[i, j] != solution[i, j])
                        return false;

            return true;
        }
        public bool KiemTraDayDuHopLe(int[,] user)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    int val = user[i, j];
                    if (val == 0) return false;

                    // tạm xóa để check tránh self-match
                    user[i, j] = 0;

                    if (!KiemTraHopLeUser(user, i, j, val))
                        return false;

                    user[i, j] = val;
                }

            return true;
        }
    }
}