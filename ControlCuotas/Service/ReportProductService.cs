using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlCuotas.Service
{
    public class ReportProductService
    {
        DataTable dt = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        public DataTable GetReportCuponClient(int IdClient)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetReportProductCuponByClient, con);
            comando.Parameters.AddWithValue("@IdClient", IdClient);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            return dt;

        }
    }//FIN
}