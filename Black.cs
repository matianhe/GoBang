using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gobang
{
    class Black:ChessMan
    {
        ChessBoard cb = new ChessBoard();
        public void draw(Graphics g)
        {
            //计算  焦点坐标
            //详细注释 同White类
            int colX = X / ChessBoard.LineSpace;
            decimal colsX = decimal.Parse(X.ToString()) / decimal.Parse(ChessBoard.LineSpace.ToString());
            decimal gapX = colsX - colX;   //鼠标点击的点和焦点差距

            int colY = Y / ChessBoard.LineSpace;
            decimal colsY = decimal.Parse(Y.ToString()) / decimal.Parse(ChessBoard.LineSpace.ToString());
            decimal gapY = colsY - colY;

            if (10 * gapX < 3)
            {
                X = colX * 30 - 10;
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
            int stone = cb.isDraw((X+10) / 30, (Y+10) / 30);
            if ( stone == 0)
            {
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    g.FillEllipse(brush, X, Y, 2 * r, 2 * r);
                    ChessMan.isBlack = false; //记录黑白棋
                    int i = (X + 10) / 30;
                    int j = (Y + 10) / 30;
                    cb.drawWhich(i, j, 1);
                }
            }
            else
                return;
        }
    }
}
