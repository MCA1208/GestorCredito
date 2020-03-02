using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlCuotas.Service
{
    public class PrestamoService
    {
        DataTable dt = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        public DataTable CreateClient(string name, string dni, string address, string phone, int zone, DateTime? birthDate, bool? married, string conyuge, string dniConyuge, int cboSitCred)
        {

            married = married == null ? false : true;

            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spCreateClient, con);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@dni", dni);
            comando.Parameters.AddWithValue("@address", name);
            comando.Parameters.AddWithValue("@phone", dni);
            comando.Parameters.AddWithValue("@zone", zone);
            comando.Parameters.AddWithValue("@birthDate", birthDate);
            comando.Parameters.AddWithValue("@married", married);
            comando.Parameters.AddWithValue("@conyuge", conyuge);
            comando.Parameters.AddWithValue("@dniConyuge", dniConyuge);
            comando.Parameters.AddWithValue("@cboSitCred", cboSitCred);

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable GetAllClient()
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllClient, con);

            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable AddPrestamo(int cboCliente, string concepto, string amount, string amountInterest, int quantity, DateTime dateStart, DateTime dateEnd)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spAddPrestamo, con);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@cboCliente", cboCliente);
            comando.Parameters.AddWithValue("@concepto", concepto);
            comando.Parameters.AddWithValue("@amount", amount);
            comando.Parameters.AddWithValue("@amountInterest", amountInterest);
            comando.Parameters.AddWithValue("@quantity", quantity);
            comando.Parameters.AddWithValue("@dateStart", dateStart);
            comando.Parameters.AddWithValue("@dateEnd", dateEnd);

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }



        public DataTable GetAllPrestamo()
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllPrestamo, con);

            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        
        public DataTable GetPrestamoDetailById(int IdPrestamo)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetPrestamoDetailById, con);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IdPrestamo", IdPrestamo);
            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        

        public DataTable ChangeEstatusCuota(int IdCuota)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spChangeEstatusCuota, con);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IdCuota", IdCuota);
            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }


        public DataTable GetCuotaDetail(int IdCuota)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetCuotaDetail, con);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IdCuota", IdCuota);
            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable SaveCuotaForId(int IdCuota, DateTime? fecha, string observation)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spSaveCuotaForId, con);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IdCuota", IdCuota);
            comando.Parameters.AddWithValue("@fecha", fecha);
            comando.Parameters.AddWithValue("@observation", observation);
            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable GetPrestamoById(int IdPrestamo)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetPrestamoForId, con);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IdPrestamo", IdPrestamo);
            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable SavePrestamoForId(int IdPrestamo, DateTime dateStart, DateTime dateEnd)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spSavePrestamoForId, con);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IdPrestamo", IdPrestamo);
            comando.Parameters.AddWithValue("@dateStart", dateStart);
            comando.Parameters.AddWithValue("@dateEnd", dateEnd);
            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        //FIN SERVICE
    }
}