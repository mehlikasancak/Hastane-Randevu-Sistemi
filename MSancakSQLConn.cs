﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    internal class MSancakSQLConn
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=Mehlika\\SQLEXPRESS;Initial Catalog=Hastane;Integrated Security=True");
            baglan.Open();
            return baglan;  
        }
    }
}
