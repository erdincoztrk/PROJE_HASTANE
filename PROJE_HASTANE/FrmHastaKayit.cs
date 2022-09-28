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
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }
        Class_Baglanti bglanti = new Class_Baglanti();
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (maskedTC.Text == "" || maskedSifre.Text == "" || maskedTelNo.Text == "" || txtAd.Text == "" || txtSoyad.Text == "" || comboCinsiyet.Text == "")
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand komut = new SqlCommand("insert into hasta_tablo (hasta_ad, hasta_soyad, hasta_TC, hasta_telno, hasta_sifre, hasta_cinsiyet) values(@p1, @p2, @p3, @p4, @p5, @p6)", bglanti.baglanti());
                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", maskedTC.Text);
                komut.Parameters.AddWithValue("@p4", maskedTelNo.Text);
                komut.Parameters.AddWithValue("@p5", maskedSifre.Text);
                komut.Parameters.AddWithValue("@p6", comboCinsiyet.Text);
                komut.ExecuteNonQuery();
                bglanti.baglanti().Close();
                MessageBox.Show("KAYIT OLUNDU! ŞİFRENİZ: " + maskedSifre.Text, "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
