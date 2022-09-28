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
    public partial class Form_hastagiris : Form
    {
        public Form_hastagiris()
        {
            InitializeComponent();
        }

        private void lnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit hastakayit = new FrmHastaKayit();
            hastakayit.Show();
        }
        Class_Baglanti bglanti = new Class_Baglanti();
        private void btnGiris_Click(object sender, EventArgs e)
        {
           SqlCommand komut = new SqlCommand("Select * From hasta_tablo where hasta_TC=@p1 and hasta_sifre=@p2", bglanti.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTC.Text);
            komut.Parameters.AddWithValue("@p2", maskedSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                Form_hastapanel hastapanel = new Form_hastapanel();
                hastapanel.tc = maskedTC.Text;
                hastapanel.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("HATALI GİRİŞ", "UYARI" ,MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form_hastagiris_Load(object sender, EventArgs e)
        {

        }
    }
}
