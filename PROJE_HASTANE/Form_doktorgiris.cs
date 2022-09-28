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
    public partial class Form_doktorgiris : Form
    {
        public Form_doktorgiris()
        {
            InitializeComponent();
        }
        Class_Baglanti bgl = new Class_Baglanti();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from doktor_tablo where doktor_TC = @p1 and doktor_sifre = @p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTC.Text);
            komut.Parameters.AddWithValue("@p2", maskedSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form_doktorpanel doktor = new Form_doktorpanel();
                doktor.TC = maskedTC.Text;
                doktor.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("HATALI GİRİŞ!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            bgl.baglanti().Close();
            
        }
    }
}
