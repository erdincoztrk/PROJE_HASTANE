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
    public partial class Form_SekreterGiris : Form
    {
        public Form_SekreterGiris()
        {
            InitializeComponent();
        }
        Class_Baglanti bgl = new Class_Baglanti();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            
                
            SqlCommand komut = new SqlCommand("Select * From sekreter_tablo where sekreter_TC=@p1 and sekreter_sifre = @p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTC.Text);
            komut.Parameters.AddWithValue("@P2", maskedSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            
            if (dr.Read())
            {
                Form_sekreterpanel sekreter = new Form_sekreterpanel();
                sekreter.tc = maskedTC.Text;
                sekreter.Show();
                this.Hide();
                
            }
            else
            {
                MessageBox.Show("HATALI GİRİŞ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            bgl.baglanti().Close();
        }

        private void Form_SekreterGiris_Load(object sender, EventArgs e)
        {
            
        }
    }
}
