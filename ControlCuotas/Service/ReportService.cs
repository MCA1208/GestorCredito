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

        public DataTable GetReportPrincipal(int? IdClient, int? IdZone, DateTime? dateFrom, DateTime? DateUp, int idUser, int idProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetReportPrincipal, con);

            comando.Parameters.AddWithValue("@idClient", IdClient);
            comando.Parameters.AddWithValue("@idZone", IdZone);
            comando.Parameters.AddWithValue("@dateFrom", dateFrom);
            comando.Parameters.AddWithValue("@dateUp", DateUp);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.Parameters.AddWithValue("@idProfile", idProfile);

            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            return dt;

        }

        public DataTable GetReportCuotaStatus(int? IdClient, int? IdZone, DateTime? DateStart, DateTime? DateEnd, int idUser, int idProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spReportCuponStatus, con);

            comando.Parameters.AddWithValue("@idClient", IdClient);
            comando.Parameters.AddWithValue("@idZone", IdZone);
            comando.Parameters.AddWithValue("@dateStart", DateStart);
            comando.Parameters.AddWithValue("@dateEnd", DateEnd);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.Parameters.AddWithValue("@idProfile", idProfile);

            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            return dt;

        }

        public DataTable GetReportGanancia( DateTime? DateStart, DateTime? DateEnd, int idUser, int idProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spReportInvestmentAndProfit, con);
            comando.Parameters.AddWithValue("@dateStart", DateStart);
            comando.Parameters.AddWithValue("@dateEnd", DateEnd);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.Parameters.AddWithValue("@idProfile", idProfile);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            return dt;

        }
        
        public DataTable GetReportCuponClient(int IdClient)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetReportCuponByClient, con);
            comando.Parameters.AddWithValue("@IdClient", IdClient);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            return dt;

        }
        public DataTable GetReportSummaryClient(int IdClient, int idUser, int idProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spReportSummaryClient, con);
            comando.Parameters.AddWithValue("@IdClient", IdClient);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.Parameters.AddWithValue("@idProfile", idProfile);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            return dt;

        }
        
        public DataTable GetReportSummaryDetail(int IdPrestamo)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spReportSummaryDetail, con);
            comando.Parameters.AddWithValue("@IdPrestamo", IdPrestamo);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            return dt;

        }

        public DataTable GetReportCobranza(int IdZone, int idUser, int idProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spReportCobranza, con);
            comando.Parameters.AddWithValue("@IdZone", IdZone);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.Parameters.AddWithValue("@idProfile", idProfile);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            return dt;

        }

        public DataTable GetReportIrregularPayment(int? IdClient, int? IdZone, int userIdLogin, int userIdProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spReportIrregularPayment, con);
            comando.Parameters.AddWithValue("@idClient", IdClient);
            comando.Parameters.AddWithValue("@idZone", IdZone);
            comando.Parameters.AddWithValue("@idUser", userIdLogin);
            comando.Parameters.AddWithValue("@idProfile", userIdProfile);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            return dt;

        }

    }//End Class  
}