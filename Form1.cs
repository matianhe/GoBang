using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gobang
{
    public partial class Form1 : Form
    {

        ChessBoard cb = new ChessBoard();
        ChessMan cm = new ChessMan();
        public Form1()
        {
            //初始化  画棋盘
            InitializeComponent();
            cb.PicCtrl = pictureBox;
            cb.drawBoard();
            lblBlack.Text = "黑方落子";
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = pictureBox.CreateGraphics();  //在期盼里创建一个画布
            //判断 是黑棋 还是白棋 获取坐标 画棋子

            //  问题  ？  怎么画到交叉点上 
            // 棋子类 解决
            if (ChessMan.isBlack)
            {
                //把鼠标坐标 给到 旗子坐标
                cm.X = e.X;
                cm.Y = e.Y;
                cm.draw(g);
                if (!ChessMan.isBlack) //判断是否落子成功
                {
                    if (cm.X + 10 == 0 || cm.Y + 10 == 0)
                    {
                        return;
                    }
                    //判断四个方向能不能组成五个黑子
                    if (cb.isBlackWinLR((cm.X + 10) / 30, (cm.Y + 10) / 30) > 5 ||
                        cb.isBlackWinUD((cm.X + 10) / 30, (cm.Y + 10) / 30) > 5 ||
                        cb.isBlackWinUL((cm.X + 10) / 30, (cm.Y + 10) / 30) > 5 ||
                        cb.isBlackWinUR((cm.X + 10) / 30, (cm.Y + 10) / 30) > 5)
                    {
                        MessageBox.Show("黑棋赢了！");
                    }
                    lblWhite.Text = "白方落子";
                    lblBlack.Text = "";
                }

            }
            else
            {
                cm.X = e.X;
                cm.Y = e.Y;
                cm.draw(g);
                if (ChessMan.isBlack)
                {
                    if (cm.X + 10 == 0 || cm.Y + 10 == 0)
                    {
                        return;
                    }
                    if (cb.isWhiteWinLR((cm.X + 10) / 30, (cm.Y + 10) / 30) > 5 ||
                        cb.isWhiteWinUD((cm.X + 10) / 30, (cm.Y + 10) / 30) > 5 ||
                        cb.isWhiteWinUL((cm.X + 10) / 30, (cm.Y + 10) / 30) > 5 ||
                        cb.isWhiteWinUR((cm.X + 10) / 30, (cm.Y + 10) / 30) > 5)
                    {
                        MessageBox.Show("白棋赢了！");
                    }
                    lblWhite.Text = "";
                    lblBlack.Text = "黑方落子";
                }

            }

        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            //初始化
            cb.init();
            lblBlack.Text = "黑方落子";
            lblWhite.Text = "";
            ChessMan.isBlack = true;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {   /*取消数据库
            Graphics g = pictureBox.CreateGraphics();
            Load load = new Load();
            load.reset(cmbLoad.SelectedItem.ToString(),g);
            */
        }

        private void btnBackup(object sender, EventArgs e)
        { /*取消数据库
            Load load = new Load();
            load.save();
            MessageBox.Show("备份成功！");
            */
        }
/*   取消数据库
        private void Form1_Load(object sender, EventArgs e)
        {
            #region LoadBoard
            string connStr = ConfigurationManager.ConnectionStrings["Sql"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "proc_SrhAllTable";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string board = reader["name"].ToString();
                            cmbLoad.Items.Add(board);
                        }
                    }// end reader

                }
            }//end conn 
            #endregion
        }
*/
        private void button2_Click(object sender, EventArgs e)
        {
            Import import = new Import();
            import.Show();
        }
        public void reset()
        {
            Graphics g = pictureBox.CreateGraphics();
            Import import = new Import();
            Load load = new Load();
            load.reset(import.lbxBoard.Text,g);
        }

        private void cmbLoad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
    

