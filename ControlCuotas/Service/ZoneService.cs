using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlCuotas.Service
{
    public class ZoneService
    {

        DataTable dt = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        public DataTable GetAllZone(int idUser, int idProfile)
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

        public DataTable AddZone(string description, string userLogin, int idUser)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spAddZone, con);
            comando.Parameters.AddWithValue("description", description);
            comando.Parameters.AddWithValue("@userLogin", userLogin);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable GetZoneById(int IdZone)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetZoneById, con);
            comando.Parameters.AddWithValue("@IdZone", IdZone);

            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }
        
        public DataTable ModifyZone(int IdZone,string Description, bool Active, string userLogin, int idUser)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spModifyZone, con);
            comando.Parameters.AddWithValue("@IdZone", IdZone);
            comando.Parameters.AddWithValue("@Description", Description);
            comando.Parameters.AddWithValue("@Active", Active);
            comando.Parameters.AddWithValue("@userLogin", userLogin);
            comando.Parameters.AddWithValue("@idUser", idUser);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable DeleteZone(int IdZone)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spDeleteZone, con);
            comando.Parameters.AddWithValue("@IdZone", IdZone);            
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }


        //end Class
    }
}