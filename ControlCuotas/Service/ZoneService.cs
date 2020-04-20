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

        public DataTable GetAllZone()
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllZone, con);

            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }

        public DataTable AddZone(string description, string userLogin)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spAddZone, con);
            comando.Parameters.AddWithValue("description", description);
            comando.Parameters.AddWithValue("@userLogin", userLogin);
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
        
        public DataTable ModifyZone(int IdZone,string Description, string userLogin)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spModifyZone, con);
            comando.Parameters.AddWithValue("@IdZone", IdZone);
            comando.Parameters.AddWithValue("@Description", Description);
            comando.Parameters.AddWithValue("@userLogin", userLogin);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(comando);

            da.Fill(dt);

            return dt;

        }


        //end Class
    }
}