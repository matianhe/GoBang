using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gobang
{
    class ChessMan
    {

        public int X
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }
        //static  是为了 在别的类用类名进行操作 
        public static int r = 10;  // 半径   
        public static bool isBlack = true; //记录黑白棋

        ChessBoard cb = new ChessBoard();
        public void draw(Graphics g)
        {
            //计算  焦点坐标

            int colX = X / ChessBoard.LineSpace; //整数跨了多少row
            //多余row的小数 如果 不转换成decimal  会出现没有小数
            decimal colsX = decimal.Parse(X.ToString()) / decimal.Parse(ChessBoard.LineSpace.ToString());
            decimal gapX = colsX - colX;   //鼠标点击的点和焦点差距

            int colY = Y / ChessBoard.LineSpace;
            decimal colsY = decimal.Parse(Y.ToString()) / decimal.Parse(ChessBoard.LineSpace.ToString());
            decimal gapY = colsY - colY;

            /*
             * 判断落子位置；
             *  如果距离小于3  或者 大于7
             *      落在附近交叉点
             *  如果 在3和7之间  
             *      return
             */
            #region 判断落子位置
            if (10 * gapX < 3)  //因为if不支持 小数 所以*10
            {
                X = colX * 30 - 10;  //-colX为 整数row ，* 30 格宽就是焦点坐标 -10是要圆心落在焦点
                if (10 * gapY < 3)
                {
                    Y = colY * 30 - 10;
                }
                else if (10 * gapY > 7)
                {
                    Y = (colY + 1) * 30 - 10; //+1是因为如果>7则需要进一row 而计算机语言只能舍去小数部分
                }
                else
                    return;
            }
            else if (10 * gapX > 7)
            {
                X = (colX + 1) * 30 - 10;
                if (10 * gapY < 3)
                {
                    Y = colY * 30 - 10;
                }
                else if (10 * gapY > 7)
                {
                    Y = (colY + 1) * 30 - 10;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
            #endregion


            // 先判断这个点有没有棋子

            int stone = cb.isDraw((X + 10) / 30,(Y + 10) / 30);
            if (X+10 == 0 || Y+10 == 0)
            {
                return;
            } 
            if (stone == 0)
            {
                if (isBlack)
                {
                    using (SolidBrush brush = new SolidBrush(Color.Black))
                    {
                        g.FillEllipse(brush, X, Y, 2 * r, 2 * r);
                        isBlack = false; //记录黑白棋谁的旗权

                        /*
                         * 记录每一个点下的是什么颜色的棋
                         * 1 黑 -1 白 无 0
                         */
                        int i = (X + 10) / 30;
                        int j = (Y + 10) / 30;
                        cb.drawWhich(i, j, 1);
                    }
                }
                else
                {
                    if (X+10 == 0 || Y+10 == 0)
                    {
                        return;
                    }
                    using (SolidBrush brush = new SolidBrush(Color.White))
                    {
                        g.FillEllipse(brush, X, Y, 2 * r, 2 * r);
                        isBlack = true; //记录黑白棋谁的旗权

                        /*
                         * 记录每一个点下的是什么颜色的棋
                         * 1 黑 -1 白 无 0
                         */

                        int i = (X + 10) / 30;
                        int j = (Y + 10) / 30;

                        cb.drawWhich(i, j, -1);
                    }
                }
            }
            else
                return;

        }
        public void draw(int j,int i,Graphics g,Color c)
        {
            int X = j * 30 - 10;
            int Y = i * 30 - 10;

            using (SolidBrush brush = new SolidBrush(c))
            {
                g.FillEllipse(brush, X, Y, 2 * r, 2 * r);

            }
         }


      }
}
