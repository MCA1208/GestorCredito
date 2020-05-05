using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlCuotas.Service
{
    public class ClientService
    {

        DataTable dt = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        public DataTable GetAllClient(int idUser, int idProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllClient, con);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.Parameters.AddWithValue("@IdProfile", idProfile);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable CreateClient(string name, string dni, string address, string phone, int zone, DateTime? birthDate, bool? married, string conyuge, string dniConyuge, int cboSitCred, string userLogin, int idUser)
        {

            married = married == null ? false : true;

            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spCreateClient, con);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@dni", dni);
            comando.Parameters.AddWithValue("@address", address);
            comando.Parameters.AddWithValue("@phone", phone);
            comando.Parameters.AddWithValue("@zone", zone);
            comando.Parameters.AddWithValue("@birthDate", birthDate);
            comando.Parameters.AddWithValue("@married", married);
            comando.Parameters.AddWithValue("@conyuge", conyuge);
            comando.Parameters.AddWithValue("@dniConyuge", dniConyuge);
            comando.Parameters.AddWithValue("@cboSitCred", cboSitCred);
            comando.Parameters.AddWithValue("@userLogin", userLogin);
            comando.Parameters.AddWithValue("@idUser", idUser);

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable GetComboZona(int idUser, int idProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllZone, con);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.Parameters.AddWithValue("@IdProfile", idProfile);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable GetClientCombo(int idUser, int idProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllClient, con);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.Parameters.AddWithValue("@IdProfile", idProfile);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable GetClientById(int IdClient)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetClientById, con);

            comando.Parameters.AddWithValue("IdClient", IdClient);

            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }


        public DataTable ModifyClient(int IdClient, string name, string dni, string address, string phone, int zone, DateTime? birthDate, bool? married, string conyuge, string dniConyuge, int cboSitCred, string userLogin, int idUser)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spModifyClient, con);

            comando.Parameters.AddWithValue("@IdClient", IdClient);
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@dni", dni);
            comando.Parameters.AddWithValue("@address", address);
            comando.Parameters.AddWithValue("@phone", phone);
            comando.Parameters.AddWithValue("@zone", zone);
            comando.Parameters.AddWithValue("@birthDate", birthDate);
            comando.Parameters.AddWithValue("@married", married);
            comando.Parameters.AddWithValue("@conyuge", conyuge);
            comando.Parameters.AddWithValue("@dniConyuge", dniConyuge);
            comando.Parameters.AddWithValue("@cboSitCred", cboSitCred);
            comando.Parameters.AddWithValue("@userLogin", userLogin);
            comando.Parameters.AddWithValue("@idUser", idUser);

            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable GetClientByDNI(string DNI)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetClientByDNI, con);

            comando.Parameters.AddWithValue("@DNI", DNI);

            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable DeleteClient(int IdClient)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spDeleteClient, con);

            comando.Parameters.AddWithValue("@IdClient", IdClient);

            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }


    }
}