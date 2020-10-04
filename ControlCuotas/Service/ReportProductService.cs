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

        public DataTable GetReportProductCobranza(int IdZone, int idUser, int idProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spReportProductCobranza, con);
            comando.Parameters.AddWithValue("@IdZone", IdZone);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.Parameters.AddWithValue("@idProfile", idProfile);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            return dt;

        }

        public DataTable GetReportProductIrregularPayment(int? IdClient, int? IdZone, int userIdLogin, int userIdProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spReportProductIrregularPayment, con);
            comando.Parameters.AddWithValue("@idClient", IdClient);
            comando.Parameters.AddWithValue("@idZone", IdZone);
            comando.Parameters.AddWithValue("@idUser", userIdLogin);
            comando.Parameters.AddWithValue("@idProfile", userIdProfile);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            return dt;

        }

        public DataTable GetReportProductQuotaPaid(int? IdZone, DateTime? dStart, DateTime? dEnd, int userIdLogin, int userIdProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spReportProductQuotaPaid, con);
            comando.Parameters.AddWithValue("@idZone", IdZone);
            comando.Parameters.AddWithValue("@dateStart", dStart);
            comando.Parameters.AddWithValue("@dateEnd", dEnd);
            comando.Parameters.AddWithValue("@idUser", userIdLogin);
            comando.Parameters.AddWithValue("@idProfile", userIdProfile);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            return dt;

        }

    }//FIN
}