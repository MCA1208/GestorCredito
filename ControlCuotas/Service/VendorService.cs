using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlCuotas.Service
{
    public class VendorService
    {
        DataTable dt = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        public DataTable AddVendor(string name, int dni, DateTime birthday, string userLogin, int idUser)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spAddVendor, con);
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@dni", dni);
            comando.Parameters.AddWithValue("@birthday", birthday);
            comando.Parameters.AddWithValue("@modifyUser", userLogin);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;
        }

        public DataTable GetAllVendor(int idUser, int idProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllVendor, con);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.Parameters.AddWithValue("@idProfile", idProfile);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;
        }

        public DataTable ModifyVendor(int idVendor, string name, int dni, DateTime birthday,string userLogin, int idUser)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spModifyVendor, con);
            comando.Parameters.AddWithValue("@idVendor", idVendor);
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@dni", dni);
            comando.Parameters.AddWithValue("@birthday", birthday);
            comando.Parameters.AddWithValue("@userLogin", userLogin);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;
        }

        public DataTable DeleteVendor(int idVendor)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spDeleteVendor, con);
            comando.Parameters.AddWithValue("@idVendor", idVendor);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;
        }
        
        public DataTable GetVendorById(int idVendor)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetVendorById, con);
            comando.Parameters.AddWithValue("@idVendor", idVendor);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;
        }
    }
}