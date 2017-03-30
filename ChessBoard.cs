using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gobang
{
    class ChessBoard
    {

        public static int LineSpace = 30; //格宽 
        public static int rows =16;    //横格
        public static int cols = 16;   //竖格
        public PictureBox PicCtrl;
        //用二维数组 记录每个点是什么棋子
        public static int[,] state = new int[rows+1, cols+1]; //+1为了防止隆哥给我点死

        //画棋盘
        public void drawBoard()
        {
            Bitmap bmp = new Bitmap(PicCtrl.Width, PicCtrl.Height);
            Graphics g = Graphics.FromImage(bmp);

            //画背景颜色      
            SolidBrush brush = new SolidBrush(Color.Gold);
            g.FillRectangle(brush, 0, 0, (rows - 1) * LineSpace, (cols - 1) * LineSpace);

            Pen linePen = new Pen(Color.Black, 1);
            //Lines条横线      
            for (int i = 0; i < cols; i++)
            {
                //先找到点作为参数传给g 之后画线
                Point start = new Point(0, i * LineSpace);
                Point end = new Point(start.X + (rows - 1) * LineSpace, start.Y);
                g.DrawLine(linePen, start, end);
            }
            //Lines条竖线      
            for (int i = 0; i < rows; i++)
            {
                Point start = new Point(i * LineSpace, 0);
                Point end = new Point(start.X, start.Y + (cols - 1) * LineSpace);
                g.DrawLine(linePen, start, end);
            }

            PicCtrl.BackgroundImage = bmp;
        }

        //设置 这个点的棋子
        public void drawWhich(int i, int j, int stone)
        {
            state[i, j] = stone;
        }
        // 判断 这个点是什么颜色棋子
        public int isDraw(int x, int y)
        {
            return state[x, y];
        }
        #region 判断白棋
        #region 判断左右方向是否连成五子
        public int isWhiteWinLR(int x, int y)
        {
            /*用来记录连续个数
             * 一开始用单边计数 不方便  
             */
            int n = 0;
            int left = x;  // 左边有几个一样的
            int right = x;   //右边有几个一样的
            while (state[left, y] == -1)
            {
                n++;
                left--;
            }
            while (state[right, y] == -1)
            {
                n++;         //这里加了1 所以判断是否连成五子时为连成4子 注意！
                right++;
            }
            return n;
        }
        #endregion
        #region 上下方向是否连成五子
        public int isWhiteWinUD(int x, int y)
        {

            int n = 0;
            int up = y;
            int down = y;
            while (state[x, up] == -1)
            {
                n++;
                up--;
            }
            while (state[x, down] == -1)
            {
                n++;
                down++;
            }
            return n;
        }
        #endregion
        #region 反斜杠方向
        public int isWhiteWinUL(int x, int y)
        {
            /*
             * \ 方向 需要上下左右坐标都移动
             * 所以定义四个方位
             */

            int n = 0;
            int up = y;
            int down = y;
            int left = x;
            int right = x;
            while (state[left, up] == -1)
            {
                n++;
                up--;
                left--;
            }

            while (state[right, down] == -1)
            {
                n++;
                down++;
                right++;
            }
            return n;
        }
        #endregion
        #region 斜杠方向
        public int isWhiteWinUR(int x, int y)
        {
            int n = 0;
            int up = y;
            int down = y;
            int left = x;
            int right = x;
            while (state[right, up] == -1)
            {
                n++;
                up--;
                right++;
            }
            while (state[left, down] == -1)
            {
                n++;
                down++;
                left--;
            }
            return n;
        }
        #endregion
        #endregion
        #region 判断黑棋
        public int isBlackWinLR(int x, int y)
        {
            
            int n = 0;
            int left = x;  
            int right = x;

            while (state[left, y] == 1)
            {
                n++;
                left--;
            }
            while (state[right, y] == 1)
            {
                n++;
                right++;
            }
            return n;
        }
        public int isBlackWinUD(int x, int y)
        {

            int n = 0;
            int up = y;
            int down = y;

                while (state[x, up] == 1)
                {
                    n++;
                    up--;
                }
                while (state[x, down] == 1)
                {
                    n++;
                    down++;
                }
            return n;
        }
        public int isBlackWinUL(int x, int y)
        {
            
            int n = 0;
            int up = y;
            int down = y;
            int left = x;
            int right = x;
            while (state[left, up] == 1)
            {
                n++;
                up--;
                left--;
            }
            while (state[right, down] == 1)
            {
                n++;
                down++;
                right++;
            }
            return n;
        }
        public int isBlackWinUR(int x, int y)
        {
            int n = 0;
            int up = y;
            int down = y;
            int left = x;
            int right = x;
            while (state[right, up] == 1)
            {
                 n++;
                 up--;
                 right++;
            }

            while (state[left, down] == 1)
            {
                 n++;
                 down++;
                 left--;
            }
            return n;
        }
        #endregion
        public void init()
        {
            //重置数组
            for(int i=0;i< rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    state[i, j] = 0;
                }
            }
            //重置图像
            PicCtrl.Invalidate();
            
        }
    }
}
