using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlCuotas.Service
{
    public class TypeProductService
    {
        DataTable dt = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        public DataTable GetAllTypeProduct(int userIdLogin, int userIdProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllTypeProduct, con);
            comando.Parameters.AddWithValue("@userId", userIdLogin);
            comando.Parameters.AddWithValue("@ProfileId", userIdProfile);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }
        public DataTable AddTypeProduct(string name, string userName, int userId)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spAddTypeProduct, con);
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@userName", userName);
            comando.Parameters.AddWithValue("@userId", userId);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }

        public DataTable ModifyTypeProduct(int idTypeProd, string name, string userName, int userId)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spModifyTypeProduct, con);
            comando.Parameters.AddWithValue("@idTypeProduct", idTypeProd);
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@userName", userName);
            comando.Parameters.AddWithValue("@userId", userId);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }

        public DataTable DeleteTypeProduct(int idTypeProd)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spDeleteTypeProduct, con);
            comando.Parameters.AddWithValue("@idTypeProduct", idTypeProd);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }
        public DataTable GetTypeProductById(int idTypeProd)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetTypeProductById, con);
            comando.Parameters.AddWithValue("@idTypeProduct", idTypeProd);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }
    }
}