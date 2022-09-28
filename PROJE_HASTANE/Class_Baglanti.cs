using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PROJE_HASTANE
{
    class Class_Baglanti
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=LAPTOP-H333VG89;Initial Catalog=HASTANE VTYS;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
