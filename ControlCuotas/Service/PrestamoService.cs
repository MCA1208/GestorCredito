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

        public DataTable CreateClient(string name, string dni, string address, string phone, int zone)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spCreateClient, con);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@dni", dni);
            comando.Parameters.AddWithValue("@address", name);
            comando.Parameters.AddWithValue("@phone", dni);
            comando.Parameters.AddWithValue("@zone", zone);

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

        public DataTable AddPrestamo(int cboCliente, string concepto, string amount, string amountInterest, int quantity, DateTime dateEnd)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spAddPrestamo, con);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@cboCliente", cboCliente);
            comando.Parameters.AddWithValue("@concepto", concepto);
            comando.Parameters.AddWithValue("@amount", amount);
            comando.Parameters.AddWithValue("@amountInterest", amountInterest);
            comando.Parameters.AddWithValue("@quantity", quantity);
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


    }
}