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
    public partial class Form_HastaBilgiDuzenle : Form
    {
        public Form_HastaBilgiDuzenle()
        {
            InitializeComponent();
        }
        public string TCno;
        Class_Baglanti bgl = new Class_Baglanti();
        private void Form_HastaBilgiDuzenle_Load(object sender, EventArgs e)
        {
            maskedTC.Text = TCno;
            SqlCommand komut = new SqlCommand("Select * From hasta_tablo where hasta_TC = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
               // maskedTC.Text = dr[3].ToString();
                maskedTelNo.Text = dr[4].ToString();
                maskedSifre.Text = dr[5].ToString();
                comboCinsiyet.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update hasta_tablo set hasta_ad=@p1, hasta_soyad=@p2, hasta_telno=@p3, hasta_sifre=@p4, hasta_cinsiyet = @p6 where hasta_TC=@p5", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtAd.Text);
            komut2.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", maskedTelNo.Text);
            komut2.Parameters.AddWithValue("@p4", maskedSifre.Text);
            komut2.Parameters.AddWithValue("@p5", maskedTC.Text);
            komut2.Parameters.AddWithValue("@p6", comboCinsiyet.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("BİLGİLER GÜNCELLENDİ","İŞLEM TAMAM" , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
