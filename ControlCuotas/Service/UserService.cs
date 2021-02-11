using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlCuotas.Service
{
    public class UserService
    {
        DataTable dt = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        public DataTable AddUser(string name, string descriptionName, int idProfile, bool active, string pass, string userLogin)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spAddUser, con);
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@descriptionName", descriptionName);
            comando.Parameters.AddWithValue("@idProfile", idProfile);
            comando.Parameters.AddWithValue("@active", active);
            comando.Parameters.AddWithValue("@pass", pass);
            comando.Parameters.AddWithValue("@modifyUser", userLogin);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }

        public DataTable GetAllUser()
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllUser, con);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }

        public DataTable ModifyUser(int idUser, string name, string userDescription, int idProfile, bool active, string pass, string userLogin)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spModifyUser, con);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@userDescription", userDescription);
            comando.Parameters.AddWithValue("@idProfile", idProfile);
            comando.Parameters.AddWithValue("@active", active);
            comando.Parameters.AddWithValue("@pass", pass);
            comando.Parameters.AddWithValue("@userLogin", userLogin);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }

        public DataTable DeleteUser(int idUser)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spDeleteUser, con);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
           
            return dt;
        }

        public DataTable GeUserById(int idUser)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetUserById, con);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }

        public DataTable GetAllUserPermits(int idUser)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllPermitsApplication, con);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }

        public DataTable ModifyPermits(int idPermit, string permits, bool active, int idUser, string userLogin)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spModifyPermits, con);
            comando.Parameters.AddWithValue("@idPermit", idPermit);
            comando.Parameters.AddWithValue("@permits", permits);
            comando.Parameters.AddWithValue("@active", active);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.Parameters.AddWithValue("@userLogin", userLogin);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }
    }
}