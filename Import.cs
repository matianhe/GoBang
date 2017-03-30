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
    public partial class Import : Form
    {
        public Import()
        { 
            InitializeComponent();
        }

        private void Import_Load(object sender, EventArgs e)
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
                            lbxBoard.Items.Add(board);
                        }
                    }// end reader

                }
            }//end conn 
            #endregion
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            Graphics g = form1.pictureBox.CreateGraphics();
            Load load = new Load();
            load.reset(lbxBoard.SelectedItem.ToString(), g);
            form1.Show();

            //Form1 form1 = new Form1();
            //form1.reset();
        }
    }
}
