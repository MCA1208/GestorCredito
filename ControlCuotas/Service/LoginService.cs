using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ControlCuotas.Models;

namespace ControlCuotas.Service
{
    public class LoginService
    {

        DataTable dt = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        public DataTable SpUserLogin(string users, string password)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spUserLogin, con);

            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@users", users);
            comando.Parameters.AddWithValue("@password", password);

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }
    }
}