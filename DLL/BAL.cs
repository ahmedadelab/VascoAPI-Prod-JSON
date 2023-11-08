using System.Data;
using System.Data.SqlClient;

namespace VascoAPI
{
    public class BAL
    {
        public async void InsertLog(string VascoReq, string RequestID, string RequestDate)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var DBConnection = MyConfig.GetValue<string>("AppSettings:DBConnection");
            SqlConnection Database_Conection = new SqlConnection(DBConnection);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertVascoLog";
            cmd.Parameters.AddWithValue("VascoReq", VascoReq);
            cmd.Parameters.AddWithValue("RequestID", RequestID);
            cmd.Parameters.AddWithValue("RequestDate", RequestDate);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            Database_Conection.Open();
            cmd.ExecuteNonQuery();
            Database_Conection.Close();

        }

        public async void updateLog(string VascoResp, string RequestID, string ResponseDate)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var DBConnection = MyConfig.GetValue<string>("AppSettings:DBConnection");
            SqlConnection Database_Conection = new SqlConnection(DBConnection);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateVascoLog";
            cmd.Parameters.AddWithValue("VascoResp", VascoResp);
            cmd.Parameters.AddWithValue("RequestID", RequestID);
            cmd.Parameters.AddWithValue("ResponseDate", ResponseDate);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            Database_Conection.Open();
            cmd.ExecuteNonQuery();
            Database_Conection.Close();
        }
    }
}
