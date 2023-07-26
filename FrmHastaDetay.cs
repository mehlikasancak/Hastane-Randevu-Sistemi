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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                //ad soyad çekme
                lbltc.Text = tc;

                SqlCommand komut = new SqlCommand("Select HastaAd, HastaSoyad From Tbl_Hastalar Where HastaTC=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", lbltc.Text);
                SqlDataReader dr = komut.ExecuteReader();

                while (dr.Read())
                {
                    lbladsoyad.Text = dr[0] + " " + dr[1];
                }
                bgl.baglanti().Close();

                //randevu geçmişi
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * From tbl_Randevular Where HastaTC= " + tc, bgl.baglanti());
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                //branşları çekme

                SqlCommand komut2 = new SqlCommand("Select BransAd From tbl_Branslar", bgl.baglanti());
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

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                //doktor çekme
                cmbDoktor.Items.Clear();

                SqlCommand komut3 = new SqlCommand("Select DoktorAd, DoktorSoyad From tbl_Doktorlar Where DoktorBrans=@p1", bgl.baglanti());
                komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
                SqlDataReader d3 = komut3.ExecuteReader();
                while (d3.Read())
                {
                    cmbDoktor.Items.Add(d3[0] + " " + d3[1]);
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * From tbl_Randevular Where RandevuBrans='" + cmbBrans.Text + "'" + "and RandevuDoktor='" + cmbDoktor.Text + "' and RandevuDurum=0", bgl.baglanti());
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lnkBilgiDüzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDüzenle fr = new FrmBilgiDüzenle();
            fr.TCno = lbltc.Text;
            fr.Show();
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                SqlCommand komut = new SqlCommand("Update tbl_Randevular Set RandevuDurum=1, HastaTC=@p1, HastaSikayet=@p2 Where Randevuid=@p3", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", lbltc.Text);
                komut.Parameters.AddWithValue("@p2", rchSikayet.Text);
                komut.Parameters.AddWithValue("@p3", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Randevu Alındı", "Bildir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }
    }
}
