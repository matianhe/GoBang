using System;
using System.Collections.Generic;
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
    class Load
    {

        public void read(string name)
        {
            #region 连接数据库
            string connStr = ConfigurationManager.ConnectionStrings["Sql"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    conn.Open();

                    for (int i = 1; i < 16; i++)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "proc_SrcData";
                        SqlParameter param = new SqlParameter("id", i);
                        cmd.Parameters.Add(param);
                        param = new SqlParameter("name",name);
                        cmd.Parameters.Add(param);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int j = 1; j < 16; j++)
                                {
                                    ChessBoard.state[j, i] = int.Parse(reader["cols" + j.ToString()].ToString());
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                    }
                }
            }
            #endregion
        } 
            
        public void save()
        {
            #region 导入数据库
            string connStr = ConfigurationManager.ConnectionStrings["Sql"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "proc_CreateTable";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    for (int i = 1; i < 16; i++)
                    {
                        for (int j = 1; j < 16; j++)
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "proc_InsertData";
                            SqlParameter param = new SqlParameter("@cols" + j.ToString(), ChessBoard.state[j, i]);
                            cmd.Parameters.Add(param);
                        }
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                    }

                }
            }//end conn 
            #endregion
        }
        public void reset(string name,Graphics g)
        {

            ChessBoard cb = new ChessBoard();
            ChessMan cm = new ChessMan();
            read(name);
            for (int i = 1; i < 16; i++)
            {
                for (int j = 1; j < 16; j++)
                {
                    if (ChessBoard.state[j, i] == 1)
                    {
                        cm.draw(j, i, g, Color.Black);
                    }
                    else if (ChessBoard.state[j, i] == -1)
                    {
                        cm.draw(j, i, g, Color.White);
                    }
                }
            }
        }
    }
}
