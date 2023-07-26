﻿using System;
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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        private void btıngiris_Click(object sender, EventArgs e)
        {
            try
            {
                MSancakSQLConn bgl = new MSancakSQLConn();
                SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad From tbl_sekreter Where SekreterTC=@p1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", mskdtc.Text);
                SqlDataReader dr = komut1.ExecuteReader();
                if (dr.Read())
                {
                    FrmSekreterDetay frs = new FrmSekreterDetay();
                    frs.TCnumara = mskdtc.Text;
                    frs.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Hatalı TC & Şifre");
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
