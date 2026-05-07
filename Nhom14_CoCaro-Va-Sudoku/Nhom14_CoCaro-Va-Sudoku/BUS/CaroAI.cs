using System;
using System.Collections.Generic;

namespace Nhom14_CoCaro_Va_Sudoku
{
    public class CaroAI
    {
        int[] attack = { 0, 2, 18, 162, 1458, 13112 };

        public (int, int) NuocDi(int[,] board, int size, int level)
        {
            if (level == 1) return Easy(board, size);
            if (level == 2) return Medium(board, size);
            return Hard(board, size);
        }

        // ================= EASY =================
        (int, int) Easy(int[,] board, int size)
        {
            int bx = size / 2;
            int by = size / 2;
            int best = -999999;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (board[x, y] != 0) continue;

                    if (IsWin(board, size, x, y, 2))
                        return (x, y);

                    if (IsWin(board, size, x, y, 1))
                        return (x, y);

                    int score = Score(board, x, y, size);

                    if (score > best)
                    {
                        best = score;
                        bx = x;
                        by = y;
                    }
                }
            }

            return (bx, by);
        }

        // ================= MEDIUM =================
        (int, int) Medium(int[,] board, int size)
        {
            int best = -999999;
            int bx = size / 2;
            int by = size / 2;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (board[x, y] != 0) continue;

                    if (IsWin(board, size, x, y, 2))
                        return (x, y);

                    if (IsWin(board, size, x, y, 1))
                        return (x, y);

                    int score = Score(board, x, y, size)
                              + Defend(board, x, y, size);

                    if (score > best)
                    {
                        best = score;
                        bx = x;
                        by = y;
                    }
                }
            }

            return (bx, by);
        }

        // ================= HARD =================
        (int, int) Hard(int[,] board, int size)
        {
            int best = -999999;
            int bx = 0, by = 0;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (board[x, y] != 0) continue;

                    if (IsWin(board, size, x, y, 2))
                        return (x, y);

                    if (IsWin(board, size, x, y, 1))
                        return (x, y);

                    int score = Score(board, x, y, size) * 2
                              + Defend(board, x, y, size);

                    if (score > best)
                    {
                        best = score;
                        bx = x;
                        by = y;
                    }
                }
            }

            return (bx, by);
        }

        // ================= SCORE =================
        int Score(int[,] board, int x, int y, int size)
        {
            return Line(board, x, y, 1, 0, size)
                 + Line(board, x, y, 0, 1, size)
                 + Line(board, x, y, 1, 1, size)
                 + Line(board, x, y, 1, -1, size);
        }

        int Defend(int[,] board, int x, int y, int size)
        {
            return Count(board, x, y, 1, 0, size, 1)
                 + Count(board, x, y, 0, 1, size, 1)
                 + Count(board, x, y, 1, 1, size, 1)
                 + Count(board, x, y, 1, -1, size, 1);
        }

        int Line(int[,] board, int x, int y, int dx, int dy, int size)
        {
            int ai = Count(board, x, y, dx, dy, size, 2);
            int pl = Count(board, x, y, dx, dy, size, 1);

            return attack[ai] + attack[pl];
        }

        // ================= CHECK WIN =================
        bool IsWin(int[,] board, int size, int x, int y, int player)
        {
            board[x, y] = player;

            bool win = CheckWin(board, size, player);

            board[x, y] = 0;

            return win;
        }

        bool CheckWin(int[,] board, int n, int player)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j + 4 < n &&
                        board[i, j] == player &&
                        board[i, j + 1] == player &&
                        board[i, j + 2] == player &&
                        board[i, j + 3] == player &&
                        board[i, j + 4] == player)
                        return true;

                    if (i + 4 < n &&
                        board[i, j] == player &&
                        board[i + 1, j] == player &&
                        board[i + 2, j] == player &&
                        board[i + 3, j] == player &&
                        board[i + 4, j] == player)
                        return true;
                }
            }

            return false;
        }

        int Count(int[,] board, int x, int y, int dx, int dy, int size, int player)
        {
            int c = 0;

            for (int i = 1; i < 5; i++)
            {
                int nx = x + dx * i;
                int ny = y + dy * i;

                if (nx < 0 || ny < 0 || nx >= size || ny >= size) break;
                if (board[nx, ny] == player) c++;
                else break;
            }

            for (int i = 1; i < 5; i++)
            {
                int nx = x - dx * i;
                int ny = y - dy * i;

                if (nx < 0 || ny < 0 || nx >= size || ny >= size) break;
                if (board[nx, ny] == player) c++;
                else break;
            }

            return c;
        }
    }
}