using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJE_HASTANE
{
    public partial class anagiris : Form
    {
        public anagiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_hastagiris hastagiris = new Form_hastagiris();
            hastagiris.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_doktorgiris doktorgiris = new Form_doktorgiris();
            doktorgiris.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form_SekreterGiris sekretergiris = new Form_SekreterGiris();
            sekretergiris.Show();
            this.Hide();
        }
    }
}
