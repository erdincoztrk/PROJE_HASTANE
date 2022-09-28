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
    public partial class Form_sekreterpanel : Form
    {
        public Form_sekreterpanel()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            { checkBox1.Text = "DOLU"; }

            if (checkBox1.Checked == false)
            { checkBox1.Text = "BOŞ"; }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_DrSekreterbilgi doktorpnl = new Form_DrSekreterbilgi();
            doktorpnl.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_BransSekreter branslar = new Form_BransSekreter();
            branslar.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form_RandevuListe randevu = new Form_RandevuListe();
            randevu.Show();
        }

        Class_Baglanti bgl = new Class_Baglanti();
        public string tc;
        private void Form_sekreterpanel_Load(object sender, EventArgs e)
        {
            //AD SOYAD TC EKLEME
            lblTC.Text = tc;
            SqlCommand komut = new SqlCommand("Select sekreter_AdSoyad From sekreter_tablo where sekreter_TC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAd_Soyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            //BRANŞLARI ÇEKME
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select brans_id as 'BRANŞ ID', brans_ad as 'BRANŞ ADI'  from brans_tablo", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //DOKTORLARI ÇEKME
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select doktor_id as 'ID', (doktor_ad + ' ' + doktor_soyad) as 'DOKTORLAR', doktor_brans as 'BRANŞLAR' From doktor_tablo", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;

            //BRANŞI COMBOYA AKTARMA
            SqlCommand komut2 = new SqlCommand("Select brans_ad From brans_tablo", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0].ToString());
            }

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into randevu_tablo (randevu_tarih, randevu_saat, randevu_brans, randevu_doktor) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTarih.Text);
            komut.Parameters.AddWithValue("@p2", maskedSaat.Text);
            komut.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komut.Parameters.AddWithValue("@p4", cmbDr.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("RANDEVU OLUŞTURULDU", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            maskedSaat.Clear();
            maskedTarih.Clear();
            
            cmbDr.Items.Clear();
            cmbDr.Text = "";
            cmbBrans.Text = "";

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDr.Items.Clear();
            SqlCommand komut = new SqlCommand("Select doktor_ad, doktor_soyad From doktor_tablo where doktor_brans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                cmbDr.Items.Add(dr[0] + " " + dr[1]);
            }

        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into duyuru (duyuru_d) values (@p1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", richDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("DUYURU OLUŞTURULDU", "BİLGİ",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form_Duyuru duyuru = new Form_Duyuru();
            duyuru.Show();
        }
    }
}
