using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API
{
    public static class Genel
    {
        private static string connectionString = "Data Source=root-muhittin\\SQL2014;Persist Security Info=true;User Id=sa;Password=qaz;Initial Catalog=BKE";

        public static SqlConnection createDBConnection()
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            return cnn;
        }
    }
}
