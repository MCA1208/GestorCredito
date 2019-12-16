using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlCuotas.Service
{
    public class ReportService
    {
        DataTable dt = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        public DataTable GetReportPrincipal(int? IdClient, int? IdZone, DateTime? dateFrom, DateTime? DateUp)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetReportPrincipal, con);

            comando.Parameters.AddWithValue("@idClient", IdClient);
            comando.Parameters.AddWithValue("@idZone", IdZone);
            comando.Parameters.AddWithValue("@dateFrom", dateFrom);
            comando.Parameters.AddWithValue("@dateUp", DateUp);


            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

    }//End Class
}