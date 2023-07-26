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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        
        public string TC;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                lbltc.Text = TC;

                //Doktor Ad Soyad

                SqlCommand komut = new SqlCommand("Select DoktorAd, DoktorSoyad From tbl_Doktorlar Where DoktorTC=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", lbltc.Text);
                SqlDataReader dr = komut.ExecuteReader();

                while (dr.Read())
                {
                    lblAdSoyad.Text = dr[0] + " " + dr[1];
                }

                bgl.baglanti().Close();

                //Randevular

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * From tbl_Randevular Where RandevuDoktor='" + lblAdSoyad.Text + "'", bgl.baglanti());
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBilgiDuzenle_Click(object sender, EventArgs e)
        {
            FrmBilgiDüzenle fr = new FrmBilgiDüzenle();
            fr.TCno = lbltc.Text;
            fr.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }

        private void btnCikisYap_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            richSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();

        }
    }
}
