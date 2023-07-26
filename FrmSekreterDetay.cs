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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                lblTC.Text = TCnumara;
                //Ad Soyad
                SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad From tbl_Sekreter Where SekreterTC=@p1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", lblTC.Text);
                SqlDataReader dr1 = komut1.ExecuteReader();
                while (dr1.Read())
                {
                    lblAdSoyad.Text = dr1[0].ToString();
                }
                bgl.baglanti().Close();

                //Branşları DataGride aktarma
                DataTable dt1 = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select BransAd from tbl_Branslar", bgl.baglanti());
                da.Fill(dt1);
                dataGridView1.DataSource = dt1;

                //Doktorları Listeye Aktarma

                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as 'Doktorlar', DoktorBrans From tbl_Doktorlar", bgl.baglanti());
                da2.Fill(dt2);
                dataGridView2.DataSource = dt2;

                //Branşı Combobx Aktarma
                SqlCommand komut2 = new SqlCommand("Select BransAd from tbl_Branslar", bgl.baglanti());
                SqlDataReader dr2 = komut2.ExecuteReader();
                while (dr2.Read())
                {
                    cmbBrans.Items.Add(dr2[0]);
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                SqlCommand komutkaydet = new SqlCommand("insert into tbl_Randevular (RandevuTarih, RandevuSaat, RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)", bgl.baglanti());
                komutkaydet.Parameters.AddWithValue("@r1", mskdTarih.Text);
                komutkaydet.Parameters.AddWithValue("@r2", mskdSaat.Text);
                komutkaydet.Parameters.AddWithValue("@r3", cmbBrans.Text);
                komutkaydet.Parameters.AddWithValue("@r4", cmbDoktor.Text);
                komutkaydet.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Oluşturuldu");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                cmbDoktor.Items.Clear();
                SqlCommand komut = new SqlCommand("Select DoktorAd, DoktorSoyad from tbl_Doktorlar Where DoktorBrans=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", cmbBrans.Text);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbDoktor.Items.Add(dr[0] + " " + dr[1]);
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                SqlCommand komut = new SqlCommand("insert into tbl_Duyurular (duyuru) values (@d1)", bgl.baglanti());
                komut.Parameters.AddWithValue("@d1", rchDuyuru.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Duyuru Oluşturuldu");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message); 
            }
        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
        }

        private void btnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBrans frb = new FrmBrans();
            frb.Show();
        }

        private void btnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frl = new FrmRandevuListesi();
            frl.Show();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular dyr = new FrmDuyurular();
            dyr.Show();
        }
    }
}
