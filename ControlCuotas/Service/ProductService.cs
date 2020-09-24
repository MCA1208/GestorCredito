using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlCuotas.Service
{
    public class ProductService
    {
        DataTable dt = new DataTable();
        SqlConnection con;
        SqlCommand comando;
        StoreProcedureModel.SPName spName = new StoreProcedureModel.SPName();
        readonly ConnectionModel Connection = new ConnectionModel();

        public DataTable GetAllProduct(int userIdLogin, int userIdProfile)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetAllProduct, con);
            comando.Parameters.AddWithValue("@userId", userIdLogin);
            comando.Parameters.AddWithValue("@ProfileId", userIdProfile);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }
        public DataTable AddProduct(string name, string description, int idTypeProduct, int idMark, string costPrice, string salePrice, int stock, string userName, int userId)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spAddProduct, con);
            comando.Parameters.AddWithValue("@description", description);
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@idTypeProduct", idTypeProduct);
            comando.Parameters.AddWithValue("@idMark", idMark);
            comando.Parameters.AddWithValue("@costPrice", costPrice);
            comando.Parameters.AddWithValue("@salePrice", salePrice);
            comando.Parameters.AddWithValue("@stock", stock);
            comando.Parameters.AddWithValue("@userId", userId);
            comando.Parameters.AddWithValue("@userName", userName);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }

        public DataTable ModifyProduct(int idProduct, string name, string description, int idTypeProduct, int idMark, string costPrice, string salePrice, int stock, string userName, int userId)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spModifyProduct, con);
            comando.Parameters.AddWithValue("@idProduct", idProduct);
            comando.Parameters.AddWithValue("@name", name);
            comando.Parameters.AddWithValue("@description", description);
            comando.Parameters.AddWithValue("@idTypeProduct", idTypeProduct);
            comando.Parameters.AddWithValue("@idMark", idMark);
            comando.Parameters.AddWithValue("@costPrice", costPrice);
            comando.Parameters.AddWithValue("@salePrice", salePrice);
            comando.Parameters.AddWithValue("@stock", stock);
            comando.Parameters.AddWithValue("@userName", userName);
            comando.Parameters.AddWithValue("@userId", userId);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }

        public DataTable DeleteProduct(int idProduct)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spDeleteProduct, con);
            comando.Parameters.AddWithValue("@idProduct", idProduct);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }
        public DataTable GetProductById(int idProduct)
        {
            con = new SqlConnection(Connection.stringConn);
            comando = new SqlCommand(spName.spGetProductById, con);
            comando.Parameters.AddWithValue("@idProduct", idProduct);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);

            return dt;
        }
    }
}