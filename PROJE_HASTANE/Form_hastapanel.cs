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

namespace PROJE_HASTANE
{
    public partial class Form_hastapanel : Form
    {
        public Form_hastapanel()
        {
            InitializeComponent();
        }
        Class_Baglanti bglanti = new Class_Baglanti();
        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_HastaBilgiDuzenle bilgiduzenle = new Form_HastaBilgiDuzenle();
            bilgiduzenle.TCno = lblTC.Text;
            bilgiduzenle.Show();
        }
        public string tc;
        
        private void Form_hastapanel_Load(object sender, EventArgs e)
        {

            //HASTA BİLGİ ÇEKME.
            lblTC.Text = tc;
            SqlCommand komut = new SqlCommand("Select hasta_ad, hasta_soyad, hasta_telno From hasta_tablo where hasta_TC=@p1", bglanti.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                lblAd_Soyad.Text = dr[0] + " " + dr[1];
                lblTelNo.Text = dr[2].ToString();
            }
            bglanti.baglanti().Close();
            //

            //RANDEVU GEÇMİŞİ.

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From randevu_tablo where hasta_TC=" + tc , bglanti.baglanti());
            da.Fill(dt);
            dataGridViewGecmis.DataSource = dt;

            //

            //BRANŞLARI ÇEKME.
            SqlCommand komut2 = new SqlCommand("Select brans_ad From brans_tablo", bglanti.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                comboBrans.Items.Add(dr2[0]);
            }


        }

        private void comboBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BRANŞA GÖRE DOKTOR EKLEME
            comboDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("Select doktor_ad, doktor_soyad From doktor_tablo Where doktor_brans=@p1", bglanti.baglanti());
            komut3.Parameters.AddWithValue("@p1", comboBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while(dr3.Read())
            {
                comboDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
        }

        private void comboDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From randevu_tablo where randevu_brans = '" + comboBrans.Text + "'" + "and randevu_doktor='" + comboDoktor.Text + "' and randevu_durum=0", bglanti.baglanti());
            da.Fill(dt);
            dataGridViewAktif.DataSource = dt;
        }

        private void dataGridViewAktif_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridViewAktif.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridViewAktif.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update randevu_tablo Set randevu_durum=1, hasta_TC = @p1, hasta_sikayet = @p2 where randevu_id = @p3", bglanti.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            komut.Parameters.AddWithValue("@p2", richSikayet.Text);
            komut.Parameters.AddWithValue("@p3", textBox1.Text);
            komut.ExecuteNonQuery();
            bglanti.baglanti().Close();
            MessageBox.Show("RANDEVU ALINDI", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
