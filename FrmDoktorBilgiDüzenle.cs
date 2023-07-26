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
    public partial class FrmDoktorBilgiDüzenle : Form
    {
        public FrmDoktorBilgiDüzenle()
        {
            InitializeComponent();
        }

        public string TCNO;
        private void FrmDoktorBilgiDüzenle_Load(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                mskdTc.Text = TCNO;
                SqlCommand komut = new SqlCommand("Select * From tbl_Randevular where DoktorTC=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", mskdTc.Text);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    txtAd.Text = dr[1].ToString();
                    txtSoyad.Text = dr[2].ToString();
                    cmbBrans.Text = dr[3].ToString();
                    txtsifre.Text = dr[5].ToString();
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                SqlCommand komut = new SqlCommand("Update tbl_Doktorlar set DoktorAd=@p1, DoktorSoyad=@p2, DoktorBrans=@p3, DoktorSifre=@p4, where DoktorTC=@p5", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", cmbBrans.Text);
                komut.Parameters.AddWithValue("@p4", txtsifre.Text);
                komut.Parameters.AddWithValue("@p5", mskdTc.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Güncenllendi");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
