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
    public partial class Form_doktorbilgiduzenle : Form
    {
        public Form_doktorbilgiduzenle()
        {
            InitializeComponent();
        }
        public string TC;
        Class_Baglanti bgl = new Class_Baglanti();
        private void Form_doktorbilgiduzenle_Load(object sender, EventArgs e)
        {
            SqlCommand komut0 = new SqlCommand("select brans_ad from brans_tablo", bgl.baglanti());
            SqlDataReader dr0 = komut0.ExecuteReader();
            while (dr0.Read())
            {
                cmbBrans.Items.Add(dr0[0].ToString());
            }
            bgl.baglanti().Close();

            //
            maskedTC.Text = TC;
            SqlCommand komut1 = new SqlCommand("select * from doktor_tablo where doktor_tc = @p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", maskedTC.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while(dr1.Read())
            {
                txtAd.Text = dr1[1].ToString();
                txtSoyad.Text = dr1[2].ToString();
                cmbBrans.Text = dr1[3].ToString();
                maskedSifre.Text = dr1[5].ToString();
            }
            bgl.baglanti().Close();


            //
            
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("Update doktor_tablo set doktor_sifre=@p1, doktor_brans=@p2 where doktor_TC = @p3", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p3", maskedTC.Text);
            komut2.Parameters.AddWithValue("@p2", cmbBrans.Text);
            komut2.Parameters.AddWithValue("@p1", maskedSifre.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("ŞİFRENİZ GÜNCELLENDİ!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
