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
    public partial class Form_doktorpanel : Form
    {
        public Form_doktorpanel()
        {
            InitializeComponent();
        }

        private void btnBilgiDuzen_Click(object sender, EventArgs e)
        {
            Form_doktorbilgiduzenle drbilgiduzenle = new Form_doktorbilgiduzenle();
            drbilgiduzenle.TC = lblTC.Text;
            drbilgiduzenle.Show();
        }

        private void btnDuyuru_Click(object sender, EventArgs e)
        {
            Form_Duyuru duyuru = new Form_Duyuru();
            duyuru.Show();
        }
        public string TC;
        Class_Baglanti bgl = new Class_Baglanti();
        private void Form_doktorpanel_Load(object sender, EventArgs e)
        {
            lblTC.Text = TC;
            SqlCommand komut = new SqlCommand("select doktor_ad, doktor_soyad from doktor_tablo where doktor_TC = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                lblAd_Soyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();



            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from randevu_tablo where randevu_doktor ='"+lblAd_Soyad.Text+"'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            richSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
