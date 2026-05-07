using System;
using System.Collections.Generic;
using System.Drawing;

namespace Nhom14_CoCaro_Va_Sudoku.BUS
{
    public class CaroEngine
    {
        public int size = 15;
        public int[,] board;

        public CaroEngine()
        {
            board = new int[size, size];
        }

        public void Reset()
        {
            board = new int[size, size];
        }

        public bool Danh(int x, int y, int player)
        {
            if (board[x, y] != 0) return false;
            board[x, y] = player;
            return true;
        }

        public bool CheckWin(int x, int y, out List<Point> list)
        {
            list = new List<Point>();
            int player = board[x, y];

            int[][] dir = {
                new int[]{1,0}, new int[]{0,1},
                new int[]{1,1}, new int[]{1,-1}
            };

            foreach (var d in dir)
            {
                List<Point> temp = new List<Point>();
                temp.Add(new Point(x, y));

                for (int k = 1; k < 5; k++)
                {
                    int nx = x + d[0] * k;
                    int ny = y + d[1] * k;
                    if (nx < 0 || ny < 0 || nx >= size || ny >= size) break;
                    if (board[nx, ny] != player) break;
                    temp.Add(new Point(nx, ny));
                }

                for (int k = 1; k < 5; k++)
                {
                    int nx = x - d[0] * k;
                    int ny = y - d[1] * k;
                    if (nx < 0 || ny < 0 || nx >= size || ny >= size) break;
                    if (board[nx, ny] != player) break;
                    temp.Add(new Point(nx, ny));
                }

                if (temp.Count >= 5)
                {
                    list = temp;
                    return true;
                }
            }
            return false;
        }
    }
}