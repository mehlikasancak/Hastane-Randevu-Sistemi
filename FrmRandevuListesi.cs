using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmRandevuListesi : Form
    {
        public FrmRandevuListesi()
        {
            InitializeComponent();
        }

        private void FrmRandevuListesi_Load(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * From tbl_Randevular", bgl.baglanti());
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }        }
    }
}
