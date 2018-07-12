using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Voice_Bot
{
    class Connection
    {
       

        static void koneksi() {

            List<int> list = new List<int>();
            SqlConnection conn = new SqlConnection("Server=.\\DANNYWAHYUDI-PC;Database=DotNetCore");
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from [Employees]");
            SqlDataReader reader = cmd.ExecuteReader();
            
            
        }
        
    }
}
