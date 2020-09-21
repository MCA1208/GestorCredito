using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlCuotas.Service
{
    public class MarkService
    {
        DataTable dt = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        public DataTable GetAllMark(int userIdLogin, int userIdProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllMark, con);
            comando.Parameters.AddWithValue("@userId", userIdLogin);
            comando.Parameters.AddWithValue("@ProfileId", userIdProfile);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }
        public DataTable AddMark(string name, string userName, int userId)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spAddMark, con);
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@userName", userName);
            comando.Parameters.AddWithValue("@userId", userId);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }

        public DataTable ModifyMark(int idMark,string name, string userName, int userId)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spModifyMark, con);
            comando.Parameters.AddWithValue("@idMark", idMark);
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@userName", userName);
            comando.Parameters.AddWithValue("@userId", userId);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }

        public DataTable DeleteMark(int idMark)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spDeleteMark, con);
            comando.Parameters.AddWithValue("@idMark", idMark);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }
        public DataTable GetMarkById(int idMark)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetMarkById, con);
            comando.Parameters.AddWithValue("@idMark", idMark);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;
        }

    }//FIN
}